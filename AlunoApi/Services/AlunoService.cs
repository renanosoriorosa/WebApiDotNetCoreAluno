using AlunoApi.Context;
using AlunoApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Aluno
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!String.IsNullOrWhiteSpace(nome))
            {
                alunos = await _context.Aluno
                    .Where(obj => obj.Nome.Contains(nome))
                    .ToListAsync();
            }
            else
            {
                alunos = await GetAlunos();
            }

            return alunos;
        }
        public async Task<Aluno> GetAluno(int id)
        {
            return await _context.Aluno.FindAsync(id);
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Add(aluno);
            await _context.SaveChangesAsync();
        }
        public async Task EditAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
