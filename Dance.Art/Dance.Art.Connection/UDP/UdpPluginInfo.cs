using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Udp插件信息
    /// </summary>
    public class UdpPluginInfo : ConnectionPluginInfoBase
    {
        /// <summary>
        /// 连接插件信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="editViewType">编辑器视图类型</param>
        /// <param name="sourceModelType">源模型类型</param>
        public UdpPluginInfo(string id, string name, Type editViewType, Type sourceModelType) : base(id, name, editViewType, sourceModelType)
        {

        }

        /// <summary>
        /// 从仓储加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void LoadFromStorage(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null || model.Source is not UdpSourceModel sourceModel)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            UdpSourceEntity? entity = collection.FindById(model.SourceID) ?? new();

            sourceModel.LocalHost = entity.LocalHost;
            sourceModel.LocalPort = entity.LocalPort;
            sourceModel.RemoteHost = entity.RemoteHost;
            sourceModel.RemotePort = entity.RemotePort;
        }

        /// <summary>
        /// 保存至仓储
        /// </summary>
        /// <param name="model">连接模型</param>
        public override int SaveToStorage(ConnectionModel model)
        {
            if (ArtDomain.Current.ProjectDomain == null || model.Source is not UdpSourceModel sourceModel)
                return 0;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            UdpSourceEntity? entity = collection.FindById(model.SourceID) ?? new();

            entity.ID = model.SourceID;
            entity.LocalHost = sourceModel.LocalHost;
            entity.LocalPort = sourceModel.LocalPort;
            entity.RemoteHost = sourceModel.RemoteHost;
            entity.RemotePort = sourceModel.RemotePort;

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

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            collection.Delete(model.SourceID);
        }

        /// <summary>
        /// 初始化连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Initialize(ConnectionModel model)
        {
            if (model.Source is not UdpSourceModel sourceModel || string.IsNullOrWhiteSpace(sourceModel.LocalHost) || string.IsNullOrWhiteSpace(sourceModel.RemoteHost) || sourceModel.LocalPort <= 0 || sourceModel.RemotePort <= 0)
                return;

            if (sourceModel.Client == null)
            {
                IPEndPoint localPoint = new(IPAddress.Parse(sourceModel.LocalHost), sourceModel.LocalPort);
                sourceModel.Client = new(localPoint);
                sourceModel.Client.Connect(sourceModel.RemoteHost, sourceModel.RemotePort);
                sourceModel.BeginReceiveThread();

                model.Status = ConnectionStatus.Connected;
            }
        }

        /// <summary>
        /// 销毁连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Destory(ConnectionModel model)
        {
            if (model.Source is not UdpSourceModel sourceModel)
                return;

            sourceModel.ReceiveThread?.Stop();
            sourceModel.ReceiveThread = null;
            sourceModel.Client?.Dispose();
            sourceModel.Client = null;
        }
    }
}