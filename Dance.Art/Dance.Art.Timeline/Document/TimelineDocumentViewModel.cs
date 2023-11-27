using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Document;
using Dance.Art.Document.Document;
using Dance.Art.Domain;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Tls.Crypto;
using SharpVectors.Dom.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 按钮组文档视图模型
    /// </summary>
    public class TimelineDocumentViewModel : ControlDocumentViewModelBase
    {
        public TimelineDocumentViewModel()
        {
            this.Tracks = new DocumentWrapperCollection<TimelineTrackModel>() { OwnerDocument = this };

            // 命令
            this.AddTrackCommand = new(this.AddTrack);
            this.PlayCommand = new(this.Play);
            this.StopCommand = new(this.Stop);
            this.TrackSelectionChangedCommand = new(this.TrackSelectionChanged);
            this.ElementSelectionChangedCommand = new(this.ElementSelectionChanged);
            this.ElementDragBeginCommand = new(this.ElementDragBegin);
            this.ElementDragOverCommand = new(this.ElementDragOver);
            this.ElementDropCommand = new(this.ElementDrop);

            this.DeleteTrackCommand = new(this.DeleteTrack, this.CanDeleteTrack);
            this.DeleteElementCommand = new(this.DeleteElement, this.CanDeleteElement);

            // 消息
            DanceDomain.Current.Messenger.Register<DockingDesignModeChangedMessage>(this, this.OnDockingDesignModeChanged);
        }

        // ====================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        protected readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ====================================================================================
        // Property

        // -------------------------------------------------------------------------

        #region Tracks -- 轨道集合

        /// <summary>
        /// 轨道集合
        /// </summary>
        public DocumentWrapperCollection<TimelineTrackModel> Tracks { get; }

        #endregion

        #region IsPlaying -- 是否正在播放

        private bool isPlaying;
        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CurrentTime -- 当前时间

        private TimeSpan currentTime;
        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsDesignMode -- 是否是设计模式

        private bool isDesignMode = ArtDomain.Current.IsDesignMode;
        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public bool IsDesignMode
        {
            get { return isDesignMode; }
            set { isDesignMode = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region TimelineModel -- 时间线模型

        private TimelineModel? timelineModel;
        /// <summary>
        /// 时间线模型
        /// </summary>
        public TimelineModel? TimelineModel
        {
            get { return timelineModel; }
            set { timelineModel = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ====================================================================================
        // Command

        #region AddTrackCommand -- 添加轨道命令

        /// <summary>
        /// 添加轨道命令
        /// </summary>
        public RelayCommand AddTrackCommand { get; private set; }

        /// <summary>
        /// 添加轨道
        /// </summary>
        private void AddTrack()
        {
            this.Tracks.Add(new TimelineTrackModel() { Name = $"新轨道", OwnerDocument = this });
        }

        #endregion

        #region PlayCommand -- 播放命令

        /// <summary>
        /// 播放命令
        /// </summary>
        public RelayCommand PlayCommand { get; private set; }

        /// <summary>
        /// 播放
        /// </summary>
        private void Play()
        {
            this.IsPlaying = true;

            if (this.View is not TimelineDocumentView view)
                return;

            view.timeline.Focus();
        }

        #endregion

        #region StopCommand -- 停止命令

        /// <summary>
        /// 停止命令
        /// </summary>
        public RelayCommand StopCommand { get; private set; }

        /// <summary>
        /// 停止
        /// </summary>
        private void Stop()
        {
            this.IsPlaying = false;

            if (this.View is not TimelineDocumentView view)
                return;

            view.timeline.Focus();
        }

        #endregion

        #region TrackSelectionChangedCommand -- 轨道选择改变命令

        /// <summary>
        /// 轨道选择改变命令
        /// </summary>
        public RelayCommand<DanceTimelineTrackSelectionChangedEventArgs> TrackSelectionChangedCommand { get; private set; }

        /// <summary>
        /// 轨道选择改变
        /// </summary>
        private void TrackSelectionChanged(DanceTimelineTrackSelectionChangedEventArgs? e)
        {
            if (e == null || this.View is not TimelineDocumentView view)
                return;

            if (e.Track == null || e.Track.DataContext is not TimelineTrackModel trackModel)
                return;

            view.timeline.ClearElementSelection();

            DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, trackModel));
        }

        #endregion

        #region ElementSelectionChangedCommand -- 元素选择改变命令

        /// <summary>
        /// 元素选择改变命令
        /// </summary>
        public RelayCommand<DanceTimelineElementSelectionChangedEventArgs> ElementSelectionChangedCommand { get; private set; }

        /// <summary>
        /// 元素选择改变
        /// </summary>
        private void ElementSelectionChanged(DanceTimelineElementSelectionChangedEventArgs? e)
        {
            if (e == null || this.View is not TimelineDocumentView view)
                return;

            view.timeline.ClearTrackSelection();

            if (e.Elements.FirstOrDefault()?.DataContext is not ITimelineElementModel model || e.Elements.Count == 0)
            {
                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, this.TimelineModel));
            }
            else if (e.Elements.Count == 1)
            {
                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, model));
            }
            else
            {
                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, null));
            }
        }

        #endregion

        #region ElementDragBeginCommand -- 元素拖拽开始命令

        /// <summary>
        /// 元素拖拽开始命令
        /// </summary>
        public RelayCommand<DanceTimelineElementDragBeginEventArgs> ElementDragBeginCommand { get; private set; }

        /// <summary>
        /// 元素拖拽开始
        /// </summary>
        private void ElementDragBegin(DanceTimelineElementDragBeginEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null || e.Element.DataContext is not TimelineElementModelBase model)
                return;

            e.Data = new TimelineDragInfo(model);
        }

        #endregion

        #region ElementDragOverCommand -- 元素拖拽经过命令

        /// <summary>
        /// 元素拖拽经过命令
        /// </summary>
        public RelayCommand<DanceTimelineElementDragEventArgs> ElementDragOverCommand { get; private set; }

        /// <summary>
        /// 元素拖拽经过
        /// </summary>
        private void ElementDragOver(DanceTimelineElementDragEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null || this.View is not TimelineDocumentView view)
                return;

            // 控件内拖拽
            if (e.EventArgs.Data.GetData(typeof(TimelineDragInfo)) is TimelineDragInfo dragInfo)
            {
                e.BeginTime = dragInfo.Model.BeginTime;
                e.EndTime = dragInfo.Model.EndTime;

                return;
            }

            // 资源拖拽
            if (e.EventArgs.Data.GetData(typeof(ResourceInfoItemModel)) is ResourceInfoItemModel resource)
            {
                e.BeginTime = TimeSpan.Zero;
                e.EndTime = view.timeline.GetViewportWidth() / 20;

                return;
            }
        }

        #endregion

        #region ElementDropCommand -- 元素拖拽放置命令

        /// <summary>
        /// 元素拖拽放置命令
        /// </summary>
        public RelayCommand<DanceTimelineElementDragEventArgs> ElementDropCommand { get; private set; }

        /// <summary>
        /// 元素拖拽放置
        /// </summary>
        private void ElementDrop(DanceTimelineElementDragEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null || this.View is not TimelineDocumentView view)
                return;

            if (e.BeginTime == null || e.EndTime == null)
                return;

            if (e.Track == null || e.Track.DataContext is not TimelineTrackModel trackModel)
                return;

            // 控件内拖拽
            if (e.EventArgs.Data.GetData(typeof(TimelineDragInfo)) is TimelineDragInfo dragInfo)
            {
                if (dragInfo.Model.JsonObjectCopy<TimelineElementModelBase>() is not TimelineElementModelBase dest)
                    return;

                dest.BeginTime = e.BeginTime.Value;
                dest.EndTime = e.EndTime.Value;
                dest.OwnerDocument = this;

                trackModel.Items.Add(dest);

                return;
            }

            // 资源拖拽
            if (e.EventArgs.Data.GetData(typeof(ResourceInfoItemModel)) is ResourceInfoItemModel resource)
            {
                if (resource.Source?.CreateInstance(ArtDomain.Current.ProjectDomain) is not TimelineElementModelBase dest)
                    return;

                dest.BeginTime = e.BeginTime.Value;
                dest.EndTime = e.EndTime.Value;
                dest.OwnerDocument = this;

                trackModel.Items.Add(dest);

                return;
            }
        }

        #endregion

        #region DeleteCommand -- 删除轨道命令

        /// <summary>
        /// 删除轨道命令
        /// </summary>
        public RelayCommand DeleteTrackCommand { get; private set; }

        /// <summary>
        /// 是否可以删除轨道
        /// </summary>
        private bool CanDeleteTrack()
        {
            return this.Tracks.Any(p => p.IsSelected);
        }

        /// <summary>
        /// 删除轨道
        /// </summary>
        private void DeleteTrack()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            TimelineTrackModel? trackModel = this.Tracks.FirstOrDefault(p => p.IsSelected);
            if (trackModel == null)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除轨道: {trackModel.Name}", DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL) != DanceMessageBoxAction.YES)
                return;

            this.Tracks.Remove(trackModel);

            this.IsModify = true;
        }

        #endregion

        #region DeleteElementCommand -- 删除元素命令

        /// <summary>
        /// 删除元素命令
        /// </summary>
        public RelayCommand DeleteElementCommand { get; private set; }

        /// <summary>
        /// 是否可以删除元素
        /// </summary>
        private bool CanDeleteElement()
        {
            foreach (TimelineTrackModel trackModel in this.Tracks)
            {
                if (trackModel.Items.Any(p => p.IsSelected))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        private void DeleteElement()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "是否删除选中元素", DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL) != DanceMessageBoxAction.YES)
                return;

            foreach (TimelineTrackModel trackModel in this.Tracks)
            {
                List<TimelineElementModelBase> removeList = trackModel.Items.Where(p => p.IsSelected).ToList();
                removeList.ForEach(p => trackModel.Items.Remove(p));
            }
        }

        #endregion

        // ====================================================================================
        // Message

        #region DockingDesignModeChangedMessage -- 设计模式改变消息

        /// <summary>
        /// 设计模式改变消息
        /// </summary>
        private void OnDockingDesignModeChanged(object sender, DockingDesignModeChangedMessage msg)
        {
            this.IsDesignMode = msg.IsDesignMode;
            if (this.View is not TimelineDocumentView view)
                return;

            view.timeline.ClearTrackSelection();
            view.timeline.ClearElementSelection();
        }

        #endregion

        // ====================================================================================
        // Override

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel is not DocumentPluginModel document)
                return;

            TimelineStorage? storage = DanceFileHelper.ReadJson<TimelineStorage>(document.File, new DanceJsonObjectConverter());

            this.TimelineModel = storage?.TimelineModel ?? new();
            this.TimelineModel.OwnerDocument = this;

            this.Tracks.Clear();
            this.Tracks.AddRange(storage?.Tracks);
            this.Tracks.ForEach(p =>
            {
                p.OwnerDocument = this;
                p.Items.ForEach(i => i.OwnerDocument = this);
            });

            this.IsModify = false;
            this.UdateDocumentStatus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {

            if (this.ViewPluginModel is not DocumentPluginModel document)
                return;

            TimelineStorage storage = new()
            {
                TimelineModel = this.TimelineModel,
                Tracks = this.Tracks.ToList()
            };

            this.FileManager.SaveFile(document.File, () =>
            {
                DanceFileHelper.WriteJson(storage, document.File);
                this.IsModify = false;
                this.UdateDocumentStatus();
            });
        }

        // ====================================================================================
        // Public Function

    }
}
