using MH.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MH.Context
{
    public class MHContext : DbContext
    {
        //public MHContext(DbContextOptions options) : base(options)
        //{

        //}

        
        //startup中未依赖注入的话，可以在此配置数据库连接
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL(Configuration.GetConnectionString("MySqlConnection"));

            optionsBuilder.UseMySQL("Server=127.0.0.1;database=MsgHelper;uid=root;pwd=1qaz@WSX;SslMode=None");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<WxUsers> WxUsers { get; set; }
        public DbSet<WxUserMessage> WxUserMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<WxUsers>().Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
