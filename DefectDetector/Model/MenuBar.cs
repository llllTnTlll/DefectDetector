using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Model
{
    public class MenuBar: BindableBase
    {
        private string _iconName;
        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; }
        }

        private string _viewName;
        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        private bool _isSelect;
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

    }
}
