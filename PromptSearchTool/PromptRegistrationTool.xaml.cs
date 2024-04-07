using log4net;
using MySql.Data.MySqlClient;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using PromptSearchTool.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PromptSearchTool
{    

    /// <summary>
    /// PromptRegistrationTool.xaml の相互作用ロジック
    /// </summary>
    public partial class PromptRegistrationTool : Window
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PromptRegistrationTool()
        {
            InitializeComponent();

            searchData();
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            logger.Info("追加ボタンクリック");

            // 接続
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                // データを追加する
                using (DataContext context = new DataContext(conn))
                {
                    // 対象のテーブルオブジェクトを取得
                    var table = context.GetTable<ReferencePronptTable>();

                    // データ作成
                    ReferencePronptTable publicRelationsInformation = new ReferencePronptTable();

                    publicRelationsInformation.No = GetMaxPublicRelationsInfo();
                    publicRelationsInformation.Title = title_textBox.Text;
                    publicRelationsInformation.Description = title_textBox.Text;
                    publicRelationsInformation.Type = 2;
                    publicRelationsInformation.Content = title_textBox.Text;
                    publicRelationsInformation.Output = title_textBox.Text;

                    // データ追加
                    table.InsertOnSubmit(publicRelationsInformation);
                    // DBの変更を確定
                    context.SubmitChanges();
                }
                conn.Close();
            }
            MessageBox.Show("データを追加しました。");
        }

        private int GetMaxPublicRelationsInfo()
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                using (DataContext con = new DataContext(conn))
                {
                    try
                    {
                        // データを取得
                        Table<ReferencePronptTable> tblPublicRelations = con.GetTable<ReferencePronptTable>();
                        int result = tblPublicRelations.Max(x => x.No);

                        return result + 1;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }

        private void Deletion_Button(object sender, RoutedEventArgs e)
        {
            logger.Info("追加ボタンクリック");

            // 接続
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                // データを削除する
                using (DataContext context = new DataContext(conn))
                {
                    // 対象のテーブルオブジェクトを取得
                    var table = context.GetTable<ReferencePronptTable>();

                    var target = table.Single(x => x.No == x.No);
                    // データ削除
                    table.DeleteOnSubmit(target);
                    // DBの変更を確定
                    context.SubmitChanges();
                }
                conn.Close();
            }

            // データ再検索
            MessageBox.Show("データを削除しました。");
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void searchData()
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                conn.Open();

                using (DataContext con = new DataContext(conn))
                {
                    // データを取得
                    Table<MainPronptTable> referenceTable = con.GetTable<MainPronptTable>();

                    // サンプルなので適当に組み立てる
                    IQueryable<MainPronptTable> result;

                    result = from x in referenceTable
                             where x.No == 1
                             orderby x.No
                             select x;

                    this.title_textBox.Text = "test";
                }

                conn.Close();
            }
        }
    }
}
