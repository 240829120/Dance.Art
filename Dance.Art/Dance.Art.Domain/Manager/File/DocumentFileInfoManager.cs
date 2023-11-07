using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档文件信息管理器
    /// </summary>
    [DanceSingleton(typeof(IDocumentFileInfoManager))]
    public class DocumentFileInfoManager : IDocumentFileInfoManager
    {
        /// <summary>
        /// 文档文件分组信息集合
        /// </summary>
        public List<DocumentFileGroupInfo> DocumentFileGroupInfos { get; private set; } = new();

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            var query = ArtDomain.Current.GetPluginCollection<DocumentPluginInfo>();

            foreach (DocumentPluginInfo plugin in query)
            {
                foreach (DocumentFileInfo item in plugin.FileInfos)
                {
                    if (!item.IsPublic)
                        continue;

                    DocumentFileGroupInfo? groupInfo = this.DocumentFileGroupInfos.FirstOrDefault(p => p.Name == item.Group);
                    if (groupInfo == null)
                    {
                        groupInfo = new(item.Group);
                        this.DocumentFileGroupInfos.Add(groupInfo);
                    }

                    groupInfo.FileInfos.Add(item);
                }
            }

            this.DocumentFileGroupInfos.SortSelf((a, b) => string.Compare(a.Name, b.Name));
            this.DocumentFileGroupInfos.ForEach(p => p.FileInfos.SortSelf((a, b) => string.Compare(a.Extension, b.Extension)));
        }
    }
}
