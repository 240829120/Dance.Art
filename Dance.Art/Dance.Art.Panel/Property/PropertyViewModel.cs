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
using System.Windows.Interop;

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
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);
        }

        /// <summary>
        /// 属性列表编辑器管理器
        /// </summary>
        private readonly IPropertyGridEditorManager PropertyGridEditorManager = DanceDomain.Current.LifeScope.Resolve<IPropertyGridEditorManager>();

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
            if (this.View is not PropertyView view)
                return;

            view.propertyGrid.EditorDefinitions.Clear();
            if (ArtDomain.Current.CurrentSelectedObject != null)
            {
                view.propertyGrid.EditorDefinitions.AddRange(this.PropertyGridEditorManager.GetEditorTemplateDefinitions(ArtDomain.Current.CurrentSelectedObject.GetType()));
            }

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
            if (this.View is not PropertyView view)
                return;

            this.SelectedObject = null;
            view.propertyGrid.EditorDefinitions.Clear();
            if (msg.SelectedObject != null)
            {
                view.propertyGrid.EditorDefinitions.AddRange(this.PropertyGridEditorManager.GetEditorTemplateDefinitions(msg.SelectedObject.GetType()));
            }

            this.SelectedObject = msg.SelectedObject;
        }

        #endregion

        #region ProjectClosedMessage -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            this.SelectedObject = null;
        }

        #endregion
    }
}
