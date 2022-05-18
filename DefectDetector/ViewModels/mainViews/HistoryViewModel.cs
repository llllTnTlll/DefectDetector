using DefectDetector.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.ViewModels.mainViews
{
    public class HistoryViewModel:BindableBase
    {
        public ObservableCollection<HistoryItemModel> HistoryItems { get; set; }

        public HistoryViewModel()
        {
            HistoryItems = new ObservableCollection<HistoryItemModel>();
            ObservableCollection<BoxItem> boxes = new ObservableCollection<BoxItem>();
            BoxItem box1 = new BoxItem() { ClsId = 1, Height = 10, Left = 10, Index = 0, Top = 10, Width = 10 };
            BoxItem box2 = new BoxItem() { ClsId = 2, Height = 10, Left = 10, Index = 0, Top = 10, Width = 10 };
            boxes.Add(box1);
            boxes.Add(box2);
            HistoryItems.Add(new HistoryItemModel() { ItemId = 0, EntryTime = DateTime.Now, Inspector = "张勇", Boxes = boxes });
        }
    }
}
