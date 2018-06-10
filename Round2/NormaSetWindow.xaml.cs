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

namespace Round2
{
    /// <summary>
    /// NormaSetWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NormaSetWindow : Window
    {
        private Rule rule;
        private List<View.PlayerViewModel> playervm;
        private Player[] plist;

        private List<int> norma = new List<int>();
        public List<int> Norma
        {
            get
            {
                return norma;
            }
        }

        private bool isNormaDefined = false;
        public bool IsNormaDefined
        {
            get
            {
                return isNormaDefined;
            }
        }

        private IO.LogWriter writer;

        public NormaSetWindow(Rule r, List<View.PlayerViewModel> pvm, IO.LogWriter writer)
        {
            rule = r;
            playervm = pvm;
            InitializeComponent();
            
            Message.Text = "0:5o2x/1:4o2x:/2:5o3x/3:4o3x";

            this.writer = writer;
        }

        public void CommandTextKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string s = CommandText.Text;
                string[] split = s.Split(new char[] { ' ', '\t' });

                foreach (string str in split)
                {
                    int value;
                    if (int.TryParse(str, out value))
                        norma.Add(value);
                }

                rule.SetNorma(plist, norma);

                string log = "set:";
                foreach (View.PlayerViewModel pvm in playervm)
                {
                    log += string.Format("{0}-{1}/", pvm.Player.TeamName, pvm.Norma);
                    pvm.RaiseNormaChanged();
                }
                writer.AddLog(log);

                this.Hide();
            }
            else
            {
            }
        }

        public void OriginalShow(Player[] pl)
        {
            plist = pl;
            Show();
            this.Focus();
        }
    }
}
