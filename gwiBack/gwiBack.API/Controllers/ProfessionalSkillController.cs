using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace gwiBack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalSkillController : ControllerBase
    {
        private readonly IProfessionalSkillService _professionalSkillService;

        public ProfessionalSkillController(IProfessionalSkillService professionalSkillService)
        {
            _professionalSkillService = professionalSkillService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista de habilidades profissionais")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista de habilidades profissionais", typeof(IEnumerable<ProfessionalSkill>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<ProfessionalSkill>>> Get()
        {
            var skills = await _professionalSkillService.GetAllProfessionalSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém uma habilidade profissional por ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a habilidade profissional encontrada", typeof(ProfessionalSkill))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Habilidade profissional não encontrada")]
        public async Task<ActionResult<ProfessionalSkill>> GetById(Guid id)
        {
            var skill = await _professionalSkillService.GetProfessionalSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova habilidade profissional")]
        [SwaggerResponse(StatusCodes.Status201Created, "Habilidade profissional criada com sucesso", typeof(ProfessionalSkill))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        public async Task<ActionResult<ProfessionalSkill>> Post(ProfessionalSkill skill)
        {
            try
            {
                var createdSkill = await _professionalSkillService.CreateProfessionalSkillAsync(skill);
                return CreatedAtAction(nameof(GetById), new { id = createdSkill.Id }, createdSkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma habilidade profissional")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Habilidade profissional atualizada com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Habilidade profissional não encontrada")]
        public async Task<IActionResult> Put(Guid id, ProfessionalSkill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest("ID da habilidade não corresponde ao ID na URL.");
            }

            try
            {
                await _professionalSkillService.UpdateProfessionalSkillAsync(skill);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove uma habilidade profissional")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Habilidade profissional removida com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Habilidade profissional não encontrada")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _professionalSkillService.DeleteProfessionalSkillAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
