using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using FormularioEleicao.Dominio.Formularios;

namespace FormularioEleicao.Infraestrutura
{
    public class FormularioEleicaoDbContext : DbContext
    {
        public FormularioEleicaoDbContext(DbContextOptions<FormularioEleicaoDbContext> options)
        : base(options)
        {
        }

        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Formulario> Formularios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pergunta>(e =>
                {
                    e.HasKey(e => e.Id);
                }
            );

            modelBuilder.Entity<Formulario>(e =>
                {
                    e.HasKey(e => e.Id);

                    e.OwnsOne(e => e.Candidato);
                    e.OwnsOne(e => e.Entrevistado);
                    e.OwnsMany(e => e.Respostas);
                }
            );
        }
    }
}
