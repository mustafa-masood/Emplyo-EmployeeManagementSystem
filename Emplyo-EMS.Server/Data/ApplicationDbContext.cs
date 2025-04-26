using Microsoft.EntityFrameworkCore;
using Emplyo_EMS.Server.Models;

namespace Emplyo_EMS.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> AttendanceRecords { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Role> Roles { get; set; }

        // OnModelCreating method where we configure the relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee <-> User: One-to-One
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee <-> Department: Many-to-One
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull); // Set null if department is deleted

            // Employee <-> Manager (Self-referencing, One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Set null if manager is deleted

            // Task <-> Assigner (Employee -> Task)
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Assigner)
                .WithMany(e => e.GivenTasks)
                .HasForeignKey(t => t.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict); // Prevent delete if tasks are assigned

            // Task <-> Assignee (Employee -> Task)
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Assignee)
                .WithMany(e => e.AssignedTasks)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict); // Prevent delete if tasks are assigned

            // Attendance <-> Employee: Many-to-One
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.AttendanceRecords)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete attendance records when employee is deleted

            // LeaveRequest <-> Employee: Many-to-One
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.Leaves)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete leave requests when employee is deleted

            // LeaveRequest <-> RequestedBy/ApprovedBy Employee: Many-to-One
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.RequestedByEmployee)
                .WithMany()
                .HasForeignKey(lr => lr.RequestedBy)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if leave request is linked

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(lr => lr.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if leave request is approved

            // Document <-> LeaveRequest: Many-to-One
            modelBuilder.Entity<Document>()
                .HasOne(d => d.LeaveRequest)
                .WithMany() // LeaveRequest does not need a navigation property to Documents
                .HasForeignKey(d => d.LeaveRequestId)
                .OnDelete(DeleteBehavior.NoAction); // Delete documents when the leave request is deleted


            // Document <-> ApprovedByEmployee: Many-to-One
            modelBuilder.Entity<Document>()
                .HasOne(d => d.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(d => d.ApprovedBy)
                .OnDelete(DeleteBehavior.SetNull); // Set null if approving employee is deleted

            // Payroll <-> Employee: Many-to-One
            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete payroll records when employee is deleted

            // Performance <-> Employee (Reviewed Employee) and Manager (Reviewer): Many-to-One
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Performances)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete performance records when employee is deleted

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if manager is deleted

            // Role <-> User: One-to-Many
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of roles if users are linked

            // Specifying Decimal Precision for Salary and other fields

            // Employee Salary
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)"); // Define precision and scale

            // Payroll Salary fields
            modelBuilder.Entity<Payroll>()
                .Property(p => p.GrossSalary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.Deductions)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.NetSalary)
                .HasColumnType("decimal(18,2)");
        }
    }
}
