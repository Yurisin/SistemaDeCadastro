using Microsoft.EntityFrameworkCore;

namespace FormCadastro.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opcoes)
            : base(opcoes)
        {
        }

        public DbSet<DbCadastro> DbCadastro { get; set; }

    }
}
