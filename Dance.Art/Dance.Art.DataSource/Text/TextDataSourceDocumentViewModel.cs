using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// Ping文档视图模型
    /// </summary>
    public class TextDataSourceDocumentViewModel : DataSourceDocumentViewModelBase, IDataSourceDocumentViewModel
    {
        // ================================================================================================
        // Property

        #region Text -- 文本

        private string? text;
        /// <summary>
        /// 文本
        /// </summary>
        public string? Text
        {
            get { return text; }
            set { text = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ================================================================================================
        // Override

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            base.Load();

            if (this.Model == null || this.Model.Source is not TextDataSourceModel sourceModel)
                return;

            this.Name = this.Model?.Name;
            this.Description = this.Model?.Description;
            this.Text = sourceModel.Text;
        }

        /// <summary>
        /// 确定
        /// </summary>
        protected override void Enter()
        {
            try
            {
                if (this.Model == null || this.Model.Source is not TextDataSourceModel sourceModel)
                    return;

                if (!this.CheckName())
                    return;

                this.ChangeDocumentTitle();
                this.Model.Name = this.Name;
                this.Model.Description = this.Description;
                sourceModel.Text = this.Text;

                this.SaveDataSourceGroups();
                sourceModel.SaveToStorage();

                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "应用成功", DanceMessageBoxAction.YES);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }
    }
}
