using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace SmartSchool_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController :  ControllerBase
    {
        private readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }  
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllAlunosAsync(false);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet("{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            try
            {
                var result = await _repo.GetAlunoAsyncById(alunoId, true);

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet("ByDisciplina/{disciplinaId}")]
        public async Task<IActionResult> GetByDisciplinaId(int disciplinaId)
        {
            try
            {
                var result = await _repo.GetAlunosAsyncByDisciplinaId(disciplinaId, false);
                return Ok(result);
            }            
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aluno model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                } 
            }            
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{alunoId}")]
        public async Task<IActionResult> put(int alunoId, Aluno model)
        {
            try
            {
                var aluno = await _repo.GetAlunoAsyncById(alunoId, false);
                if(aluno == null)
                {
                    return NotFound();
                }

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                } 
            }            
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{alunoId}")]
        public async Task<IActionResult> delete(int alunoId)
        {
            try
            {
                var aluno =await _repo.GetAlunoAsyncById(alunoId, false);
                if(aluno == null)
                {
                    return NotFound();
                }
                
                _repo.Delete(aluno);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(new {message = "Deletado"});
                } 
            }            
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest();
        }

    }
}
