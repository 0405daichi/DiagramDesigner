using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DiagramDesigner
{
    // パスをキャンバス上で扱うための処理に使うパスのリスト
    public class LineItemList
    {
        public LineItemList()
        {
            LineItems = new ObservableCollection<SelectableDesignerItemViewModelBase>();
        }
        public ObservableCollection<SelectableDesignerItemViewModelBase> LineItems { get; }
        public List<SelectableDesignerItemViewModelBase> SelectedLineItems
        {
            get { return LineItems.Where(v => v.IsSelected == true).ToList(); }
        }
        public List<CircleDesignerItemViewModel> Arcs
        {
            get { return LineItems.Where(v => v.Type == "Arc").Cast<CircleDesignerItemViewModel>().ToList(); }
        }
        public List<CircleDesignerItemViewModel> SelectedArcs
        {
            get { return SelectedLineItems.Where(v => v.Type == "Arc").Cast<CircleDesignerItemViewModel>().ToList(); }
        }
    }
}
