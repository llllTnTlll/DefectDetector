using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Model
{
    public class SettingModel:BindableBase
    {
        // 检测完成后自动存入数据库
        private bool _resultAutoSave;
        public bool ResultAutoSave
        {
            get { return _resultAutoSave; }
            set { SetProperty(ref _resultAutoSave, value); }
        }

        // 检测图表网格线显示
        private bool _showChartGrid;
        public bool ShowChartGrid
        {
            get { return _showChartGrid; }
            set { SetProperty(ref _showChartGrid, value); }
        }
    }
}
