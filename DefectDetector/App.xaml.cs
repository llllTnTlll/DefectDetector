using DefectDetector.ViewModels.components;
using DefectDetector.ViewModels.mainViews;
using DefectDetector.Views;
using DefectDetector.Views.mainViews;
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
            containerRegistry.RegisterForNavigation<ChartView>("ChartView");
            containerRegistry.RegisterForNavigation<SettingView>("SettingView");
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register("LoginInputRegion", typeof(LoginInputRegionViewModel));
            ViewModelLocationProvider.Register("MenuList", typeof(MenuListViewModel));
            ViewModelLocationProvider.Register("ChartView", typeof(ChartViewModel));
            ViewModelLocationProvider.Register("SettingViewModel", typeof(SettingViewModel));
        }
    }
}
