using DefectDetector.Common;
using DefectDetector.Helper;
using DefectDetector.Model;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.ViewModels.mainViews
{
    public class HistoryViewModel:BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        // 左侧历史列表数据源
        public ObservableCollection<HistoryItemModel> HistoryItems { get; set; }

        // 当前被选中的历史信息块
        private HistoryItemModel _selectedItem;
        public HistoryItemModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public HistoryViewModel(IEventAggregator eventAggregator)
        {
            // 订阅列表选中事件
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<HistorySelectedEvent>().Subscribe((item) =>
            {
                SelectedItem = item;
            });

            HistoryItems = new ObservableCollection<HistoryItemModel>();

            // 还原预制棒检测信息
            string sql1 = "SELECT * FROM PreformInfo";
            SqlDataReader reader1 = SQLhelper.ExecuteReader(sql1,1);
            while (reader1.Read())
            {
                int preformId = (int)reader1["PreformId"];
                bool isMarked = (bool)reader1["IsMarked"];
                DateTime detectionTime = (DateTime)reader1["DetectionTime"];
                string _operator = (string)reader1["Operator"];

                // 还原缺陷信息
                ObservableCollection<BoxItem> boxItems = new ObservableCollection<BoxItem>();
                string sql2 = String.Format("SELECT * FROM DefectInfo WHERE PreformId = {0}", preformId);
                SqlDataReader reader2 = SQLhelper.ExecuteReader(sql2, 1);
                while (reader2.Read())
                {
                    BoxItem item = new BoxItem();
                    item.ClsId = (int)reader2["DefectId"];
                    item.Left = (double)reader2["BoxLeft"];
                    item.Top = (double)reader2["BoxTop"];
                    item.Width = (double)reader2["BoxWidth"];
                    item.Height = (double)reader2["BoxHeight"];
                    boxItems.Add(item);
                }

                HistoryItemModel historyItem = new HistoryItemModel()
                {
                    PreformId = preformId,
                    IsMarked = isMarked,
                    DetectionTime = detectionTime,
                    Operator = _operator,
                    Boxes = boxItems
                };
                HistoryItems.Add(historyItem);

            }


            
        }
    }
}
