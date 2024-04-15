using log4net;
using MySql.Data.MySqlClient;
using PromptSearchTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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

            // 検索
            SetButtonDynamicEvent();

            // コンボボックスの設定
            SetcomboBoxTypeEvent();
        }

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerch_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("検索ボタンクリック");
            SetButtonDynamicEvent();
        }

        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("登録ボタンクリック");
            PromptRegistration sw = new PromptRegistration();
            sw.ShowDialog();

            // 再検索
            SetButtonDynamicEvent();
        }

        /// <summary>
        /// 詳細ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReference_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("詳細ボタンクリック");
            ReferenceDisplay(sender, e);

            // 再検索
            SetButtonDynamicEvent();
        }

        /// <summary>
        /// 詳細画面の表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceDisplay(object sender, RoutedEventArgs e) 
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                using (DataContext context = new DataContext(conn))
                {
                    int getNo = int.Parse(((Button)sender).Name.Substring(6));

                    Table<MainPronptTable> tPronpt = context.GetTable<MainPronptTable>();
                    IQueryable<MainPronptTable> result = from x in tPronpt
                                                         where x.No == getNo & x.DeleteFlag == 0
                                                         orderby x.No
                                                         select x;

                    MainPronptTable pronptTable = new MainPronptTable();

                    foreach (var term in result)
                    {
                        pronptTable.No = term.No;
                        pronptTable.Title = term.Title;
                        pronptTable.Description = term.Description;
                        pronptTable.Type = term.Type;
                        pronptTable.Content = term.Content;
                        pronptTable.Output = term.Output;
                        pronptTable.DeleteFlag = term.DeleteFlag;
                    }

                    var win = new PromptReference(pronptTable);
                    win.Owner = GetWindow(this);
                    win.ShowDialog();
                }

                conn.Close();
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void SetButtonDynamicEvent()
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    using (DataContext con = new DataContext(conn))
                    {
                        String searchName = this.serch_textBox.Text;

                        int searchType = this.serch_comboBox.SelectedIndex;

                        Table<MainPronptTable> tPronpt = con.GetTable<MainPronptTable>();

                        IQueryable<MainPronptTable> result;
                        if (searchType <= 0 )
                        {
                            result = from x in tPronpt
                                     where (x.Title.StartsWith(searchName) || x.Description.StartsWith(searchName)) & x.DeleteFlag == 0
                                     orderby x.No
                                     select x;
                        }
                        else
                        {
                            result = from x in tPronpt
                                     where (x.Title.StartsWith(searchName) || x.Description.StartsWith(searchName)) & x.Type == searchType & x.DeleteFlag == 0
                                     orderby x.No
                                     select x;
                        }

                        // Gridの値
                        panelButtunList.Rows = result.Count();
                        panelButtunList.Columns = 3;

                        // ボタン（Panel）を初期化
                        panelButtunList.Children.Clear();

                        foreach (var term in result)
                        {
                            Button button = new Button();
                            button.Margin = new Thickness(10);
                            button.Width = 230;

                            //ボタンを区別するためにNoを追加
                            button.Name = "button" + term.No.ToString();

                            button.Click += (sender, e) => btnReference_Click(sender, e);

                            // button contentの設定
                            StackPanel stackPanel = new StackPanel();
                            Image image = new Image();

                            Table<MainTypeTable> tType = con.GetTable<MainTypeTable>();

                            IQueryable<MainTypeTable> resultType;

                            resultType = from x in tType
                                      where x.Type == term.Type
                                      orderby x.Type
                                      select x;

                            foreach (var type in resultType)
                            {
                                image.Source = new BitmapImage(new Uri("Assets/" + type.TypeImage, UriKind.Relative));
                            }

                            image.Width = 30;
                            image.Height = 30;
                            stackPanel.Children.Add(image);

                            TextBlock titleTextBlock = new TextBlock();
                            titleTextBlock.FontSize = 10;
                            titleTextBlock.Text = term.Title;
                            stackPanel.Children.Add(titleTextBlock);

                            TextBlock descriptionTextBlock = new TextBlock();
                            descriptionTextBlock.FontSize = 10;
                            descriptionTextBlock.Text = term.Description;
                            stackPanel.Children.Add(descriptionTextBlock);

                            button.Content = stackPanel;

                            // ボタンを追加
                            panelButtunList.Children.Add(button);
                        }
                    }
                }

                conn.Close();
            }
        }

        /// <summary>
        /// コンボボックスの設定
        /// </summary>
        private void SetcomboBoxTypeEvent()
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {

                    using (DataContext con = new DataContext(conn))
                    {

                        Table<MainTypeTable> tPronpt = con.GetTable<MainTypeTable>();
                        IQueryable<MainTypeTable> result = from x in tPronpt orderby x.Type select x;

                        MainTypeTable empty = new MainTypeTable();
                        var list = result.ToList();
                        list.Insert(0, empty);

                        // コンボボックスに設定
                        this.serch_comboBox.ItemsSource = list;
                        this.serch_comboBox.DisplayMemberPath = "TypeContent";
                        this.serch_comboBox.SelectedIndex = 0;
                    }
                }

                conn.Close();
            }
        }
    }
}
