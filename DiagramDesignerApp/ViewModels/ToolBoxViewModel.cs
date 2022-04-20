using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using DiagramDesigner.Helpers;

namespace DiagramDesignerApp
{
    public class ToolBoxViewModel
    {
        private List<ToolBoxData> toolBoxItems = new List<ToolBoxData>();

        public ToolBoxViewModel()
        {
            toolBoxItems.Add(new ToolBoxData("../Images/humanImage.png", typeof(HumanDesignerItemViewModel)));
            toolBoxItems.Add(new ToolBoxData("../Images/pinImage.png", typeof(PinDesignerItemViewModel)));
            toolBoxItems.Add(new ToolBoxData("../Images/lineImage.png", typeof(PathDesignerItemViewModel)));
            toolBoxItems.Add(new ToolBoxData("../Images/curveImage.png", typeof(CircleDesignerItemViewModel)));
        }

        public List<ToolBoxData> ToolBoxItems
        {
            get { return toolBoxItems; }
        }
    }
}
