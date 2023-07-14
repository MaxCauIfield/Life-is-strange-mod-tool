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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个FolderBrowserDialog实例
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            // 设置初始选中的路径
            dialog.SelectedPath = "C:\\";
            // 显示对话框
            DialogResult result = dialog.ShowDialog();
            // 如果用户点击了确定按钮
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // 获取用户选择的路径
                string path = dialog.SelectedPath;
                // 将路径显示在编辑框中
                txtPath1.Text = path;
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            // 创建一个OpenFileDialog实例
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            // 设置过滤器，只显示某些类型的文件
            openFileDialog.Filter = "奇异人生包文件 (*.upk)|*.upk|All files (*.*)|*.*";
            // 设置初始目录，可以是绝对路径或相对路径
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // 设置对话框标题
            openFileDialog.Title = "请选择一个或多个文件";
            // 设置Multiselect属性为true，允许用户选择多个文件
            openFileDialog.Multiselect = true;
            // 显示对话框，并判断用户是否点击了确定按钮
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 获取用户选择的所有文件的完整路径，返回一个字符串数组
                string[] filePaths = openFileDialog.FileNames;
                // 使用换行符作为分隔符，将字符串数组中的元素连接成一个字符串
                string fileText = String.Join("\n", filePaths);
                // 将文件路径插入到指定的编辑框中
                txtPath.Text = fileText;
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // 创建一个OpenFileDialog实例
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            // 设置过滤器，只显示某些类型的文件
            openFileDialog.Filter = "奇异人生验证文件 (*.txt)|*.txt|All files (*.*)|*.*";
            // 设置初始目录，可以是绝对路径或相对路径
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // 设置对话框标题
            openFileDialog.Title = "请选择一个文件";
            // 显示对话框，并判断用户是否点击了确定按钮
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 获取用户选择的文件的完整路径
                string filePath = openFileDialog.FileName;
                // 将文件路径插入到指定的编辑框中
                TxtPach2.Text = filePath;
                // 如果需要，可以使用StreamReader类来读取文件内容，并显示在另一个编辑框中
                // var sr = new StreamReader(filePath);
                // textBox2.Text = sr.ReadToEnd();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Ca1 != null)
            //防止返回空null引发错误
            {
                Ca1.Visibility = Visibility.Visible;
                Lableyz1.Visibility = Visibility.Visible;
                TxtPach2.Visibility = Visibility.Visible;
                buttonyz2.Visibility = Visibility.Visible;
            }
        }
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ca1.Visibility = Visibility.Collapsed;
            Lableyz1.Visibility = Visibility.Collapsed;
            TxtPach2.Visibility = Visibility.Collapsed;
            buttonyz2.Visibility = Visibility.Collapsed;

        }

        private void cai(object sender, RoutedEventArgs e)
        {
            // 获取用户输入的路径
            string path = TxtPach2.Text;
            // 判断路径是否为空或无效
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                System.Windows.MessageBox.Show("请选择游戏验证文件");
                return;
            }
            // 判断路径是否指向一个txt文件
            if (!path.Contains("PCConsoleFINALTOC"))
            {
                System.Windows.MessageBox.Show("转换VERIFY模式时出现错误，所选文件不是有效的《奇异人生》验证文件！");
                return;
            }
            // 清空文件内容
            System.IO.File.WriteAllText(path, "");
            // 提示操作成功
            System.Windows.MessageBox.Show("已成功将游戏MOD模式转换为VERIFY模式");
        }

        private void cbupk(object sender, RoutedEventArgs e)
        {
            // 获取编辑框的文本
            string sourceText = txtPath.Text;
            // 判断文本是否为空
            if (string.IsNullOrEmpty(sourceText))
            {
                System.Windows.MessageBox.Show("请输入有效的文件路径！");
                return;
            }
            // 将文本按换行符分割成一个字符串数组
            string[] sourcePaths = sourceText.Split('\n');
            // 遍历数组中的每个元素，即每个文件路径
            foreach (string sourcePath in sourcePaths)
            {
                // 判断路径是否无效
                if (!System.IO.File.Exists(sourcePath))
                {
                    System.Windows.MessageBox.Show("请输入有效的文件路径！");
                    continue;
                }
                // 获取文件名称
                string fileName = System.IO.Path.GetFileName(sourcePath);
                // 获取程序运行目录
                string targetDir = System.AppDomain.CurrentDomain.BaseDirectory;
                // 拼接新的路径
                string targetPath = System.IO.Path.Combine(targetDir, fileName);
                // 判断目标目录下是否已存在同名文件
                if (System.IO.File.Exists(targetPath))
                {
                    // 弹出一个带有是和否按钮的对话框，询问用户是否覆盖文件
                    var result = System.Windows.MessageBox.Show("目标目录下已存在同名文件，是否覆盖？", "提示", System.Windows.MessageBoxButton.YesNo);
                    // 如果用户选择否，跳过当前循环，继续下一个文件路径
                    if (result == System.Windows.MessageBoxResult.No)
                    {
                        continue;
                    }
                }
                // 复制文件，并覆盖已存在的文件
                System.IO.File.Copy(sourcePath, targetPath, true);
            }
            //===================================================================================================
            // 创建一个Process实例
            var process = new Process();
            // 设置其StartInfo属性
            process.StartInfo.FileName = "Life is Strange UPK Pos Server.bat"; // 文件名
            process.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory; // 工作目录
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal; // 窗口样式
                                                                       // 调用Start方法来启动程序
            process.Start();

        }
        private void scupk(object sender, RoutedEventArgs e)
        {
            string sourceFolder = txtPath1.Text; //编辑框内的文件夹路径
            string destinationFolder = AppDomain.CurrentDomain.BaseDirectory; //程序的运行目录
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceFolder, destinationFolder);
            //===================================================================================================
            // 创建一个Process实例
            var process = new Process();
            // 设置其StartInfo属性
            process.StartInfo.FileName = "Life is Strange UPK Pos Clent.bat"; // 文件名
            process.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory; // 工作目录
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal; // 窗口样式
                                                                       // 调用Start方法来启动程序
            process.Start();
        }

        private void jci(object sender, RoutedEventArgs e)
        {
            // 打开 https://www.bing.com 网页
            System.Diagnostics.Process.Start("cmd", "/C start https://maxcauifield.github.io/");

        }
    }
}