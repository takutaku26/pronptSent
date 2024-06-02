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

            using (var context = new PgDbContext())
            {

                var tPronpt = context.MainPronptTable;

                MainPronptTable mainPronptTable = new MainPronptTable();

                mainPronptTable.No = GetMaxNo();
                mainPronptTable.Title = title_textBox.Text;
                mainPronptTable.Description = description_textBox.Text;

                var tType = context.MainTypeTable;
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

                // データ追加
                context.MainPronptTable.Add(mainPronptTable);
                context.SaveChanges();
            }

            this.Close();
        }

        /// <summary>
        /// 最大のNoを取得
        /// </summary>
        /// <returns></returns>
        private int GetMaxNo()
        {
            using (var context = new PgDbContext())
            {

                try
                {
                    var tPronpt = context.MainPronptTable;
                    int result = tPronpt.Max(x => x.No);

                    return result + 1;
                }
                catch
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// コンボボックスの設定
        /// </summary>
        private void SetcomboBoxTypeEvent()
        {
            using (var context = new PgDbContext())
            {

                var tType = context.MainTypeTable;
                IQueryable<MainTypeTable> result = from x in tType orderby x.Type select x;

                MainTypeTable empty = new MainTypeTable();
                var list = result.ToList();

                // コンボボックスに設定
                this.type_comboBox.ItemsSource = list;
                this.type_comboBox.DisplayMemberPath = "TypeContent";
                this.type_comboBox.SelectedIndex = 0;
            }
        }
    }
}
