using AlunoApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Renan Osório",
                    Email = "Renan@email.com",
                    Idade = 28
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Wagner Rodrigues",
                    Email = "Wagner@email.com",
                    Idade = 30
                }
            );

            base.OnModelCreating(modelBuilder);
        }

       public DbSet<Aluno> Aluno { get; set; }
    }
}
