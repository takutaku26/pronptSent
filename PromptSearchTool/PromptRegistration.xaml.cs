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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PromptSearchTool
{
    /// <summary>
    /// PromptRegistration.xaml の相互作用ロジック
    /// </summary>
    public partial class PromptRegistration : Window
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PromptRegistration()
        {
            InitializeComponent();

            SetcomboBoxTypeEvent();
        }

        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("登録ボタンクリック");
            RegistrationPromptInfo(sender, e);
        }

        /// <summary>
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("閉じるボタンクリック");
            this.Close();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrationPromptInfo(object sender, RoutedEventArgs e)
        {
            DialogResult Msgresult = System.Windows.Forms.MessageBox.Show(
              "追加します。よろしいですか？",
              "プロンプト検索ツール",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question
            );

            if (String.IsNullOrEmpty(title_textBox.Text))
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「題名」を記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                             );
            }
            else if (String.IsNullOrEmpty(description_textBox.Text))
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「簡単な説明」を記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                             );
            }
            else if (String.IsNullOrEmpty(type_comboBox.Text))
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「種別」を記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                               );
            }
            else if (String.IsNullOrEmpty(content_textBox.Text))
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「プロンプトの内容」を記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                               );
            }
            else if (String.IsNullOrEmpty(output_textBox.Text))
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「出力例」を記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                               );
            }
            else if (title_textBox.Text.Length >= 101)
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「題名」は100文字以内で記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                );
            }
            else if (description_textBox.Text.Length >= 101)
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「簡単な説明」は100文字以内で記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                );
            }
            else if (content_textBox.Text.Length >= 8088)
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「プロンプトの内容」は8087文字以内で記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                );
            }
            else if (output_textBox.Text.Length >= 8088)
            {
                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "「出力例」は8087文字以内で記載して下さい。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                );
            }
            else if (Msgresult == System.Windows.Forms.DialogResult.Yes)
            {
                using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
                {
                    conn.Open();

                    using (DataContext context = new DataContext(conn))
                    {
                        var tPronpt = context.GetTable<MainPronptTable>();

                        MainPronptTable mainPronptTable = new MainPronptTable();

                        mainPronptTable.No = GetMaxNo();
                        mainPronptTable.Title = title_textBox.Text;
                        mainPronptTable.Description = description_textBox.Text;

                        Table<MainTypeTable> tType = context.GetTable<MainTypeTable>();
                        IQueryable<MainTypeTable> resultType;

                        resultType = from x in tType
                                     where x.TypeContent.StartsWith(type_comboBox.Text)
                                     orderby x.Type
                                     select x;

                        foreach (var term in resultType)
                        {
                            mainPronptTable.Type = term.Type;
                        }
                        mainPronptTable.Content = content_textBox.Text;
                        mainPronptTable.Output = output_textBox.Text;
                        mainPronptTable.DeleteFlag = 0;
                        mainPronptTable.CreateTime = DateTime.Now;

                        tPronpt.InsertOnSubmit(mainPronptTable);

                        context.SubmitChanges();
                    }

                    conn.Close();
                }

                Msgresult = System.Windows.Forms.MessageBox.Show(
                               "データを追加しました。",
                               "プロンプト検索ツール",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information
                             );

                this.Close();
            }
        }

        /// <summary>
        /// 最大のNoを取得
        /// </summary>
        /// <returns></returns>
        private int GetMaxNo()
        {
            using (var conn = new MySqlConnection("Database=mysql;Data Source=localhost;User Id=root;Password=root; sqlservermode=True;"))
            {
                using (DataContext con = new DataContext(conn))
                {
                    try
                    {
                        Table<MainPronptTable> tPronpt = con.GetTable<MainPronptTable>();
                        int result = tPronpt.Max(x => x.No);

                        return result + 1;
                    }
                    catch
                    {
                        return 1;
                    }
                }
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
                        Table<MainTypeTable> tType = con.GetTable<MainTypeTable>();
                        IQueryable<MainTypeTable> result = from x in tType orderby x.Type select x;

                        MainTypeTable empty = new MainTypeTable();
                        var list = result.ToList();
                        list.Insert(0, empty);

                        // コンボボックスに設定
                        this.type_comboBox.ItemsSource = list;
                        this.type_comboBox.DisplayMemberPath = "TypeContent";
                        this.type_comboBox.SelectedIndex = 0;
                    }
                }

                conn.Close();
            }
        }
    }
}
