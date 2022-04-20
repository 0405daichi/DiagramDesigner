using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramDesigner
{
    public class MyConnector : BaseShape
    {
        private Brush _selectedBrush = new SolidColorBrush(Colors.HotPink);

        public MyConnector()
        {
            _defaultBrush = new SolidColorBrush(Colors.Black);

            var shape = new Ellipse();
            shape.Width = 10;
            shape.Height = 10;
            shape.Fill = _defaultBrush;

            Element = shape;

            Canvas.SetLeft(Element, 0);
            Canvas.SetTop(Element, 0);

            Id_ = Guid.NewGuid();
        }

        protected override void ChangeSelection()
        {
            (Element as Shape).Fill = _isSelected ? _selectedBrush : _defaultBrush;
        }

        public Point CenterPoint { get { return new Point(Left_ + Width / 2, Top_ + Height / 2); } }

        protected override void UpdateDependantsGeometry(double dx, double dy)
        {
            Line line;
            if (_startLine != null)
            {
                line = _startLine.GetShape() as Line;
                line.X1 = CenterPoint.X;
                line.Y1 = CenterPoint.Y;
            }

            if (_endLine != null)
            {
                line = _endLine.GetShape() as Line;
                line.X2 = CenterPoint.X;
                line.Y2 = CenterPoint.Y;
            }
        }

        private MyLine _startLine;
        private MyLine _endLine;

        public void AddLine(MyLine line, bool isStart)
        {
            if (isStart)
                _startLine = line;
            else
                _endLine = line;
        }
    }
}
