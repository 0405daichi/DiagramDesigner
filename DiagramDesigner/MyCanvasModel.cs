using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner
{
    public class MyCanvasModel
    {
        public MyCanvasModel()
        {
            Lines = new List<MyLine>();
            Connectors = new List<MyConnector>();
        }

        private BaseShape _currentElement;
        public BaseShape CurrentElement
        {
            get
            {
                return _currentElement;
            }
            set
            {
                Connectors.ForEach(s => s.IsSelected_ = false);

                _currentElement = value;
                if (value != null)
                    _currentElement.IsSelected_ = true;
            }
        }

        public Guid SelectedId
        {
            set
            {
                if (Connectors.Any(s => s.Id_ == value)) CurrentElement = Connectors.First(s => s.Id_ == value);
                else CurrentElement = null;
            }
        }

        public List<MyLine> Lines { get; set; }
        public List<MyConnector> Connectors { get; set; }
    }
}
