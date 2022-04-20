using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace DiagramDesigner
{
    public class LinePoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Point _point;
        private List<SelectableDesignerItemViewModelBase> _accessLine;
        private SelectableDesignerItemViewModelBase _parent;

        public LinePoint() { }

        public LinePoint(double x, double y, SelectableDesignerItemViewModelBase parent)
        {
            _point = new Point(x, y);
            _accessLine = new List<SelectableDesignerItemViewModelBase>();
            _parent = parent;
        }

        public Point Point
        {
            get { return _point; }
            set
            {
                if (_point == value)
                {
                    return;
                }
                _point = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Point"));
            }
        }

        public List<SelectableDesignerItemViewModelBase> AccessLine
        {
            get { return _accessLine; }
            set
            {
                if (_accessLine == value)
                {
                    return;
                }
                _accessLine = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AccessLine"));
            }
        }

        public SelectableDesignerItemViewModelBase Parent
        {
            get { return _parent; }
            set
            {
                if (_parent == value)
                {
                    return;
                }
                _parent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Parent"));
            }
        }
    }
}
