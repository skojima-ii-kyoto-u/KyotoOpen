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

namespace Round3Select
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CourseViewModel> courseVM = new List<CourseViewModel>();
        NextTeamsViewModel nextTeamsVM;

        public MainWindow()
        {
            InitializeComponent();

            courseVM.Add(new CourseViewModel("α"));
            courseVM.Add(new CourseViewModel("β"));
            courseVM.Add(new CourseViewModel("γ"));
            courseVM.Add(new CourseViewModel("δ"));
            courseVM.Add(new CourseViewModel("ε"));

            nextTeamsVM = new NextTeamsViewModel(IO.Loader.LoadPlayer("player.txt"));

            this.DataContext = new { courseVM, nextTeamsVM };
        }
    }
}
