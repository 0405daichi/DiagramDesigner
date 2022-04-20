using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiagramDesigner;

namespace DiagramDesignerApp
{
    /// <summary>
    /// EditProperty.xaml の相互作用ロジック
    /// </summary>
    public partial class EditProperty : UserControl
    {
        public EditProperty()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StatureProperty =
            DependencyProperty.Register(
                "Stature",
                typeof(double),
                typeof(EditProperty),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty PinProperty =
            DependencyProperty.Register(
                "Pin",
                typeof(string),
                typeof(EditProperty),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.Register(
                "SelectEnable",
                typeof(bool),
                typeof(EditProperty),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Stature
        {
            get { return (double)GetValue(StatureProperty); }
            set { SetValue(StatureProperty, value); }
        }

        public string Pin
        {
            get { return (string)GetValue(PinProperty); }
            set { SetValue(PinProperty, value); }
        }

        public bool SelectEnable
        {
            get { return (bool)GetValue(EnableProperty); }
            set { SetValue(EnableProperty, value); }
        }


    }
}
