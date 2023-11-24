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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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

            this.DeleteCommand = new(this.Delete, this.CanDelete);

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
            if (this.View is not TimelineDocumentView view)
                return;

            Random random = new();

            TimelineTrackModel track = new() { Name = $"轨道" };

            TimeSpan beginTime = TimeSpan.FromSeconds(random.Next(0, (int)TimeSpan.FromMinutes(5).TotalSeconds));

            for (int i = 0; i < 100; ++i)
            {
                TimeSpan endTime = TimeSpan.FromSeconds(random.Next((int)beginTime.TotalSeconds, (int)(beginTime + TimeSpan.FromMinutes(5)).TotalSeconds));

                if (beginTime >= view.timeline.Duration || endTime >= view.timeline.Duration)
                    break;

                ScriptElementModel item = new()
                {
                    BeginTime = beginTime,
                    EndTime = endTime
                };

                track.Items.Add(item);

                beginTime = TimeSpan.FromSeconds(random.Next((int)endTime.TotalSeconds, (int)(beginTime + TimeSpan.FromMinutes(5)).TotalSeconds)); ;
            }

            this.Tracks.Add(track);
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
            if (e == null)
                return;

            if (e.Track == null || e.Track.DataContext is not TimelineTrackModel trackModel)
                return;

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
            if (e == null)
                return;

            ITimelineElementModel? model = e.Elements.FirstOrDefault()?.DataContext as ITimelineElementModel;
            if (model == null || e.Elements.Count != 1)
            {
                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, null));
            }
            else
            {
                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, model));
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

        }

        #endregion

        #region DeleteCommand -- 删除命令

        /// <summary>
        /// 删除命令
        /// </summary>
        public RelayCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool CanDelete()
        {
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive)
                return;


            this.IsModify = true;
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
