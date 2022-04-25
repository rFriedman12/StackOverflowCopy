using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow25.Data
{
    public class QuestionsDataContext :DbContext
    {
        private string _connString;

        public QuestionsDataContext(string connString)
        {
            _connString = connString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }  
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionsTags> QuestionsTags { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuestionsTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.QuestionId, l.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
