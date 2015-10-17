using System.Data.Entity;
using TicTacToe.Core;

namespace TicTacToe.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public virtual void Commit()
        {
            SaveChanges();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Move> Moves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Game>()
                .HasMany<Move>(g => g.Moves)
                .WithRequired(m => m.Game)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Game>()
                .HasRequired<Field>(g => g.Field)
                .WithRequiredDependent(f => f.Game)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Field>()
            //    .HasRequired<Game>(f => f.Game)
            //    .WithRequiredPrincipal(g => g.Field);

            //.WillCascadeOnDelete(true);
            //modelBuilder.Entity<Field>()
            //    .HasRequired<Game>(f => f.Game)
            //    .WithRequiredPrincipal(g => g.Field)
            //    .WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

    }
}