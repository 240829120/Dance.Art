using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping插件信息
    /// </summary>
    public class PingPluginInfo : ConnectionPluginInfoBase
    {
        /// <summary>
        /// 连接插件信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="editViewType">编辑器视图类型</param>
        public PingPluginInfo(string id, string name, Type editViewType) : base(id, name, editViewType)
        {

        }

        /// <summary>
        /// 循环管理器
        /// </summary>
        private readonly IDanceLoopManager LoopManager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();

        /// <summary>
        /// 创建连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Create(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingEntity>();
            PingEntity? entity = collection.FindById(model.SourceID);
            if (entity == null)
            {
                entity = new();
                collection.Insert(entity);
            }

            PingSourceModel source = new()
            {
                ID = entity.ID,
                Host = entity.Host,
                Frequency = entity.Frequency
            };

            model.SourceID = entity.ID;
            model.Source = source;
        }

        /// <summary>
        /// 删除连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Delete(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null || model.SourceID <= 0)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingEntity>();
            collection.Delete(model.SourceID);
        }

        /// <summary>
        /// 初始化连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Initialize(ConnectionModel model)
        {
            if (model.Source == null)
                this.Create(model);

            if (model.Source is not PingSourceModel sourceModel)
                return;

            if (sourceModel.Ping == null)
                sourceModel.Ping = new();

            this.LoopManager.Register($"PingPluginInfo__{sourceModel.ID}", sourceModel.Frequency / 1000d, () =>
            {
                if (sourceModel.PingTask != null || string.IsNullOrWhiteSpace(sourceModel.Host))
                    return;

                sourceModel.PingTask = Task.Run(async () =>
                {
                    var result = await sourceModel.Ping.SendPingAsync(sourceModel.Host);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        model.Status = result.Status == System.Net.NetworkInformation.IPStatus.Success ? ConnectionStatus.Connected : ConnectionStatus.Disconnected;
                    });

                    sourceModel.PingTask = null;
                });
            });
        }

        /// <summary>
        /// 销毁连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Destory(ConnectionModel model)
        {
            if (model.Source is not PingSourceModel sourceModel)
                return;

            this.LoopManager.UnRegister($"PingPluginInfo__{sourceModel.ID}");
        }
    }
}