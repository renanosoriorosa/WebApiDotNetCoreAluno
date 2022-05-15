using AlunoApi.Model;
using AlunoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")] // define o formato dos dados que a api vai responder. O padrão é json 
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)] Serve para definir quais os codigos de erros http a action podera retornar, usa so se quiser.
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();

                return Ok(alunos);
            }
            catch (Exception)
            {
                //exemplos
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError ,"Erro ao obter alunos");
            }
        }

        [HttpGet("GetAlunosByName")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);

                if (alunos == null)
                    return NotFound($"Não existem alunos com o critério {nome}.");

                return Ok(alunos);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com o id {id}.");

                return Ok(aluno);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);

                //retorna 201 
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);

            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (id == aluno.Id)
                {
                    await _alunoService.EditAluno(aluno);
                    return Ok($"Aluno do id {id} foi alterado cm sucesso.");
                }
                else
                {
                    return BadRequest("dados inconsistente");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno do id {id} foi deletado com sucesso.");
                }
                else
                {
                    return NotFound($"Aluno com o id {id} não encontrado.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
