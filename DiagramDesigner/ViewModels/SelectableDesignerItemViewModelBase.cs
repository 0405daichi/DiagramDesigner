using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace DiagramDesigner
{

    public interface ISelectItems
    {
        SimpleCommand SelectItemCommand { get;  }
    }

    
    public abstract class SelectableDesignerItemViewModelBase : INPCBase, ISelectItems
    {
        private bool isSelected;

        public SelectableDesignerItemViewModelBase(int id, IDiagramViewModel parent)
        {
            this.Id = id;
            this.Parent = parent;
            Init();
        }

        public SelectableDesignerItemViewModelBase()
        {
            Init();
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {             
            get { return Parent.SelectedItems; }
        }

        public IDiagramViewModel Parent { get; set; }
        public SimpleCommand SelectItemCommand { get; private set; }
        public int Id { get; set; }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    
                    isSelected = value;
                    NotifyChanged("IsSelected");
                }
            }
        }

        private void ExecuteSelectItemCommand(object param)
        {
            SelectItem((bool)param, !IsSelected);
        }
        
        private void SelectItem(bool newselect, bool select)
        {
            if (newselect)
            {
                foreach (var designerItemViewModelBase in Parent.SelectedItems.ToList())
                {
                    designerItemViewModelBase.isSelected = false;
                }
            }

            IsSelected = select;
        }
    
        private void Init()
        {
            SelectItemCommand = new SimpleCommand(ExecuteSelectItemCommand);
        }

        // パスの端同士をスナップさせるためのコードが複雑になるため
        // パスの端の情報はこちらに持たせるようにした
        private LinePoint _start;
        private LinePoint _end;
        private LinePoint _center;
        public LinePoint Start
        {
            get { return _start; }
            set
            {
                if (_start == value)
                {
                    return;
                }

                if(_start != null)
                {
                    _start.PropertyChanged -= CalcCirclesValue;
                }

                _start = value;
                if(!(value is null))
                {
                    _start.PropertyChanged += CalcCirclesValue;
                }
                NotifyChanged("Start");
            }
        }
        public LinePoint End
        {
            get { return _end; }
            set
            {
                if (_end == value)
                {
                    return;
                }

                if (_end != null)
                {
                    _end.PropertyChanged -= CalcCirclesValue;
                }

                _end = value;
                if (!(value is null))
                {
                    _end.PropertyChanged += CalcCirclesValue;
                }
                NotifyChanged("End");
            }
        }
        public LinePoint Center
        {
            get { return _center; }
            set
            {
                if (_center == value)
                {
                    return;
                }

                if (_center != null)
                {
                    _center.PropertyChanged -= CalcCirclesValue;
                }

                _center = value;
                if (!(value is null))
                {
                    _center.PropertyChanged += CalcCirclesValue;
                }
                NotifyChanged("Center");
            }
        }
        // edit type from name to class        
        public string Type { get; set; }
        
        public string ParentPathName { get; set; }

        // 移動し補正された端点もしくは中心点からの半径と描かれる弧の角度算出
        public void CalcCirclesValue(object sender, PropertyChangedEventArgs e)
        {
            var shape = (sender as LinePoint).Parent as CircleDesignerItemViewModel;
            if (shape != null)
            {
                var vX = Start.Point - Center.Point;
                var vY = End.Point - Center.Point;
                shape.Digree = Vector.AngleBetween(vX, vY);
                shape.Radius = new Size(vX.Length, vY.Length);
            }
        }
    }
}
