using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 属性视图模型
    /// </summary>
    public class PropertyViewModel : PanelViewModelBase
    {
        public PropertyViewModel()
        {
            // 命令
            this.LoadedCommand = new(this.Loaded);

            // 消息
            DanceDomain.Current.Messenger.Register<PropertySelectedChangedMessage>(this, this.OnPropertySelectedChanged);
        }

        // =========================================================================================
        // Propery

        #region SelectedObject -- 当前选中对象

        private object? selectedObject;
        /// <summary>
        /// 当前选中对象
        /// </summary>
        public object? SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; this.OnPropertyChanged(); }
        }

        #endregion

        // =========================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private void Loaded()
        {
            this.SelectedObject = ArtDomain.Current.CurrentSelectedObject;
        }

        #endregion

        // =========================================================================================
        // Message

        #region PropertySelectedChangedMessage -- 属性选择改变消息

        /// <summary>
        /// 属性选择改变消息
        /// </summary>
        private void OnPropertySelectedChanged(object sender, PropertySelectedChangedMessage msg)
        {
            this.SelectedObject = msg.SelectedObject;
        }

        #endregion
    }
}
