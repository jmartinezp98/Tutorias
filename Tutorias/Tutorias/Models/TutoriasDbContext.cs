using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Tutorias.Models
{
    public class TutoriasDbContext : DbContext
    {
        public TutoriasDbContext() : base("TutoriasDbContext")
        {
            //Constructor vacio
            //La cadena de conexion se llamara...
        }

        public DbSet<Career> Careers { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentVulnerability> StudentVulnerabilities { get; set; }
        public DbSet<Turn> Turns { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Vulnerability> Vulnerabilities { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}