using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;  // Certifique-se de que este namespace está correto
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace gwiBack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormationController : ControllerBase
    {
        private readonly IFormationService _formationService;

        public FormationController(IFormationService formationService)
        {
            _formationService = formationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista de formações")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista de formações", typeof(IEnumerable<Formation>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<Formation>>> Get()
        {
            var formations = await _formationService.GetAllFormationsAsync();
            return Ok(formations);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém uma formação por ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a formação encontrada", typeof(Formation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Formação não encontrada")]
        public async Task<ActionResult<Formation>> GetById(Guid id)
        {
            var formation = await _formationService.GetFormationByIdAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            return Ok(formation);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova formação")]
        [SwaggerResponse(StatusCodes.Status201Created, "Formação criada com sucesso", typeof(Formation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        public async Task<ActionResult<Formation>> Post(Formation formation)
        {
            try
            {
                var createdFormation = await _formationService.CreateFormationAsync(formation);
                return CreatedAtAction(nameof(GetById), new { id = createdFormation.Id }, createdFormation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma formação existente")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Formação atualizada com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Formação não encontrada")]
        public async Task<IActionResult> Put(Guid id, Formation formation)
        {
            if (id != formation.Id)
            {
                return BadRequest("ID da formação não corresponde ao ID na URL.");
            }

            try
            {
                await _formationService.UpdateFormationAsync(formation);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove uma formação")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Formação removida com sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Formação não encontrada")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _formationService.DeleteFormationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
