using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档列表
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DocumentWrapperCollection<T> : DanceWrapperCollection<T>
    {
        /// <summary>
        /// 所属文档
        /// </summary>
        public IDocumentViewModel? OwnerDocument { get; set; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                base.OnCollectionChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                base.OnCollectionChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                base.OnCollectionChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

            });
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                base.OnPropertyChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                base.OnPropertyChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                base.OnPropertyChanged(e);

                if (this.OwnerDocument != null)
                {
                    this.OwnerDocument.IsModify = true;
                }

            });
        }
    }
}
