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
    /// FileSourceNewFolderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSourceNewFolderWindow : Window
    {
        public FileSourceNewFolderWindow()
        {
            InitializeComponent();
        }

        public FileSourceNewFolderWindow(FileModel fileModel) : this()
        {
            FileSourceNewFolderWindowModel vm = new()
            {
                View = this,
                FileModel = fileModel,
                FileName = "新建文件夹"
            };

            this.DataContext = vm;
        }
    }
}
