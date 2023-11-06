using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dance.Art.Panel
{
    /// <summary>
    /// FileSourceRenameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSourceRenameWindow : Window
    {
        public FileSourceRenameWindow()
        {
            InitializeComponent();
        }

        public FileSourceRenameWindow(FileModel fileModel) : this()
        {
            FileSourceRenameWindowModel vm = new()
            {
                View = this,
                FileModel = fileModel,
                FileName = fileModel.FileName,
                NewFileName = fileModel.FileName
            };
            this.DataContext = vm;
        }
    }
}
