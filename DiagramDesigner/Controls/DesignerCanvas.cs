using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Linq;
using System.Windows.Shapes;

namespace DiagramDesigner
{
    public class DesignerCanvas : Canvas
    {
        private ConnectorViewModel partialConnection;
        private List<Connector> connectorsHit = new List<Connector>();
        private Connector sourceConnector;
        private Point? rubberbandSelectionStartPoint = null;
        private double lineNumber = 1;
        private double circleNumber = 1;
        private System.Windows.Shapes.Path CurrentElement { get; set; }
        private CircleDesignerItemViewModel CurrentCircle { get; set; }
        private Point BeforeMovingPosition { get; set; }
        private Point AnotherPoint { get; set; }
        public IDiagramViewModel Items { get; set; }
        public List<SelectableDesignerItemViewModelBase> PathItems
        {
            get { return Items.Items.Where(v => v is PathDesignerItemViewModel || v is CircleDesignerItemViewModel).ToList(); }
        }
        public List<SelectableDesignerItemViewModelBase> SelectedLineItems
        {
            get { return PathItems.Where(v => v.IsSelected == true).ToList(); }
        }
        public List<CircleDesignerItemViewModel> Arcs
        {
            get { return PathItems.Where(v => v is CircleDesignerItemViewModel).Cast<CircleDesignerItemViewModel>().ToList(); }
        }
        public List<CircleDesignerItemViewModel> SelectedArcs
        {
            get { return SelectedLineItems.Where(v => v is CircleDesignerItemViewModel).Cast<CircleDesignerItemViewModel>().ToList(); }
        }

        public DesignerCanvas()
        {
            this.AllowDrop = true;
            Mediator.Instance.Register(this);
        }

        private CircleDesignerItemViewModel CreateCircleObj(System.Windows.Shapes.Path element)
        {
            var shape = element.DataContext as CircleDesignerItemViewModel;
            return shape;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            Items = (DataContext as IDiagramViewModel);
            BeforeMovingPosition = e.GetPosition(this);
            var element = e.Source as System.Windows.Shapes.Path;
            CurrentElement = element;
            if (e.Source is System.Windows.Shapes.Path)
            {
                if (CurrentElement.DataContext is CircleDesignerItemViewModel)
                {
                    var shape = CreateCircleObj(CurrentElement);

                    // 中心点の可動域を表す直線の端点を求める
                    if (CurrentElement.Name == "Center")
                    {
                        var startVector = shape.Start.Point - shape.Center.Point;
                        var endVector = shape.End.Point - shape.Center.Point;
                        var congruentVector = startVector + endVector;

                        var angle = Vector.AngleBetween(startVector, endVector);

                        // 合成ベクトルを評価するために角度の確認
                        if (Math.Abs(angle) == 180)
                        {
                            var point = new Point(startVector.Y, -startVector.X);
                            shape.CenterRangeStart.Point = new Point(shape.Center.Point.X - point.X, shape.Center.Point.Y - point.Y);
                            shape.CenterRangeEnd.Point = new Point(shape.Center.Point.X + point.X, shape.Center.Point.Y + point.Y);
                        }
                        else
                        {
                            shape.CenterRangeStart.Point = new Point(shape.Center.Point.X - congruentVector.X, shape.Center.Point.Y - congruentVector.Y);
                            shape.CenterRangeEnd.Point = new Point(shape.Center.Point.X + congruentVector.X, shape.Center.Point.Y + congruentVector.Y);
                        }
                    }

                    if (SelectedArcs.Any())
                    {
                        foreach (var selectedArc in SelectedArcs)
                        {
                            selectedArc.SetRadius = 5;
                            selectedArc.CenterFlag = true;
                        }
                    }
                }
                else
                {
                    var shape = CurrentElement.DataContext as PathDesignerItemViewModel;
                    if (CurrentElement.Name == "StartPoint")
                    {
                        AnotherPoint = shape.End.Point;
                    }
                    else if (CurrentElement.Name == "EndPoint")
                    {
                        AnotherPoint = shape.Start.Point;
                    }
                }
            }
            else if (Arcs.Any())
            {
                foreach (var arc in Arcs.Where(v => v.CenterFlag == true))
                {
                    arc.SetRadius = 0;
                    arc.CenterFlag = false;
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //if we are source of event, we are rubberband selecting
                if (e.Source == this)
                {
                    // in case that this click is the start for a 
                    // drag operation we cache the start point
                    rubberbandSelectionStartPoint = e.GetPosition(this);

                    IDiagramViewModel vm = (this.DataContext as IDiagramViewModel);
                    if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        vm.ClearSelectedItemsCommand.Execute(null);
                    }
                    e.Handled = true;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed && CurrentElement != null)
            {
                var currentPoint = e.GetPosition(this);
                var newX = currentPoint.X;
                var newY = currentPoint.Y;

                if (newX < 0)
                {
                    newX = 0;
                }
                if (newX > this.ActualWidth - CurrentElement.Width)
                {
                    newX = this.ActualWidth - CurrentElement.Width;
                }

                if (newY < 0)
                {
                    newY = 0;
                }
                if (newY > this.ActualHeight - CurrentElement.Height)
                {
                    newY = this.ActualHeight - CurrentElement.Height;
                }

                // 両端を移動する処理
                if (CurrentElement.Name == "Line" || CurrentElement.Name == "Arc")
                {
                    ShiftGeometry(newX, newY);
                    BeforeMovingPosition = currentPoint;
                    CheckSnap();
                }
                // 点を扱う処理
                else
                {
                    var ellipse = CurrentElement.Data as EllipseGeometry;

                    if (CurrentElement.DataContext is PathDesignerItemViewModel)
                    {
                        ellipse.Center = new Point(newX, newY);
                        CheckSnap();

                        // コントロールキーを押しながら端点を移動すると
                        // 直線の角度が45度刻みに補正される
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            var shape = CurrentElement.DataContext as PathDesignerItemViewModel;
                            var vector = currentPoint - AnotherPoint;
                            var digree = CorrectAngle(AnotherPoint, currentPoint);
                            ellipse.Center = CalcNewPoint(AnotherPoint, vector.Length, digree);
                        }
                    }
                    else if (CurrentElement.DataContext is CircleDesignerItemViewModel)
                    {
                        if (CurrentElement.Name == "Center")
                        {
                            ellipse.Center = CalcCenterPoint(currentPoint);
                        }
                        else
                        {
                            ellipse.Center = CalcCirclesPoint(currentPoint);
                            CheckSnap();

                            // コントロールキーを押しながら端点を移動すると
                            // 曲線の角度が45度刻みに補正される
                            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                            {
                                var shape = CreateCircleObj(CurrentElement);
                                var digree = CorrectAngle(shape.Center.Point, currentPoint);
                                ellipse.Center = CalcNewPoint(shape.Center.Point, shape.Radius.Width, digree);
                            }

                        }
                    }
                }
            }


            if (SourceConnector != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPoint = e.GetPosition(this);
                    partialConnection.SinkConnectorInfo = new PartCreatedConnectionInfo(currentPoint);
                    HitTesting(currentPoint);
                }
            }
            else
            {
                // if mouse button is not pressed we have no drag operation, ...
                if (e.LeftButton != MouseButtonState.Pressed)
                {
                    rubberbandSelectionStartPoint = null;
                }

                // ... but if mouse button is pressed and start
                // point value is set we do have one
                if (this.rubberbandSelectionStartPoint.HasValue)
                {
                    // create rubberband adorner
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                    if (adornerLayer != null)
                    {
                        RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                        }
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            CurrentElement = null;

            Mediator.Instance.NotifyColleagues<bool>("DoneDrawingMessage", true);

            if (sourceConnector != null)
            {
                FullyCreatedConnectorInfo sourceDataItem = sourceConnector.DataContext as FullyCreatedConnectorInfo;
                if (connectorsHit.Count() == 2)
                {
                    Connector sinkConnector = connectorsHit.Last();
                    FullyCreatedConnectorInfo sinkDataItem = sinkConnector.DataContext as FullyCreatedConnectorInfo;

                    int indexOfLastTempConnection = sinkDataItem.DataItem.Parent.Items.Count - 1;
                    sinkDataItem.DataItem.Parent.RemoveItemCommand.Execute(
                        sinkDataItem.DataItem.Parent.Items[indexOfLastTempConnection]);
                    sinkDataItem.DataItem.Parent.AddItemCommand.Execute(new ConnectorViewModel(sourceDataItem, sinkDataItem));
                }
                else
                {
                    //Need to remove last item as we did not finish drawing the path
                    int indexOfLastTempConnection = sourceDataItem.DataItem.Parent.Items.Count - 1;
                    sourceDataItem.DataItem.Parent.RemoveItemCommand.Execute(
                        sourceDataItem.DataItem.Parent.Items[indexOfLastTempConnection]);
                }
            }
            connectorsHit = new List<Connector>();
            sourceConnector = null;
        }

        // 中心点の移動制限と変更された中心点の値を求める処理
        private Point CalcCenterPoint(Point currentPoint)
        {
            // 移動した中心点を線分上に補正する処理
            var shape = CreateCircleObj(CurrentElement);
            var centerVector = shape.CenterRangeEnd.Point - shape.CenterRangeStart.Point;
            var currentPointVector = currentPoint - shape.CenterRangeStart.Point;
            var distance = Vector.Multiply(centerVector, currentPointVector) / Math.Pow(centerVector.Length, 2);

            // 新しい中心点の座標
            var newX = shape.CenterRangeStart.Point.X + distance * centerVector.X;
            var newY = shape.CenterRangeStart.Point.Y + distance * centerVector.Y;

            return new Point(newX, newY);
        }

        // 移動された端点を円周上の点に補正しその座標を返す
        private Point CalcCirclesPoint(Point point)
        {
            var shape = CreateCircleObj(CurrentElement);
            var rad = Math.Atan2(point.Y - shape.Center.Point.Y, point.X - shape.Center.Point.X);
            return CalcNewPoint(shape.Center.Point, shape.Radius.Width, rad);
        }

        // 求めたい点の座標を求める
        private Point CalcNewPoint(Point addPoint, double radius, double digree)
        {
            var cos = Math.Cos(digree);
            var sin = Math.Sin(digree);
            var x = addPoint.X + cos * radius;
            var y = addPoint.Y + sin * radius;
            return new Point(x, y);
        }

        // 現座の座標の角度を45度に補正する
        private double CorrectAngle(Point addPoint, Point currPoint)
        {
            var vector = currPoint - addPoint;
            var rad = Math.Atan2(currPoint.Y - addPoint.Y, currPoint.X - addPoint.X);
            var actualDig = ReturnDig(rad);
            return ReturnRad(Math.Floor(actualDig / 45) * 45);
        }

        // 与えた角度を弧度法で返す
        private double ReturnRad(double digree)
        {
            return digree * (Math.PI / 180);
        }

        // 与えたラジアンを度数法で返す
        private double ReturnDig(double radian)
        {
            return radian * (180 / Math.PI);
        }

        // パスの位置を変更する処理
        private void ShiftGeometry(double newX, double newY)
        {
            var diffX = newX - BeforeMovingPosition.X;
            var diffY = newY - BeforeMovingPosition.Y;
            var v = new Vector(diffX, diffY);

            foreach (var selectedLineItem in SelectedLineItems)
            {
                selectedLineItem.Start.Point += v;
                selectedLineItem.End.Point += v;
                if (selectedLineItem is CircleDesignerItemViewModel)
                {
                    selectedLineItem.Center.Point += v;
                }
            }
        }

        // 端点同士のスナップ判定のための処理
        // 端点同士が端点の直径未満まで近づくとスナップ
        private void CheckSnap()
        {
            foreach (var movingShape in SelectedLineItems)
            {
                // 接続される側のパスリスト
                var accessLines = PathItems.Where(v => v != movingShape).Where(v => v.IsSelected == false).ToList();

                // 始点を動かし接続する場合
                if (CurrentElement.Name != "EndPoint")
                {
                    foreach (var accessLine in accessLines)
                    {
                        var startToStart = accessLine.Start.Point - movingShape.Start.Point;
                        var startToEnd = accessLine.End.Point - movingShape.Start.Point;

                        // 動かす始点からほかの始点へのスナップ判定
                        if (startToStart.Length < 10)
                        {
                            if (movingShape is CircleDesignerItemViewModel && movingShape.End.AccessLine.Any())
                            {
                                if (movingShape.End.AccessLine.Contains(accessLine) && accessLine.Start.AccessLine.Contains(movingShape))
                                {
                                    continue;
                                }
                            }

                            SnapConnection(movingShape, startToStart, "StartPoint");
                            if (!movingShape.Start.AccessLine.Contains(accessLine))
                            {
                                movingShape.Start.AccessLine.Add(accessLine);
                            }
                            if (!accessLine.Start.AccessLine.Contains(movingShape))
                            {
                                accessLine.Start.AccessLine.Add(movingShape);
                            }
                        }

                        // 動かす始点からほかの終点へのスナップ判定
                        if (startToEnd.Length < 10)
                        {
                            if (movingShape is CircleDesignerItemViewModel && movingShape.End.AccessLine.Any())
                            {
                                if (movingShape.End.AccessLine.Contains(accessLine) && accessLine.End.AccessLine.Contains(movingShape))
                                {
                                    continue;
                                }
                            }

                            SnapConnection(movingShape, startToEnd, "StartPoint");
                            if (!movingShape.Start.AccessLine.Contains(accessLine))
                            {
                                movingShape.Start.AccessLine.Add(accessLine);
                            }
                            if (!accessLine.End.AccessLine.Contains(movingShape))
                            {
                                accessLine.End.AccessLine.Add(movingShape);
                            }
                        }
                    }
                }

                // 終点を動かし接続する場合
                if (CurrentElement.Name != "StartPoint")
                {
                    foreach (var accessLine in accessLines)
                    {
                        var endToStart = accessLine.Start.Point - movingShape.End.Point;
                        var endToEnd = accessLine.End.Point - movingShape.End.Point;

                        // 動かす終点からほかの始点へのスナップ判定
                        if (endToStart.Length < 10)
                        {
                            if (movingShape is CircleDesignerItemViewModel && movingShape.Start.AccessLine.Any())
                            {
                                if (movingShape.Start.AccessLine.Contains(accessLine) && accessLine.Start.AccessLine.Contains(movingShape))
                                {
                                    continue;
                                }
                            }

                            SnapConnection(movingShape, endToStart, "EndPoint");
                            if (!movingShape.End.AccessLine.Contains(accessLine))
                            {
                                movingShape.End.AccessLine.Add(accessLine);
                            }
                            if (!accessLine.Start.AccessLine.Contains(movingShape))
                            {
                                accessLine.Start.AccessLine.Add(movingShape);
                            }
                        }

                        // 動かす終点からほかの終点へのスナップ判定
                        if (endToEnd.Length < 10)
                        {
                            if (movingShape is CircleDesignerItemViewModel && movingShape.Start.AccessLine.Any())
                            {
                                if (movingShape.Start.AccessLine.Contains(accessLine) && accessLine.End.AccessLine.Contains(movingShape))
                                {
                                    continue;
                                }
                            }

                            SnapConnection(movingShape, endToEnd, "EndPoint");
                            if (!movingShape.End.AccessLine.Contains(accessLine))
                            {
                                movingShape.End.AccessLine.Add(accessLine);
                            }
                            if (!accessLine.End.AccessLine.Contains(movingShape))
                            {
                                accessLine.End.AccessLine.Add(movingShape);
                            }
                        }
                    }
                }

                CheckConnectionStatus(accessLines, movingShape);
            }
        }

        // パスの端点同士をスナップさせる処理
        private void SnapConnection(SelectableDesignerItemViewModelBase movingShape, Vector diff, string elementName)
        {
            // 始点を移動する場合
            if (CurrentElement.Name == "StartPoint")
            {
                movingShape.Start.Point += diff;
                if (movingShape is CircleDesignerItemViewModel)
                {
                    movingShape.Center.Point = CalcCenterPoint(movingShape);
                }
            }

            // 終点を移動する場合
            if (CurrentElement.Name == "EndPoint")
            {
                movingShape.End.Point += diff;
                if (movingShape is CircleDesignerItemViewModel)
                {
                    movingShape.Center.Point = CalcCenterPoint(movingShape);
                }
            }

            // 始点も終点も動かす場合
            if (CurrentElement.Name == "Line" || CurrentElement.Name == "Arc")
            {
                movingShape.End.Point += diff;
                movingShape.Start.Point += diff;

                // 動かしているパスが円弧の場合
                if (movingShape is CircleDesignerItemViewModel)
                {
                    movingShape.Center.Point += diff;
                }

                // 動かしているパスがほかのパスと接続状態である場合
                // 選択状態のパスの数で判定している
                if (SelectedLineItems.Count > 1)
                {
                    var connectedLines = SelectedLineItems.Where(v => v != movingShape);

                    foreach (var connectedLine in connectedLines)
                    {
                        connectedLine.Start.Point += diff;
                        connectedLine.End.Point += diff;
                        if (connectedLine is CircleDesignerItemViewModel)
                        {
                            connectedLine.Center.Point += diff;
                        }
                    }
                }
            }

        }

        // 円弧の端点の移動中にスナップが働いた場合に
        // スナップ後の円弧の中心点の座標を補正する処理
        private Point CalcCenterPoint(SelectableDesignerItemViewModelBase movingShape)
        {
            var shape = movingShape as CircleDesignerItemViewModel;
            var start = movingShape.Start.Point;
            var end = movingShape.End.Point;
            var center = movingShape.Center.Point;
            var radius = Math.Max((start - center).Length, (end - center).Length);
            var mid = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var squared = Math.Pow(radius, 2);
            var x = Math.Pow(start.X - mid.X, 2) + Math.Pow(end.Y - mid.Y, 2);
            if (squared >= x && x > 0)
            {
                var d = Math.Sqrt(squared / x - 1);
                var dx = d * (end.Y - mid.Y);
                var dy = d * (end.X - mid.X);
                var res1 = new Point(mid.X + dx, mid.Y - dy);
                var res2 = new Point(mid.X - dx, mid.Y + dy);
                if (shape.Digree > 0)
                {
                    return res2;
                }
                else
                {
                    return res1;
                }
            }
            return shape.Center.Point;
        }

        // 端点が持っている接続先の情報と実際の接続情報を一致させるための処理
        // movingShapeの端点の座標とその端点が持っている接続先の端点の座標が異なる場合、
        // 矛盾した接続情報のみを削除
        private void CheckConnectionStatus(List<SelectableDesignerItemViewModelBase> accessLines, SelectableDesignerItemViewModelBase movingShape)
        {
            foreach (var accessLine in accessLines)
            {
                // movingShapeの始点がaccessLineと接続しているという情報を持っている場合
                if (movingShape.Start.AccessLine.Contains(accessLine))
                {
                    // accessLineの始点がmovingShapeと接続しているという情報を持っている場合
                    if (accessLine.Start.AccessLine.Contains(movingShape))
                    {
                        // 座標が一致しない場合
                        if (movingShape.Start.Point != accessLine.Start.Point)
                        {
                            movingShape.Start.AccessLine.Remove(accessLine);
                            accessLine.Start.AccessLine.Remove(movingShape);
                        }
                    }

                    // accessLineの終点がmovingShapeと接続しているという情報を持っている場合
                    if (accessLine.End.AccessLine.Contains(movingShape))
                    {
                        // 座標が一致しない場合
                        if (movingShape.Start.Point != accessLine.End.Point)
                        {
                            movingShape.Start.AccessLine.Remove(accessLine);
                            accessLine.End.AccessLine.Remove(movingShape);
                        }
                    }
                }

                // movingShapeの終点がaccessLineと接続しているという情報を持っている場合
                if (movingShape.End.AccessLine.Contains(accessLine))
                {
                    // accessLineの始点がmovingShapeと接続しているという情報を持っている場合
                    if (accessLine.Start.AccessLine.Contains(movingShape))
                    {
                        // 座標が一致しない場合
                        if (movingShape.End.Point != accessLine.Start.Point)
                        {
                            movingShape.End.AccessLine.Remove(accessLine);
                            accessLine.Start.AccessLine.Remove(movingShape);
                        }
                    }

                    // accessLineの終点がmovingShapeと接続しているという情報を持っている場合
                    if (accessLine.End.AccessLine.Contains(movingShape))
                    {
                        // 座標が一致しない場合
                        if (movingShape.End.Point != accessLine.End.Point)
                        {
                            movingShape.End.AccessLine.Remove(accessLine);
                            accessLine.End.AccessLine.Remove(movingShape);
                        }
                    }
                }
            }
        }

        public Connector SourceConnector
        {
            get { return sourceConnector; }
            set
            {
                if (sourceConnector != value)
                {
                    sourceConnector = value;
                    connectorsHit.Add(sourceConnector);
                    FullyCreatedConnectorInfo sourceDataItem = sourceConnector.DataContext as FullyCreatedConnectorInfo;


                    Rect rectangleBounds = sourceConnector.TransformToVisual(this).TransformBounds(new Rect(sourceConnector.RenderSize));
                    Point point = new Point(rectangleBounds.Left + (rectangleBounds.Width / 2),
                                            rectangleBounds.Bottom + (rectangleBounds.Height / 2));
                    partialConnection = new ConnectorViewModel(sourceDataItem, new PartCreatedConnectionInfo(point));
                    sourceDataItem.DataItem.Parent.AddItemCommand.Execute(partialConnection);
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                ////measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            ////add margin
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        private void HitTesting(Point hitPoint)
        {
            DependencyObject hitObject = this.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                    hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    if (!connectorsHit.Contains(hitObject as Connector))
                        connectorsHit.Add(hitObject as Connector);
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null)
            {
                (DataContext as IDiagramViewModel).ClearSelectedItemsCommand.Execute(null);
                Point position = e.GetPosition(this);
                DesignerItemViewModelBase itemBase = (DesignerItemViewModelBase)Activator.CreateInstance(dragObject.ContentType);
                if (itemBase is PathDesignerItemViewModel)
                {
                    var shape = (PathDesignerItemViewModel)itemBase;
                    shape.Start = new LinePoint(position.X - 50, position.Y, shape);
                    shape.End = new LinePoint(position.X, position.Y, shape);
                    shape.Center = null;
                    shape.Type = "PathDesignerItemViewModel";
                    shape.ParentPathName = $"Line{lineNumber}";
                    lineNumber++;
                }
                else if (itemBase is CircleDesignerItemViewModel)
                {
                    var shape = (CircleDesignerItemViewModel)itemBase;
                    shape.Start = new LinePoint(position.X + 50, position.Y, shape);
                    shape.End = new LinePoint(position.X, position.Y + 50, shape);
                    shape.Center = new LinePoint(position.X, position.Y, shape);
                    shape.CenterRangeStart = new LinePoint();
                    shape.CenterRangeEnd = new LinePoint();
                    shape.Radius = new Size(50, 50);
                    shape.CenterFlag = false;
                    shape.Type = "CircleDesignerItemViewModel";
                    shape.ParentPathName = $"Circle{circleNumber}";
                    circleNumber++;
                }
                else
                {
                    itemBase.Left = Math.Max(0, position.X - itemBase.ItemWidth / 2);
                    itemBase.Top = Math.Max(0, position.Y - itemBase.ItemHeight / 2);
                }
                itemBase.IsSelected = true;
                (DataContext as IDiagramViewModel).AddItemCommand.Execute(itemBase);
            }
            e.Handled = true;
        }
    }
}
