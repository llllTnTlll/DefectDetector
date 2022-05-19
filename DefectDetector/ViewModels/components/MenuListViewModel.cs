using DefectDetector.Common;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DefectDetector.ViewModels.components
{
    public class MenuListViewModel: BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;
        private IEventAggregator _eventAggregator;
        public MenuListViewModel(IRegionManager regionManager, IRegionNavigationJournal journal, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _journal = journal;
            _eventAggregator = eventAggregator;

            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            MenuList = new ObservableCollection<MenuBar>();
            MenuList.Add(new MenuBar() { IconName= "ChartAreasplineVariant", IsSelect=false, ViewName="ChartView"});
            MenuList.Add(new MenuBar() { IconName = "DesktopClassic", IsSelect = false, ViewName="HistoryView"});
            MenuList.Add(new MenuBar() { IconName = "VideoInputComponent", IsSelect = false, ViewName = "PLCView" });
            MenuList.Add(new MenuBar() { IconName = "Cog", IsSelect = false, ViewName = "SettingView" });
        }

        private ObservableCollection<MenuBar> _menuList;

        public ObservableCollection<MenuBar> MenuList
        {
            get { return _menuList; }
            set { _menuList = value; }
        }

        private string lastNavigation;
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ViewName)) 
                return;

            // 发布选择变化事件
            _eventAggregator.GetEvent<MenuListSlectionChanged>().Publish(obj.ViewName);

            // 若检测到离开SettingView
            // 通知其ViewModel提醒保存
            if(lastNavigation == "SettingView")
            {
               _eventAggregator.GetEvent<SaveReminderEvent>().Publish();
            }

            _regionManager.RequestNavigate("MainRegion", obj.ViewName, callback);
            lastNavigation = obj.ViewName;

        }

        private void callback(NavigationResult obj)
        {
            _journal = obj.Context.NavigationService.Journal;
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; set; }

    }
}
