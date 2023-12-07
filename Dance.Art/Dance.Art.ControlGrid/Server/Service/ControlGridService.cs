using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板服务
    /// </summary>
    [DanceServiceRoute]
    public class ControlGridService
    {
        /// <summary>
        /// 资源管理器
        /// </summary>
        private readonly IResourceManager ResourceManager = DanceDomain.Current.LifeScope.Resolve<IResourceManager>();

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [DanceServiceRoute]
        public ServerResponse AddItem(ControlGridAddItemRequest request)
        {
            string msg = "添加失败";

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
                    return;

                if (string.IsNullOrWhiteSpace(request.path) || string.IsNullOrWhiteSpace(request.type))
                    return;

                if (ArtDomain.Current.ProjectDomain.GetDocumentViewModel(request.path) is not ControlGridDocumentViewModel vm)
                    return;

                List<ResourceInfoGroupModel>? groups = this.ResourceManager.GetOrCreateResources(FileSuffixCategory.CONTROL_GRID_PANEL);
                if (groups == null)
                    return;

                ResourceInfoItemModel? item = null;
                foreach (ResourceInfoGroupModel group in groups)
                {
                    item = group.Items.FirstOrDefault(p => p.Source != null && p.Source.ID == request.type);
                    if (item != null)
                        break;
                }

                if (item != null && item.Source != null && item.Source.CreateInstance(ArtDomain.Current.ProjectDomain) is IControlGridItemModel model)
                {
                    model.ID = request.id;
                    model.Row = request.row;
                    model.Column = request.column;
                    model.OwnerDocument = vm;

                    vm.Items.Add(model);
                }

                msg = "添加成功";
            });

            return new ServerResponse(msg);
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [DanceServiceRoute]
        public ServerResponse DeleteItem(ControlGridDeleteItemRequest request)
        {
            string msg = "删除失败";

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
                    return;

                if (string.IsNullOrWhiteSpace(request.path) || string.IsNullOrWhiteSpace(request.id))
                    return;

                if (ArtDomain.Current.ProjectDomain.GetDocumentViewModel(request.path) is not ControlGridDocumentViewModel vm)
                    return;

                IControlGridItemModel? item = vm.Items.FirstOrDefault(p => p.ID == request.id);
                if (item == null)
                    return;

                vm.Items.Remove(item);

                msg = "删除成功";
            });

            return new ServerResponse(msg);
        }

        /// <summary>
        /// 移动项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [DanceServiceRoute]
        public ServerResponse MoveItem(ControlGridMoveItemRequest request)
        {
            string msg = "移动失败";

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
                    return;

                if (string.IsNullOrWhiteSpace(request.path) || string.IsNullOrWhiteSpace(request.id))
                    return;

                if (ArtDomain.Current.ProjectDomain.GetDocumentViewModel(request.path) is not ControlGridDocumentViewModel vm)
                    return;

                IControlGridItemModel? item = vm.Items.FirstOrDefault(p => p.ID == request.id);
                if (item == null)
                    return;

                item.Row = request.row;
                item.Column = request.column;

                msg = "移动成功";
            });

            return new ServerResponse(msg);
        }
    }
}
