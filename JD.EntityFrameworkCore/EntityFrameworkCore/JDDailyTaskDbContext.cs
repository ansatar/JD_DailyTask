using JD.Core.JDTaskSummaries;
using JD.Core.JDUsers;
using JD.Core.JDUserTaskRecords;
using JD.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JD.EntityFrameworkCore.EntityFrameworkCore
{
    public class JDDailyTaskDbContext : DbContext
    {
        public JDDailyTaskDbContext(DbContextOptions<JDDailyTaskDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //decimal 类型设置精度
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var type = item.ClrType;
                var props = type.GetProperties().Where(c => c.IsDefined(typeof(DecimalPrecisionAttribute), true)).ToArray();
                foreach (var p in props)
                {
                    var precis = p.GetCustomAttribute<DecimalPrecisionAttribute>();
                    modelBuilder.Entity(type).Property(p.Name).HasColumnType($"decimal({precis.Precision},{precis.Scale})");
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JDUser> JDUsers { get; set; }
        public DbSet<JDTaskSummary> JDTaskSummaries { get; set; }
        public DbSet<JDUserTaskRecord> JDUserTaskRecords { get; set; }

    }
}
