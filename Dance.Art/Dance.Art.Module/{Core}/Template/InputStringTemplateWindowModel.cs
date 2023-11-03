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
    /// 输入字符串模板窗口模型
    /// </summary>
    public class InputStringTemplateWindowModel : DanceViewModel
    {
        public InputStringTemplateWindowModel()
        {
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // ======================================================================================
        // Property

        /// <summary>
        /// 执行方法
        /// </summary>
        public Func<InputStringTemplateWindowModel, bool>? ExecuteFunc { get; set; }

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

        #region InputLabel -- 输入标签

        private string? inputLabel;
        /// <summary>
        /// 输入标签
        /// </summary>
        public string? InputLabel
        {
            get { return inputLabel; }
            set { inputLabel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region InputValue -- 输入值

        private string? inputValue;
        /// <summary>
        /// 输入值
        /// </summary>
        public string? InputValue
        {
            get { return inputValue; }
            set { inputValue = value; this.OnPropertyChanged(); }
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
