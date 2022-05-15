using DefectDetector.Common;
using DefectDetector.Model;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DefectDetector.ViewModels.mainViews
{
    public class SettingViewModel:BindableBase
    {
        private IDialogService _dialogService;
        public SettingViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _dialogService = dialogService;
            eventAggregator.GetEvent<SaveReminderEvent>().Subscribe(SaveReminder);
            AppSettings = GetSettings();
        }

        private void SaveReminder()
        {
            var properties = typeof(SettingModel).GetProperties();
            bool shouldAsk = false;
            foreach (System.Reflection.PropertyInfo info in properties)
            {
                var property = AppSettings.GetType().GetProperty(info.Name);
                var myset = property.GetValue(AppSettings);
                // 若当前设置内容与配置文件中不符
                if ((bool)property.GetValue(AppSettings)!= (bool)DetectorSettings.Default[info.Name])
                {
                    shouldAsk = true;
                }
            }
            if (shouldAsk)
            {
                _dialogService.ShowDialog("SaveReminder",callback);
            }
        }

        private void callback(IDialogResult obj)
        {
            var result = obj.Result;
            if(result == ButtonResult.OK)
            {
                SetSettings();
            }
            if(result == ButtonResult.Cancel)
            {
                AppSettings =  GetSettings();
            }
        }

        /// <summary>
        /// 从DetetorSettings中读取程序设置
        /// </summary>
        /// <returns></returns>
        private SettingModel GetSettings()
        {
            SettingModel settings = new SettingModel();
            var properties = typeof(SettingModel).GetProperties();
            foreach (System.Reflection.PropertyInfo info in properties)
            {
                var property = settings.GetType().GetProperty(info.Name);
                property.SetValue(settings, DetectorSettings.Default[info.Name]);
            }
            return settings;
        }


        /// <summary>
        /// 保存设置修改
        /// </summary>
        private void SetSettings()
        {
            var properties = typeof(SettingModel).GetProperties();
            foreach (System.Reflection.PropertyInfo info in properties)
            {
                var property = AppSettings.GetType().GetProperty(info.Name);
                DetectorSettings.Default[info.Name] = property.GetValue(AppSettings);
            }
            DetectorSettings.Default.Save();
        }

        // 存储属性对象
        private SettingModel _appSettings;
        public SettingModel AppSettings
        {
            get { return _appSettings; }
            set { SetProperty(ref _appSettings, value); }
        }
    }
}
