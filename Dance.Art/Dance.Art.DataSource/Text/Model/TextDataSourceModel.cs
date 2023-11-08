using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// 文本数据源模型
    /// </summary>
    public class TextDataSourceModel : DanceWrapperModel, IDataSource
    {
        // =====================================================================================
        // Field

        // =====================================================================================
        // Property

        /// <summary>
        /// 关联模型
        /// </summary>
        [NotNull]
        public DataSourceModel? Model { get; set; }

        #region Text -- 文本内容

        private string? text;
        /// <summary>
        /// 文本内容
        /// </summary>
        public string? Text
        {
            get { return text; }
            set { text = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // =====================================================================================
        // Public Function

        /// <summary>
        /// 保存至仓储
        /// </summary>
        public void SaveToStorage()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<TextDataSourceEntity>();
            TextDataSourceEntity? entity = collection.FindById(this.Model.SourceID) ?? new();
            entity.Text = this.Text;

            collection.Upsert(entity);
            this.Model.SourceID = entity.ID;
        }

        /// <summary>
        /// 从仓储中获取
        /// </summary>
        public void LoadFromStorage()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<TextDataSourceEntity>();
            TextDataSourceEntity? entity = collection.FindById(this.Model.SourceID);
            if (entity == null)
                return;

            this.Text = entity.Text;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<TextDataSourceEntity>();
            collection.Delete(this.Model.SourceID);
        }
    }
}
