using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.ComponentModel;

namespace DiagramDesigner
{
    public class PathDesignerItemViewModel : DesignerItemViewModelBase
    {
        public static PathDesignerItemViewModel Deserialize(SerializeItems item)
        {
            PathDesignerItemViewModel obj = new PathDesignerItemViewModel()
            {
                Type = item.Type,
                ParentPathName = item.Name,
                Start = new LinePoint(item.StartPoint.X, item.StartPoint.Y),
                End = new LinePoint(item.EndPoint.X, item.EndPoint.Y),
                Center = null
            };
            obj.Start.Parent = obj;
            obj.End.Parent = obj;
            return obj;
        }
    }
}
