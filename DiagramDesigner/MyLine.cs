using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramDesigner
{
    public class MyLine : DesignerItemViewModelBase
    {
        private MyConnector _startConnector;
        private MyConnector _endConnector;

        public MyConnector GetStartConnector()
        {
            return _startConnector;
        }

        public MyConnector GetEndConnector()
        {
            return _endConnector;
        }

        public MyLine()
        {
            Shape = new Line();
            Shape.Fill = Brushes.Black;
            Shape.Stroke = Brushes.Black;
            Shape.StrokeThickness = 2;

            _startConnector = new MyConnector();
            _startConnector.AddLine(this, true);
            _endConnector = new MyConnector();
            _endConnector.AddLine(this, false);
        }

        protected Shape Shape;

        public Shape GetShape()
        {
            return Shape;
        }
    }
}
