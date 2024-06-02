using log4net;
using PromptSearchTool.Model;
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

namespace PromptSearchTool
{
    /// <summary>
    /// PromptReference.xaml の相互作用ロジック
    /// </summary>
    public partial class PromptReference : Window
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainPronptTable SelectedPronpt { set; get; }

        public PromptReference(MainPronptTable SelectedPronpt)
        {
            InitializeComponent();

            SearchPromptReference(SelectedPronpt);

            SetcomboBoxTypeEvent(SelectedPronpt);
        }

        /// <summary>
        /// 保存ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("保存ボタンクリック");
            SavePromptReference(sender, e);
        }

        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletion_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("削除ボタンクリック");
            DeletionPromptReference(sender, e);
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
        /// 保存処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePromptReference(object sender, RoutedEventArgs e)
        {

            using (var context = new PgDbContext())
            {

                var tPronpt = context.MainPronptTable;

                var result = tPronpt.Single(x => x.No == SelectedPronpt.No);

                result.Title = title_textBox.Text;
                result.Description = description_textBox.Text;

                var tType = context.MainTypeTable;
                IQueryable<MainTypeTable> resultType;

                resultType = from x in tType
                             where x.TypeContent.StartsWith(type_comboBox.Text)
                             orderby x.Type
                             select x;

                foreach (var term in resultType)
                {
                    result.Type = term.Type;
                }
                result.Content = content_textBox.Text;
                result.Output = output_textBox.Text;
                result.DeleteFlag = 0;
                result.UpdateTtime = DateTime.Now;

                context.SaveChanges();
            }

            this.Close();
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletionPromptReference(object sender, RoutedEventArgs e)
        {

            using (var context = new PgDbContext())
            {

                var tPronpt = context.MainPronptTable;

                var result = tPronpt.Single(x => x.No == SelectedPronpt.No);

                result.Title = title_textBox.Text;
                result.Description = description_textBox.Text;

                var tType = context.MainTypeTable;
                IQueryable<MainTypeTable> resultType;

                resultType = from x in tType
                             where x.TypeContent.StartsWith(type_comboBox.Text)
                             orderby x.Type
                             select x;

                foreach (var term in resultType)
                {
                    result.Type = term.Type;
                }
                result.Content = content_textBox.Text;
                result.Output = output_textBox.Text;
                result.DeleteFlag = 1;
                result.UpdateTtime = DateTime.Now;

                context.SaveChanges();
            }

            this.Close();
        }

        /// <summary>
        /// メイン画面から詳細画面のデータを移行
        /// </summary>
        /// <param name="SelectedPronpt"></param>
        private void SearchPromptReference(MainPronptTable SelectedPronpt)
        {
            using (var context = new PgDbContext())
            {
                this.SelectedPronpt = SelectedPronpt;

                this.title_textBox.Text = SelectedPronpt.Title.ToString();
                this.description_textBox.Text = SelectedPronpt.Description.ToString();
                this.type_comboBox.Text = SelectedPronpt.Type.ToString();
                this.content_textBox.Text = SelectedPronpt.Content.ToString();
                this.output_textBox.Text = SelectedPronpt.Output.ToString();
            }
        }

        /// <summary>
        /// コンボボックスの設定
        /// </summary>
        /// <param name="SelectedCat"></param>
        private void SetcomboBoxTypeEvent(MainPronptTable SelectedCat)
        {
            using (var context = new PgDbContext())
            {
                var tPronpt = context.MainTypeTable;
                IQueryable<MainTypeTable> result = from x in tPronpt orderby x.Type select x;

                MainTypeTable empty = new MainTypeTable();
                var list = result.ToList();

                // コンボボックスに設定
                this.type_comboBox.ItemsSource = list;
                this.type_comboBox.DisplayMemberPath = "TypeContent";
                this.type_comboBox.SelectedIndex = SelectedCat.Type - 1;
            }
        }
    }
}
