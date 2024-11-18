using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Role> roles { get; set; }
		public DbSet<User> users { get; set; }
		public DbSet<Notifications> notifications { get; set; }
		public DbSet<UserNotifications> userNotifications { get; set; }
		public DbSet<Teacher> teachers { get; set; }
		public DbSet<Student> students { get; set; }
		public DbSet<TeacherSalary> teacherSalaries { get; set; }
		public DbSet<SubjectCategories> subjectCategories { get; set; }
		public DbSet<Subjects> subjects { get; set; }

		// Cấu hình khóa ngoại trong phương thức OnModelCreating
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasOne(u => u.Role)
				.WithMany(r => r.users)
				.HasForeignKey(u => u.RoleID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserNotifications>()
				.HasOne(u => u.Notifications)
				.WithMany(r => r.UserNotifications)
				.HasForeignKey(u => u.NotificationID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserNotifications>()
				.HasOne(u => u.User)
				.WithMany(r => r.UserNotifications)
				.HasForeignKey(u => u.UserID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Teacher>()
			 .HasOne(t => t.User)
			 .WithOne(u => u.Teachers)
			 .HasForeignKey<Teacher>(t => t.UserID)
			 .OnDelete(DeleteBehavior.Restrict);

			// Student: 1-1 với User
			modelBuilder.Entity<Student>()
				.HasOne(s => s.User)
				.WithOne(u => u.Students)
				.HasForeignKey<Student>(s => s.UserID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<TeacherSalary>()
				.HasOne(u => u.Teacher)
				.WithMany(r => r.TeacherSalaries)
				.HasForeignKey(u => u.TeacherID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Subjects>()
					.HasOne(u => u.SubjectCategories)
					.WithMany(r => r.Subjects)
					.HasForeignKey(u => u.CategoryID)  // Chỉ rõ khóa ngoại là CategoryID
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
