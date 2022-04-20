using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows;
using System.ComponentModel;

namespace DiagramDesignerApp
{
    public class MainWindowViewModel : INPCBase
    {
        private SDiagramViewModel diagramViewModel;
        public MainWindowViewModel()
        {
            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new SDiagramViewModel();
            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();
        }

        public ToolBoxViewModel ToolBoxViewModel { get; private set; }

        public SDiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel == value)
                {
                    return;
                }

                diagramViewModel = value;
                NotifyChanged("DiagramViewModel");
            }
        }
    }
}
