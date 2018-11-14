using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entity.Models;

namespace Entity
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() :
          base("DataBaseContext")
        {
        }

        public static DataBaseContext Create()
        {
            return new DataBaseContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


        }

    }
}
