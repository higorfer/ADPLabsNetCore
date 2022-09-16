using ADPLabsNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ADPLabsNetCore.Db
{
    public class ADPContext : DbContext
    {
        public ADPContext(DbContextOptions<ADPContext> options) : base(options)
        { }
        public DbSet<TaskTable> TaskTable { get; set; }
    }

}

