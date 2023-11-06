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
    /// FileSourceNewFileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSourceNewFileWindow : Window
    {
        public FileSourceNewFileWindow()
        {
            InitializeComponent();
        }

        public FileSourceNewFileWindow(FileModel fileModel) : this()
        {
            FileSourceNewFileWindowModel vm = new()
            {
                View = this,
                FileModel = fileModel,
                Folder = fileModel.Path,
                FileName = "新建文件"
            };

            this.DataContext = vm;
        }
    }
}
