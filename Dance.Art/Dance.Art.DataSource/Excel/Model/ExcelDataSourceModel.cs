using Dance.Art.Domain;
using Dance.Art.Storage;
using Dance.Wpf;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// Excel数据源模型
    /// </summary>
    public class ExcelDataSourceModel : DanceWrapperModel, IDataSource
    {
        // =====================================================================================
        // Field

        /// <summary>
        /// 数据集仓储
        /// </summary>
        private readonly IDataSourceStorage DataSourceStorage = DanceDomain.Current.LifeScope.Resolve<IDataSourceStorage>();

        /// <summary>
        /// 支持的文件格式
        /// </summary>
        private readonly IReadOnlyList<string> Extensions = new List<string>() { ".xls", ".xlsx" };

        // =====================================================================================
        // Property

        /// <summary>
        /// 关联模型
        /// </summary>
        [NotNull]
        public DataSourceModel? Model { get; set; }

        /// <summary>
        /// 数据集集合
        /// </summary>
        public DanceWrapperCollection<DataSetModel> DataSourceSets { get; } = [];

        #region Path -- 文件路径

        private string? path;
        /// <summary>
        /// 文件路径（相对路径 | 绝对路径）
        /// </summary>
        public string? Path
        {
            get { return path; }
            set { path = value; this.OnWrapperPropertyChanged(); }
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

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<ExcelDataSourceEntity>();
            ExcelDataSourceEntity? entity = collection.FindById(this.Model.SourceID) ?? new();
            entity.Path = this.Path;

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

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<ExcelDataSourceEntity>();
            ExcelDataSourceEntity? entity = collection.FindById(this.Model.SourceID);
            if (entity == null)
                return;

            this.Path = entity.Path;
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

        /// <summary>
        /// 加载数据
        /// </summary>
        public void Load()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
            {
                this.Model.Status = DataSourceStatus.Error;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.Path))
            {
                this.Model.Status = DataSourceStatus.Error;
                return;
            }

            string path = System.IO.Path.IsPathRooted(this.Path) ? this.Path : System.IO.Path.GetFullPath(this.Path, ArtDomain.Current.ProjectDomain.ProjectFolderPath);
            string extension = System.IO.Path.GetExtension(path).ToLower();

            if (!Extensions.Contains(extension) || !File.Exists(path))
            {
                this.Model.Status = DataSourceStatus.Error;
                return;
            }

            this.Model.Status = DataSourceStatus.Waiting;

            Task.Run(() =>
            {
                try
                {
                    IWorkbook workBook;
                    switch (extension)
                    {
                        case ".xls": workBook = new HSSFWorkbook(new FileStream(path, FileMode.Open)); break;
                        case ".xlsx": workBook = new XSSFWorkbook(path); break;
                        default: return;
                    }

                    List<DataSetModel> dataSets = [];
                    foreach (ISheet sheet in workBook)
                    {
                        if (sheet == null)
                            continue;

                        DataSetModel dataSet = new()
                        {
                            Name = sheet.SheetName
                        };

                        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null)
                                continue;

                            for (int j = row.FirstCellNum; j <= row.LastCellNum; j++)
                            {
                                ICell cell = row.GetCell(j);
                                if (cell == null)
                                    continue;

                                DataSetCellModel cellModel = new()
                                {
                                    Row = i,
                                    Column = j,
                                    Value = cell.ToString()
                                };

                                dataSet.Cells.Add(cellModel);
                            }
                        }

                        dataSet.BuildIndex();
                        dataSets.Add(dataSet);
                    }

                    workBook.Dispose();

                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.DataSourceSets.Clear();
                        this.DataSourceSets.AddRange(dataSets);

                        this.Model.Status = DataSourceStatus.Ready;
                    });
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.Model.Status = DataSourceStatus.Error;
                    });
                }
            });
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="name">数据集名称</param>
        /// <returns>数据集</returns>
        public DataSetModel? GetDataSet(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return this.DataSourceSets.FirstOrDefault(p => string.Equals(p.Name, name));
        }
    }
}