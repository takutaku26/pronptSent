using log4net;
using MySql.Data.MySqlClient;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using PromptSearchTool.Model;

namespace PromptSearchTool
{
    /// <summary>
    /// PromptReferenceTool.xaml の相互作用ロジック
    /// </summary>
    public partial class PromptReferenceTool : Window
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PromptReferenceTool()
        {
            InitializeComponent();
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

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
