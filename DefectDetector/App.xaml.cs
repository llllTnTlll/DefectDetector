using DefectDetector.ViewModels;
using DefectDetector.ViewModels.components;
using DefectDetector.ViewModels.mainViews;
using DefectDetector.Views;
using DefectDetector.Views.components;
using DefectDetector.Views.mainViews;
using DefectDetector.Views.toolBars;
using Prism.Ioc;
using Prism.Mvvm;
using System.Windows;

namespace DefectDetector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }  

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScreenSaver>("ScreenSaver");
            containerRegistry.RegisterForNavigation<ChartView>("ChartView");
            containerRegistry.RegisterForNavigation<ChartViewBar>("ChartViewBar");
            containerRegistry.RegisterForNavigation<SettingView>("SettingView");
            containerRegistry.RegisterForNavigation<HistoryView>("HistoryView");
            containerRegistry.RegisterForNavigation<PLCView>("PLCView");

            containerRegistry.RegisterDialog<SaveReminder, SaveReminderViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register("LoginInputRegion", typeof(LoginInputRegionViewModel));
            ViewModelLocationProvider.Register("MenuList", typeof(MenuListViewModel));
            ViewModelLocationProvider.Register("ChartView", typeof(MainWindowViewModel));
            ViewModelLocationProvider.Register("ChartViewBar", typeof(MainWindowViewModel));
            ViewModelLocationProvider.Register("ScreenSaver", typeof(ScreenSaverViewModel));
        }
    }
}
