using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows;

namespace DiagramDesigner
{
    public class SerializeItems
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Point CenterPoint { get; set; }
        public List<string> StartPointAccessLines { get; set; }
        public List<string> EndPointAccessLines { get; set; }
    }
}
