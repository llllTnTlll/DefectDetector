using DefectDetector.Model;
using DefectDetector.Views.components;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.ViewModels.components
{
    public class MenuListViewModel: BindableBase
    {
        private IRegionManager _regionManager;
        public MenuListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            MenuList = new ObservableCollection<MenuBar>();
            MenuList.Add(new MenuBar() { IconName= "ChartAreasplineVariant", IsSelect=false, ViewName="ChartView"});
            MenuList.Add(new MenuBar() { IconName = "DesktopClassic", IsSelect = false, ViewName="SettingView"});
            MenuList.Add(new MenuBar() { IconName = "Cog", IsSelect = false, ViewName = "SettingView" });
        }

        private ObservableCollection<MenuBar> _menuList;

        public ObservableCollection<MenuBar> MenuList
        {
            get { return _menuList; }
            set { _menuList = value; }
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ViewName)) 
                return;
            _regionManager.Regions["MainRegion"].RequestNavigate(obj.ViewName);
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; set; }

    }
}
