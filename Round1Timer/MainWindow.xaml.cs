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

namespace Round1Timer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ShowWindow showWindow;

        Timer timer;

        MainWindowViewModel mainWindowVM = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            
            timer = new Timer();

            showWindow = new ShowWindow();
            showWindow.DataContext = new { timer, mainWindowVM };
            showWindow.Show();
            
            this.DataContext = new { mainWindowVM };
            mainWindowVM.Time = string.Format(
                        "{0}:{1}", minute.ToString("00"), second.ToString("00"));

            //操作方法の説明を表示（MainWindowKeyEHに記述）
            this.CommandContent.Text = explanation;

        }
    }
}
