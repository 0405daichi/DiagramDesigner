using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiagramDesigner
{
    public abstract class BaseShape : DesignerItemViewModelBase
    {
        protected FrameworkElement Element;
        protected bool _isSelected;
        protected Guid _id;
        protected Brush _defaultBrush;

        public Guid Id_
        {
            get { return _id; }
            set
            {
                _id = value;
                Element.Tag = _id;
            }
        }

        public FrameworkElement GetShape()
        {
            return Element;
        }

        protected abstract void UpdateDependantsGeometry(double dx, double dy);

        protected double _left;
        public double Left_
        {
            get { return Canvas.GetLeft(Element); }
            set
            {
                var dx = value - Left_;
                Canvas.SetLeft(Element, value);
                _left = value;
                UpdateDependantsGeometry(dx, 0);
            }
        }

        protected double _top;
        public double Top_
        {
            get { return Canvas.GetTop(Element); }
            set
            {
                var dy = value - Top_;
                Canvas.SetTop(Element, value);
                _top = value;
                UpdateDependantsGeometry(0, dy);
            }
        }

        public double Width
        {
            get { return Element.Width; }
            set { Element.Width = value; }
        }


        public double Height
        {
            get { return Element.Height; }
            set { Element.Height = value; }
        }

        protected abstract void ChangeSelection();
        public bool IsSelected_
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                ChangeSelection();
            }
        }
    }
}
