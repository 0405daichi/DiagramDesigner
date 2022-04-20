using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramDesignerApp
{
    public interface IUIVisualizerService
    {
        bool? ShowDialog(object dataContextForPopup);
    }
}
