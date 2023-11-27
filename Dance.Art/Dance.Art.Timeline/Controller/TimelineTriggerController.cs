using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线触发控制器
    /// </summary>
    public class TimelineTriggerController : DanceObject
    {
        /// <summary>
        /// 时间线触发器控制器
        /// </summary>
        /// <param name="vm">文档视图模型</param>
        public TimelineTriggerController(TimelineDocumentViewModel vm)
        {
            this.ViewModel = vm;
        }

        // ==========================================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        /// <summary>
        /// 视图模型
        /// </summary>
        public TimelineDocumentViewModel ViewModel { get; private set; }

        // ==========================================================================================================
        // Public

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            if (this.ViewModel.View is not TimelineDocumentView view || this.ViewModel.ViewPluginModel is not DocumentPluginModel document)
                return;

            this.OutputManager.WriteLine($"==============================================================");
            this.OutputManager.WriteLine($"[{Path.GetFileName(document.File)}] 开始播放");
            this.OutputManager.WriteLine($"==============================================================");

            TimeSpan currentTime = view.timeline.CurrentTime;

            foreach (TimelineTrackModel trackModel in this.ViewModel.Tracks)
            {
                foreach (TimelineElementModelBase element in trackModel.Items)
                {
                    element.IsTriggeiedBegin = element.BeginTime < currentTime;
                    element.IsTriggeiedEnd = element.EndTime < currentTime;
                }
            }

            this.InvokeTrigger();

            this.ViewModel.IsPlaying = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (this.ViewModel.ViewPluginModel is not DocumentPluginModel document)
                return;

            this.ViewModel.IsPlaying = false;

            this.OutputManager.WriteLine($"==============================================================");
            this.OutputManager.WriteLine($"[{Path.GetFileName(document.File)}] 停止播放");
            this.OutputManager.WriteLine($"==============================================================");
        }

        /// <summary>
        /// 执行触发
        /// </summary>
        public void InvokeTrigger()
        {
            if (!this.ViewModel.IsPlaying || this.ViewModel.View is not TimelineDocumentView view)
                return;

            TimeSpan currentTime = view.timeline.CurrentTime;

            foreach (TimelineTrackModel trackModel in this.ViewModel.Tracks)
            {
                foreach (TimelineElementModelBase element in trackModel.Items)
                {
                    if (element.BeginTime <= currentTime && !element.IsTriggeiedBegin)
                    {
                        try
                        {
                            this.OutputManager.WriteLine($"[ID: {element.ID}, Content: {element.Content}] 开始");
                            element.IsTriggeiedBegin = true;
                            element.OnBegin();
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }

                    if (element.EndTime <= currentTime && !element.IsTriggeiedEnd)
                    {
                        try
                        {
                            this.OutputManager.WriteLine($"[ID: {element.ID}, Content: {element.Content}] 结束");
                            element.IsTriggeiedEnd = true;
                            element.OnEnd();
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }
                }
            }
        }
    }
}
