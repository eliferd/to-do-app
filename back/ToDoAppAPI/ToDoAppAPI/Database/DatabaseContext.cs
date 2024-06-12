using Microsoft.EntityFrameworkCore;
using ToDoAppAPI.Models;
using Task = ToDoAppAPI.Models.Task;

namespace ToDoAppAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> ctx)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "server=localhost;user=todoappapi;password=0000;database=todoapp";
            MySqlServerVersion dbmsVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString, dbmsVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().Property<string>("Label").HasColumnType("varchar(20)");
            modelBuilder.Entity<Status>().Property<string>("Code").HasColumnType("varchar(10)");
            modelBuilder.Entity<Status>().HasData(new Status[]
            {
                new Status { Id = 1, Code = "TODO", Label = "To-Do"},
                new Status { Id = 2, Code = "PROGRESS", Label = "In Progress"},
                new Status { Id = 3, Code = "DONE", Label = "Done"},
            });

            modelBuilder.Entity<Board>().HasMany(b => b.Tasks).WithOne(t => t.Board).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Board>().HasOne(b => b.Author);
            modelBuilder.Entity<Board>().Property<string>("Label").HasColumnType("varchar(100)");

            modelBuilder.Entity<Task>().HasOne(t => t.Board).WithMany(b => b.Tasks);
            modelBuilder.Entity<Task>().HasOne(t => t.Status);
            modelBuilder.Entity<Task>().Property<string>("Label").HasColumnType("varchar(70)");
            modelBuilder.Entity<Task>().Property<string>("PicturePath").HasColumnType("varchar(255)");

            modelBuilder.Entity<User>().Property<string>("Username").HasColumnType("varchar(50)");
            modelBuilder.Entity<User>().Property<string>("Password").HasColumnType("varchar(255)");
            modelBuilder.Entity<User>().Property<string>("PasswordSalt").HasColumnType("varchar(255)");

        }
    }
}
