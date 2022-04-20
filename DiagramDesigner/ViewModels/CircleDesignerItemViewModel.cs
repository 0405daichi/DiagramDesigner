using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace DiagramDesigner
{
    public class CircleDesignerItemViewModel : DesignerItemViewModelBase
    {
        private double _setRadius;
        private double _digree;
        private Size _radius;
        private bool _centerFlag;

        public LinePoint CenterRangeStart { get; set; }
        public LinePoint CenterRangeEnd { get; set; }
        public string CircleName { get; set; }

        public bool CenterFlag
        {
            get { return _centerFlag; }
            set
            {
                if (_centerFlag == value)
                {
                    return;
                }
                _centerFlag = value;
                NotifyChanged("CenterFlag");
            }
        }

        public double SetRadius
        {
            get { return _setRadius; }
            set
            {
                if (_setRadius == value)
                {
                    return;
                }
                _setRadius = value;
                NotifyChanged("SetRadius");
            }
        }

        public double Digree
        {
            get { return _digree; }
            set
            {
                if (_digree == value)
                {
                    return;
                }
                _digree = value;
                NotifyChanged("Digree");
            }
        }

        public Size Radius
        {
            get { return _radius; }
            set
            {
                if (_radius == value)
                {
                    return;
                }
                _radius = value;
                NotifyChanged("Radius");
            }
        }
    }
}
