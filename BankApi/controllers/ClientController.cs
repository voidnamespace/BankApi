using BankApi.DTOs;
using BankApi.Entities;
using BankApi.Enums;
using BankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> GetAll(int page = 1, int pageSize = 20)
        {
            var clients = await _clientService.GetAllAsync(page, pageSize);

            if (!clients.Any())
                return NotFound("Клиенты не найдены");

            return Ok(clients);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetById(Guid id)
        {
            var getClient = await _clientService.GetByIdAsync(id);

            var userIdClaim = User.FindFirst("id")?.Value;
            var userRole = User.FindFirst("role")?.Value;

            if (userRole != "Admin" && userIdClaim != getClient.Id.ToString())
                return Forbid(); 

            return Ok(getClient);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Post(PostClientDTO postClientDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postClient = await _clientService.PostAsync(postClientDTO);

            return CreatedAtAction(nameof(GetById), new { id = postClient.Id }, postClient);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClientDTO>> Put(Guid id, PutClientDTO putClientDTO)
        {
            var userRole = User.FindFirst("role")?.Value ?? "User";

            var updatedClient = await _clientService.PutAsync(id, putClientDTO, userRole);

            return Ok(updatedClient);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete (Guid id)
        {
            try
            {
                await _clientService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
