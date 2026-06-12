using FieldServiceTracker.API.DTOs;
using FieldServiceTracker.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FieldServiceTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _service;

        public ServiceRequestsController(IServiceRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceRequestResponseDto>>> GetAll([FromQuery] string? status, [FromQuery] string? priority)
        {
            var serviceRequests = await _service.GetAllAsync(status, priority);
            return Ok(serviceRequests);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceRequestResponseDto>> GetById(int id)
        {
            var serviceRequest = await _service.GetByIdAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return Ok(serviceRequest);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRequestResponseDto>> Create(CreateServiceRequestDto createDto)
        {
            var created = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServiceRequestResponseDto>> Update(int id, UpdateServiceRequestDto updateDto)
        {
            var updated = await _service.UpdateAsync(id, updateDto);
            return Ok(updated);
        }

        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<ServiceRequestResponseDto>> PatchStatus(int id, PatchStatusDto patchDto)
        {
            var updated = await _service.PatchStatusAsync(id, patchDto);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

    }
}
