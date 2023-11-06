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
        /// <param name="sourceModelType">源模型类型</param>
        public PingPluginInfo(string id, string name, Type editViewType, Type sourceModelType) : base(id, name, editViewType, sourceModelType)
        {

        }

        /// <summary>
        /// 循环管理器
        /// </summary>
        private readonly IDanceLoopManager LoopManager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();

        /// <summary>
        /// 从仓储加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void LoadFromStorage(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null || model.Source is not PingSourceModel sourceModel)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingEntity>();
            PingEntity? entity = collection.FindById(model.SourceID) ?? new();

            sourceModel.Host = entity.Host;
            sourceModel.Frequency = entity.Frequency;
        }

        /// <summary>
        /// 保存至仓储
        /// </summary>
        /// <param name="model">连接模型</param>
        public override int SaveToStorage(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null || model.Source is not PingSourceModel sourceModel)
                return 0;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingEntity>();
            PingEntity? entity = collection.FindById(model.SourceID) ?? new();

            entity.ID = model.SourceID;
            entity.Host = sourceModel.Host;
            entity.Frequency = sourceModel.Frequency;

            collection.Upsert(entity);

            return entity.ID;
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
            if (model.Source is not PingSourceModel sourceModel)
                return;

            if (sourceModel.Ping == null)
                sourceModel.Ping = new();

            this.LoopManager.Register($"PingPluginInfo__{model.SourceID}", sourceModel.Frequency / 1000d, () =>
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
            this.LoopManager.UnRegister($"PingPluginInfo__{model.SourceID}");
        }
    }
}