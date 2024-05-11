using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromptSearchTool.Model
{
    [Table("PROMPT_TABLE")]
    public class MainPronptTable
    {
        [Key]
        [Column("No")]
        public int No { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("content")]
        public String Content { get; set; }
        [Column("output")]
        public String Output { get; set; }
        [Column("delete_flag")]
        public int DeleteFlag { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("update_time")]
        public DateTime UpdateTtime { get; set; }
    }

    [Table("TYPE_TABLE")]
    public class MainTypeTable
    {
        [Key]
        [Column("type")]
        public int Type { get; set; }
        [Column("type_content")]
        public String TypeContent { get; set; }
        [Column("type_image")]
        public String TypeImage { get; set; }
    }
}
