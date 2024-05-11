using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Npgsql;
using PromptSearchTool.Model;

namespace PromptSearchTool
{
    class PgDbContext : DbContext
    {
        private const string ConnectionString = "Server=localhost;User ID=root;Password=root;Database=postgres;port=5432";

        // コンストラクタにて接続文字列を設定
        public PgDbContext() : base(new NpgsqlConnection(ConnectionString), true) { }

        public DbSet<MainPronptTable> MainPronptTable { get; set; }
        public DbSet<MainTypeTable> MainTypeTable { get; set; }

        // スキーマを変更する場合にはここに設定
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("dora");
        }
    }
}
