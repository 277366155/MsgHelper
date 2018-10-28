using MH.Core;
using MH.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MH.Context
{
    public class MHContext : DbContext
    {
        private string _dbConnStr;

        public MHContext()
        {
            _dbConnStr = BaseCore.Configuration.GetConnectionString("MySqlConnection");
        }
        public MHContext(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                this._dbConnStr = BaseCore.Configuration.GetConnectionString("MySqlConnection");
            }
            else
            {
                _dbConnStr = connStr;
            }
        }

        /// <summary>
        /// 基类DbContext()构造函数中会调用该方法
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_dbConnStr);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<WxUsers> WxUsers { get; set; }
        public DbSet<WxUserMessage> WxUserMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<WxUsers>().Property(p => p.RowVersion).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
