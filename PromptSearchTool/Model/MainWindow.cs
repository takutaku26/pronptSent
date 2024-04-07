using System;
using System.Data.Linq.Mapping;

namespace PromptSearchTool.Model
{
    [Table(Name = "PROMPT_TABLE")]
    class MainPronptTable
    {
        [Column(Name = "No", IsPrimaryKey = true)]
        public int No { get; set; }
        [Column(Name = "title")]
        public String Title { get; set; }
        [Column(Name = "description")]
        public String Description { get; set; }
        [Column(Name = "type", IsPrimaryKey = true)]
        public int Type { get; set; }
        [Column(Name = "content")]
        public String Content { get; set; }
        [Column(Name = "output")]
        public String Output { get; set; }
    }

    [Table(Name = "TYPE_TABLE")]
    class MainTypeTable
    {
        [Column(Name = "type", IsPrimaryKey = true)]
        public int Type { get; set; }
        [Column(Name = "type_content")]
        public String TypeContent { get; set; }
    }
}
