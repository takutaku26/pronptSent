using PromptSearchTool.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using log4net;
using MySql.Data.MySqlClient;
using System.Data.Linq;

namespace PromptSearchTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            InitializeComponent();

            var data = new ObservableCollection<MainPronptTable>();
            buttonStackPanel.DataContext = data;

            data.Add(new MainPronptTable { No = 1, Title = "2", Description = "3", Type = 4, Content = "5", Output = "6" });
        }

        private void Registration_Button(object sender, RoutedEventArgs e)
        {
            PromptRegistrationTool sw = new PromptRegistrationTool();
            sw.ShowDialog();
        }

        private void Reference_Button(object sender, RoutedEventArgs e)
        {
            PromptReferenceTool sw = new PromptReferenceTool();
            sw.ShowDialog();
        }
    }
}
