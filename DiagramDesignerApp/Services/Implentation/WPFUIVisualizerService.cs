using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiagramDesignerApp
{
    public class WPFUIVisualizerService : IUIVisualizerService
    {
        public bool? ShowDialog(object dataContextForPopup)
        {
            Window win = new PopupWindow();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;
            if (win != null)
                return win.ShowDialog();

            return false;
        }
    }
}
