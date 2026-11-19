#region complete code

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System.Collections.Generic;
//using System.Reflection.Emit;
//// 1) ENUMS

//public enum CourseLevel
//{
//    Beginner,
//    Intermediate,
//    Advanced
//}

//public enum EnrollmentStatus
//{
//    Active,
//    Completed,
//    Dropped
//}


//// 2) ENTITIY

//public class Instructor
//{
//    public int Id { get; set; }
//    public string FullName { get; set; } = "";
//    public string Email { get; set; } = "";
//    public string? Bio { get; set; }
//    public DateTime JoinedDate { get; set; }

//    public List<Course> Courses { get; set; } = new();
//}

//public class Category
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = "";
//    public string? Description { get; set; }

//    public int? ParentCategoryId { get; set; }
//    public Category? ParentCategory { get; set; }

//    public List<Category> SubCategories { get; set; } = new();
//    public List<Course> Courses { get; set; } = new();
//}

//public class Course
//{
//    public int Id { get; set; }
//    public string Title { get; set; } = "";
//    public string? Description { get; set; }
//    public decimal Price { get; set; }
//    public CourseLevel Level { get; set; }
//    public bool IsPublished { get; set; }
//    public DateTime? PublishedDate { get; set; }

//    public int InstructorId { get; set; }
//    public int CategoryId { get; set; }

//    public List<Enrollment> Enrollments { get; set; } = new();
//}

//public class Student
//{
//    public int Id { get; set; }
//    public string FullName { get; set; } = "";
//    public string Email { get; set; } = "";
//    public DateTime DateOfBirth { get; set; }

//    public List<Enrollment> Enrollments { get; set; } = new();
//}

//public class Enrollment
//{
//    public int StudentId { get; set; }
//    public int CourseId { get; set; }

//    public DateTime EnrollmentDate { get; set; }
//    public int Progress { get; set; }
//    public EnrollmentStatus Status { get; set; }
//    public decimal? FinalGrade { get; set; }
//}

//// 3) ENTITY CONFIGURATIONS

//public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
//{
//    public void Configure(EntityTypeBuilder<Instructor> builder)
//    {
//        builder.HasKey(i => i.Id);

//        builder.Property(i => i.FullName)
//            .IsRequired()
//            .HasMaxLength(100);

//        builder.Property(i => i.Email)
//            .IsRequired()
//            .HasMaxLength(150);

//        builder.Property(i => i.Bio)
//            .HasMaxLength(500);

//        builder.HasData(
//            new Instructor { Id = 1, FullName = "Ahmed Ali", Email = "ahmed@mail.com", JoinedDate = new DateTime(2024, 1, 1) },
//            new Instructor { Id = 2, FullName = "Sara Mohamed", Email = "sara@mail.com", JoinedDate = new DateTime(2024, 2, 1) },
//            new Instructor { Id = 3, FullName = "Omar Hassan", Email = "omar@mail.com", JoinedDate = new DateTime(2024, 3, 1) }
//        );
//    }
//}

//public class CategoryConfiguration : IEntityTypeConfiguration<Category>
//{
//    public void Configure(EntityTypeBuilder<Category> builder)
//    {
//        builder.HasKey(c => c.Id);

//        builder.Property(c => c.Name)
//            .IsRequired()
//            .HasMaxLength(100);

//        builder.Property(c => c.Description)
//            .HasMaxLength(300);

//        // Self Referencing Relationship
//        builder.HasOne(c => c.ParentCategory)
//            .WithMany(c => c.SubCategories)
//            .HasForeignKey(c => c.ParentCategoryId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.HasData(
//            new Category { Id = 1, Name = "Programming" },
//            new Category { Id = 2, Name = "CSharp", ParentCategoryId = 1 },
//            new Category { Id = 3, Name = "Advanced CSharp", ParentCategoryId = 2 },
//            new Category { Id = 4, Name = "Design" },
//            new Category { Id = 5, Name = "Web Design", ParentCategoryId = 4 }
//        );
//    }
//}

//public class CourseConfiguration : IEntityTypeConfiguration<Course>
//{
//    public void Configure(EntityTypeBuilder<Course> builder)
//    {
//        builder.HasKey(c => c.Id);

//        builder.Property(c => c.Title)
//            .IsRequired()
//            .HasMaxLength(150);

//        builder.Property(c => c.Description)
//            .HasMaxLength(500);

//        builder.Property(c => c.Price)
//            .HasPrecision(10, 2);

//        builder.Property(c => c.Level)
//            .HasConversion<int>();

//        // Instructor - Course Relationship
//        builder.HasOne<Instructor>()
//            .WithMany(i => i.Courses)
//            .HasForeignKey(c => c.InstructorId);

//        // Category - Course Relationship
//        builder.HasOne<Category>()
//            .WithMany(c => c.Courses)
//            .HasForeignKey(c => c.CategoryId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.HasData(
//            new Course { Id = 1, Title = "C# Basics", Price = 100, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 1), InstructorId = 1, CategoryId = 1 },
//            new Course { Id = 2, Title = "OOP in C#", Price = 200, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 4, 2), InstructorId = 1, CategoryId = 2 },
//            new Course { Id = 3, Title = "Advanced C#", Price = 300, Level = CourseLevel.Advanced, IsPublished = false, InstructorId = 2, CategoryId = 3 },
//            new Course { Id = 4, Title = "HTML and CSS", Price = 150, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 3), InstructorId = 2, CategoryId = 4 },
//            new Course { Id = 5, Title = "Web Design", Price = 250, Level = CourseLevel.Intermediate, IsPublished = false, InstructorId = 3, CategoryId = 5 },
//            new Course { Id = 6, Title = "JavaScript", Price = 180, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 4), InstructorId = 3, CategoryId = 4 }
//        );
//    }
//}

//public class StudentConfiguration : IEntityTypeConfiguration<Student>
//{
//    public void Configure(EntityTypeBuilder<Student> builder)
//    {
//        builder.HasKey(s => s.Id);

//        builder.Property(s => s.FullName)
//            .IsRequired()
//            .HasMaxLength(100);

//        builder.Property(s => s.Email)
//            .IsRequired()
//            .HasMaxLength(150);
//    }
//}

//public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
//{
//    public void Configure(EntityTypeBuilder<Enrollment> builder)
//    {
//        // Composite Primary Key
//        builder.HasKey(e => new { e.StudentId, e.CourseId });

//        builder.Property(e => e.FinalGrade)
//            .HasPrecision(5, 2);

//        builder.Property(e => e.Status)
//            .HasConversion<int>();

//        builder.HasOne<Student>()
//            .WithMany(s => s.Enrollments)
//            .HasForeignKey(e => e.StudentId);

//        builder.HasOne<Course>()
//            .WithMany(c => c.Enrollments)
//            .HasForeignKey(e => e.CourseId);
//    }
//}

//// 4) DB CONTEXT

//public class AppDbContext : DbContext
//{
//    public DbSet<Instructor> Instructors => Set<Instructor>();
//    public DbSet<Category> Categories => Set<Category>();
//    public DbSet<Course> Courses => Set<Course>();
//    public DbSet<Student> Students => Set<Student>();
//    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

//    protected override void OnConfiguring(DbContextOptionsBuilder options)
//    {
//        options.UseSqlServer(
//            "Server=.;Database=OnlineLearningPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
//    }
//}
//// 5) PROGRAM - MAIN


//public class Program
//{
//    static void Main()
//    {
//        using var db = new AppDbContext();

//        db.Database.Migrate();

//        SeedStudentsAndEnrollments();

//        // CREATE
//        AddInstructor();
//        AddCourse();
//        AddSeveralStudents();
//        EnrollStudent(1, 1);

//        // READ
//        FindCourse(1);
//        FindCourseByTitle("C# Basics");
//        FilterCourses(100, 250);
//        DisplayCategories();
//        DisplayCourseRoster(1);

//        // UPDATE
//        ChangeCoursePrice(1, 120);
//        UpdateProgress(2, 1, 100);
//        PublishCourse(5);

//        // DELETE
//        UnenrollStudent(1, 2);
//        TryDeleteCategory(1);
//        SafeDeleteCategory(5);
//        DeleteCourse(6);

//        // BULK OPERATIONS
//        BulkPriceRaise(CourseLevel.Beginner);
//        BulkDeleteDroppedEnrollments();
//    }

//    // 6) RUNTIME SEEDING

//    static void SeedStudentsAndEnrollments()
//    {
//        using var db = new AppDbContext();

//        if (!db.Students.Any())
//        {
//            db.Students.AddRange(
//                new Student { FullName = "Ali Ahmed", Email = "ali@mail.com", DateOfBirth = new DateTime(2003, 1, 1) },
//                new Student { FullName = "Sara Ali", Email = "sara.student@mail.com", DateOfBirth = new DateTime(2002, 2, 2) },
//                new Student { FullName = "Omar Ali", Email = "omar.student@mail.com", DateOfBirth = new DateTime(2003, 3, 3) },
//                new Student { FullName = "Mona Hassan", Email = "mona@mail.com", DateOfBirth = new DateTime(2001, 4, 4) },
//                new Student { FullName = "Khaled Samir", Email = "khaled@mail.com", DateOfBirth = new DateTime(2002, 5, 5) }
//            );

//            db.SaveChanges();
//        }

//        if (!db.Enrollments.Any())
//        {
//            db.Enrollments.AddRange(
//                new Enrollment { StudentId = 1, CourseId = 2, EnrollmentDate = DateTime.Now, Progress = 40, Status = EnrollmentStatus.Active },
//                new Enrollment { StudentId = 1, CourseId = 4, EnrollmentDate = DateTime.Now, Progress = 100, Status = EnrollmentStatus.Completed },
//                new Enrollment { StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.Now, Progress = 50, Status = EnrollmentStatus.Active },
//                new Enrollment { StudentId = 2, CourseId = 6, EnrollmentDate = DateTime.Now, Progress = 20, Status = EnrollmentStatus.Dropped },
//                new Enrollment { StudentId = 3, CourseId = 2, EnrollmentDate = DateTime.Now, Progress = 10, Status = EnrollmentStatus.Dropped },
//                new Enrollment { StudentId = 3, CourseId = 4, EnrollmentDate = DateTime.Now, Progress = 60, Status = EnrollmentStatus.Active },
//                new Enrollment { StudentId = 4, CourseId = 1, EnrollmentDate = DateTime.Now, Progress = 100, Status = EnrollmentStatus.Completed },
//                new Enrollment { StudentId = 5, CourseId = 6, EnrollmentDate = DateTime.Now, Progress = 30, Status = EnrollmentStatus.Active }
//            );

//            db.SaveChanges();
//        }
//    }

//    // 7) CREATE OPERATIONS

//    static void AddInstructor()
//    {
//        using var db = new AppDbContext();

//        db.Instructors.Add(new Instructor
//        {
//            FullName = "New Instructor",
//            Email = "new@mail.com",
//            JoinedDate = DateTime.Now
//        });

//        db.SaveChanges();
//        Console.WriteLine("Instructor added.");
//    }

//    static void AddCourse()
//    {
//        using var db = new AppDbContext();

//        if (!db.Instructors.Any(i => i.Id == 1) ||
//            !db.Categories.Any(c => c.Id == 1))
//        {
//            Console.WriteLine("Instructor or Category not found.");
//            return;
//        }

//        db.Courses.Add(new Course
//        {
//            Title = "New Course",
//            Price = 200,
//            Level = CourseLevel.Beginner,
//            IsPublished = false,
//            InstructorId = 1,
//            CategoryId = 1
//        });

//        db.SaveChanges();
//        Console.WriteLine("Course added.");
//    }

//    static void AddSeveralStudents()
//    {
//        using var db = new AppDbContext();

//        db.Students.AddRange(
//            new Student { FullName = "Student Six", Email = "six@mail.com", DateOfBirth = new DateTime(2003, 1, 1) },
//            new Student { FullName = "Student Seven", Email = "seven@mail.com", DateOfBirth = new DateTime(2003, 2, 2) },
//            new Student { FullName = "Student Eight", Email = "eight@mail.com", DateOfBirth = new DateTime(2003, 3, 3) }
//        );

//        db.SaveChanges();
//        Console.WriteLine("Students added.");
//    }

//    static void EnrollStudent(int studentId, int courseId)
//    {
//        using var db = new AppDbContext();

//        var course = db.Courses.Find(courseId);

//        if (!db.Students.Any(s => s.Id == studentId) ||
//            course == null || !course.IsPublished)
//        {
//            Console.WriteLine("Cannot enroll: Student or published course not found.");
//            return;
//        }

//        if (db.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
//        {
//            Console.WriteLine("Student already enrolled.");
//            return;
//        }

//        db.Enrollments.Add(new Enrollment
//        {
//            StudentId = studentId,
//            CourseId = courseId,
//            EnrollmentDate = DateTime.Now,
//            Progress = 0,
//            Status = EnrollmentStatus.Active
//        });

//        db.SaveChanges();
//        Console.WriteLine("Enrollment completed.");
//    }

//    // 8) READ OPERATIONS

//    static void FindCourse(int id)
//    {
//        using var db = new AppDbContext();

//        var course = db.Courses.Find(id);

//        Console.WriteLine(course == null ? "Course not found." : course.Title);
//    }

//    static void FindCourseByTitle(string title)
//    {
//        using var db = new AppDbContext();

//        var course = db.Courses.FirstOrDefault(c => c.Title == title);

//        Console.WriteLine(course == null ? "Course not found." : course.Title);
//    }

//    static void FilterCourses(decimal minPrice, decimal maxPrice)
//    {
//        using var db = new AppDbContext();

//        var courses = db.Courses
//            .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
//            .OrderByDescending(c => c.Price)
//            .ToList();

//        foreach (var course in courses)
//            Console.WriteLine($"{course.Title} - {course.Price}");
//    }

//    static void DisplayCategories()
//    {
//        using var db = new AppDbContext();

//        var topCategories = db.Categories
//            .Where(c => c.ParentCategoryId == null)
//            .ToList();

//        var subCategories = db.Categories
//            .Where(c => c.ParentCategoryId != null)
//            .ToList();

//        Console.WriteLine("Top-level Categories:");
//        foreach (var c in topCategories)
//            Console.WriteLine(c.Name);

//        Console.WriteLine("Subcategories:");
//        foreach (var c in subCategories)
//            Console.WriteLine(c.Name);
//    }

//    static void DisplayCourseRoster(int courseId)
//    {
//        using var db = new AppDbContext();

//        var studentIds = db.Enrollments
//            .Where(e => e.CourseId == courseId)
//            .Select(e => e.StudentId)
//            .ToList();

//        var students = db.Students
//            .Where(s => studentIds.Contains(s.Id))
//            .ToList();

//        Console.WriteLine($"Students enrolled: {students.Count}");

//        foreach (var student in students)
//            Console.WriteLine(student.FullName);
//    }

//    // 9) UPDATE OPERATIONS

//    static void ChangeCoursePrice(int courseId, decimal newPrice)
//    {
//        using var db = new AppDbContext();

//        var course = db.Courses.Find(courseId);

//        if (course == null)
//            return;

//        course.Price = newPrice;
//        db.SaveChanges();

//        Console.WriteLine("Price updated.");
//    }

//    static void UpdateProgress(int studentId, int courseId, int newProgress)
//    {
//        using var db = new AppDbContext();

//        if (newProgress < 0 || newProgress > 100)
//        {
//            Console.WriteLine("Progress must be between 0 and 100.");
//            return;
//        }

//        var enrollment = db.Enrollments.Find(studentId, courseId);

//        if (enrollment == null)
//            return;

//        enrollment.Progress = newProgress;

//        if (newProgress == 100)
//            enrollment.Status = EnrollmentStatus.Completed;
//        else if (enrollment.Status == EnrollmentStatus.Completed)
//            enrollment.Status = EnrollmentStatus.Active;

//        db.SaveChanges();
//        Console.WriteLine("Progress updated.");
//    }

//    static void PublishCourse(int courseId)
//    {
//        using var db = new AppDbContext();

//        var course = db.Courses.Find(courseId);

//        if (course == null)
//            return;

//        course.IsPublished = true;
//        course.PublishedDate = DateTime.Now;

//        db.SaveChanges();
//        Console.WriteLine("Course published.");
//    }

//    // 10) DELETE OPERATIONS

//    static void UnenrollStudent(int studentId, int courseId)
//    {
//        using var db = new AppDbContext();

//        var enrollment = db.Enrollments.Find(studentId, courseId);

//        if (enrollment == null)
//            return;

//        db.Enrollments.Remove(enrollment);
//        db.SaveChanges();

//        Console.WriteLine("Student unenrolled.");
//    }

//    static void TryDeleteCategory(int categoryId)
//    {
//        using var db = new AppDbContext();

//        var category = db.Categories.Find(categoryId);

//        if (category == null)
//            return;

//        db.Categories.Remove(category);

//        try
//        {
//            db.SaveChanges();
//            Console.WriteLine("Category deleted.");
//        }
//        catch (DbUpdateException)
//        {
//            Console.WriteLine("Cannot delete category because it has related records.");
//        }
//    }

//    static void SafeDeleteCategory(int categoryId)
//    {
//        using var db = new AppDbContext();

//        var category = db.Categories.Find(categoryId);

//        if (category == null)
//            return;

//        bool hasSubCategories = db.Categories
//            .Any(c => c.ParentCategoryId == categoryId);

//        bool hasCourses = db.Courses
//            .Any(c => c.CategoryId == categoryId);

//        if (hasSubCategories || hasCourses)
//        {
//            Console.WriteLine("Category cannot be deleted.");
//            return;
//        }

//        db.Categories.Remove(category);
//        db.SaveChanges();

//        Console.WriteLine("Category deleted safely.");
//    }

//    static void DeleteCourse(int courseId)
//    {
//        using var db = new AppDbContext();

//        int before = db.Enrollments.Count(e => e.CourseId == courseId);

//        var course = db.Courses.Find(courseId);

//        if (course == null)
//            return;

//        db.Courses.Remove(course);
//        db.SaveChanges();

//        int after = db.Enrollments.Count(e => e.CourseId == courseId);

//        Console.WriteLine($"Enrollments before delete: {before}");
//        Console.WriteLine($"Enrollments after delete: {after}");
//        Console.WriteLine("Enrollments are deleted with the course.");
//    }

//    // 11) BULK OPERATION

//    static void BulkPriceRaise(CourseLevel level)
//    {
//        using var db = new AppDbContext();

//        int affected = db.Courses
//            .Where(c => c.IsPublished && c.Level == level)
//            .ExecuteUpdate(s =>
//                s.SetProperty(c => c.Price, c => c.Price * 1.10m));

//        Console.WriteLine($"Updated courses: {affected}");
//    }

//    static void BulkDeleteDroppedEnrollments()
//    {
//        using var db = new AppDbContext();

//        int deleted = db.Enrollments
//            .Where(e => e.Status == EnrollmentStatus.Dropped)
//            .ExecuteDelete();

//        Console.WriteLine($"Deleted enrollments: {deleted}");
//    }
//}

#endregion