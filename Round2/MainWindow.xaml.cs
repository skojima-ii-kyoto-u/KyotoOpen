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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Round2.View;

namespace Round2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Player[] plist;
        Rule rule;

        ShowWindow showWindow;
        NormaSetWindow normaSetWindow;

        List<PlayerViewModel> playerVM;
        List<PlayerSelectViewModel> playerSVM;
        MainWindowViewModel mainWindowVM;

        IO.LogWriter writer;

        public MainWindow()
        {
            plist = IO.Loader.LoadPlayer("player.txt").ToArray();

            rule = new Rule(plist, "log.txt");
            
            InitializeComponent();

            playerVM = new List<PlayerViewModel>();
            playerSVM = new List<PlayerSelectViewModel>();
            for(int i = 0; i < plist.Length; i++)
            {
                playerVM.Add(new PlayerViewModel(plist[i]));
                playerSVM.Add(new PlayerSelectViewModel(plist[i]));
            }

            showWindow = new ShowWindow();
            showWindow.DataContext = new { playerVM };
            showWindow.Show();

            playerSVM[0].Color = new SolidColorBrush(Colors.Black);

            mainWindowVM = new MainWindowViewModel();
            mainWindowVM.Player1 = plist[0].Player1;
            mainWindowVM.Player2 = plist[0].Player2;
            
            this.DataContext = new { playerSVM, mainWindowVM };


            //操作方法の説明を表示（MainWindowKeyEHに記述）
            this.CommandContent.Text = explanation;

            writer = new IO.LogWriter(string.Format(
                "Log/PlayData{0}.txt",
                DateTime.Now.ToString("yyyyMMdd-HHmmss")));
            
            normaSetWindow = new NormaSetWindow(rule, playerVM, writer);
        }
        
        private Player[] makePlayer(int n)
        {
            Player[] plist = new Player[n];
            for (int i = 0; i < n; i++)
            {
                plist[i] = new Player(
                    "チームチームチーム" + i.ToString(),
                    i.ToString() + "佐藤A作",
                    i.ToString() + "佐藤B作");
            }

            return plist;
        }
    }
}
