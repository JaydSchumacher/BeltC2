using Microsoft.EntityFrameworkCore;
 
namespace Belt_Exam.Models
{
    public class Belt_ExamContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Belt_ExamContext(DbContextOptions<Belt_ExamContext> options) : base(options) { }
	    public DbSet<User> users { get; set; }
        public DbSet<Idea> ideas { get; set; }
        public DbSet<Like> likes { get; set; }
    }
}