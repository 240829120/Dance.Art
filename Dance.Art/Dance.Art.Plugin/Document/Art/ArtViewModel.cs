using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Plugin.Document;
using Dance.Art.Storage;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// .art项目文件视图模型
    /// </summary>
    public class ArtViewModel : ControlDocumentViewModelBase
    {
        // ==========================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        protected readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ==========================================================================================
        // Property

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set
            {
                name = value; this.OnPropertyChanged();
                this.isModify = true;
                this.UdateDocumentStatus();
            }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                this.OnPropertyChanged();

                this.isModify = true;
                this.UdateDocumentStatus();
            }
        }

        #endregion

        // ==========================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            if (DanceDomain.Current is not ArtDomain artDomain || artDomain.ProjectDomain == null)
                return;

            if (string.IsNullOrWhiteSpace(artDomain.ProjectDomain.ProjectFilePath))
                return;

            ProjectNode? projectNode = DanceFileHelper.ReadJson<ProjectNode>(artDomain.ProjectDomain.ProjectFilePath);
            if (projectNode == null)
                return;

            this.Name = projectNode.Name;
            this.Description = projectNode.Description;

            this.isModify = false;
            this.UdateDocumentStatus();
        }

        #endregion

        #region SaveCommand -- 保存命令

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            if (DanceDomain.Current is not ArtDomain artDomain || artDomain.ProjectDomain == null)
                return;

            if (string.IsNullOrWhiteSpace(artDomain.ProjectDomain.ProjectFilePath))
                return;

            ProjectNode? projectNode = DanceFileHelper.ReadJson<ProjectNode>(artDomain.ProjectDomain.ProjectFilePath);
            if (projectNode == null)
                return;

            projectNode.Name = this.Name;
            projectNode.Description = this.Description;


            this.FileManager.SaveFile(artDomain.ProjectDomain.ProjectFilePath, () =>
            {
                DanceFileHelper.WriteJson(projectNode, artDomain.ProjectDomain.ProjectFilePath);
            });

            this.isModify = false;
            this.UdateDocumentStatus();
        }

        #endregion
    }

}