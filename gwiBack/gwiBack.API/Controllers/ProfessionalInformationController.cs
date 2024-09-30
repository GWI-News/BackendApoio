using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace gwiBack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalInformationController : ControllerBase
    {
        private readonly IProfessionalInformationService _professionalInformationService;

        public ProfessionalInformationController(IProfessionalInformationService professionalInformationService)
        {
            _professionalInformationService = professionalInformationService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém as informações profissionais por ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna as informações profissionais encontradas", typeof(ProfessionalInformation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Informações profissionais não encontradas")]
        public async Task<ActionResult<ProfessionalInformation>> GetById(Guid id)
        {
            var information = await _professionalInformationService.GetProfessionalInformationByIdAsync(id);
            if (information == null)
            {
                return NotFound();
            }
            return Ok(information);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Registra novas informações profissionais")]
        [SwaggerResponse(StatusCodes.Status201Created, "Informações profissionais registradas com sucesso", typeof(ProfessionalInformation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        public async Task<ActionResult<ProfessionalInformation>> Post(ProfessionalInformation information)
        {
            try
            {
                var createdInformation = await _professionalInformationService.CreateProfessionalInformationAsync(information);
                return CreatedAtAction(nameof(GetById), new { id = createdInformation.Id }, createdInformation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza as informações profissionais")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Informações profissionais atualizadas com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Informações profissionais não encontradas")]
        public async Task<IActionResult> Put(Guid id, ProfessionalInformation information)
        {
            if (id != information.Id)
            {
                return BadRequest("ID da informação profissional não corresponde ao ID na URL.");
            }

            try
            {
                await _professionalInformationService.UpdateProfessionalInformationAsync(information);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove as informações profissionais")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Informações profissionais removidas com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Informações profissionais não encontradas")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _professionalInformationService.DeleteProfessionalInformationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
