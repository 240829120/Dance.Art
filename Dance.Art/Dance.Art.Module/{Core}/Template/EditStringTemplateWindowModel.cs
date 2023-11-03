using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 修改字符串模板窗口
    /// </summary>
    public class EditStringTemplateWindowModel : DanceViewModel
    {
        public EditStringTemplateWindowModel()
        {
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // ======================================================================================
        // Property

        /// <summary>
        /// 执行方法
        /// </summary>
        public Func<EditStringTemplateWindowModel, bool>? ExecuteFunc { get; set; }

        #region Data -- 数据

        private object? data;
        /// <summary>
        /// 数据
        /// </summary>
        public object? Data
        {
            get { return data; }
            set { data = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region OldLabel -- 原始标签

        private string? oldLabel;
        /// <summary>
        /// 原始标签
        /// </summary>
        public string? OldLabel
        {
            get { return oldLabel; }
            set { oldLabel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region NewLabel -- 新标签

        private string? newLabel;
        /// <summary>
        /// 新标签
        /// </summary>
        public string? NewLabel
        {
            get { return newLabel; }
            set { newLabel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region OldValue -- 原始值

        private string? oldValue;
        /// <summary>
        /// 原始值
        /// </summary>
        public string? OldValue
        {
            get { return oldValue; }
            set { oldValue = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region NewValue -- 新值

        private string? newValue;
        /// <summary>
        /// 新值
        /// </summary>
        public string? NewValue
        {
            get { return newValue; }
            set { newValue = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ======================================================================================
        // Command

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private void Enter()
        {
            try
            {
                if (this.View is not Window window)
                    return;

                if (!(this.ExecuteFunc?.Invoke(this) ?? true))
                    return;

                window.DialogResult = true;
                window.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (this.View is not Window window)
                return;

            window.DialogResult = false;
            window.Close();
        }

        #endregion
    }
}
