using CleanProject.Domain.DTO;
using CleanProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    IAccountService service) : ControllerBase
{
    private readonly IAccountService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> Get()
    {
        var result = await _service.AllAccountAsync();
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountDto>> Get(Guid id)
    {
        var result = await _service.GetAccountByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }


    [HttpPost]
    public async Task<ActionResult> Post(CreateAccountDto dto)
    {
        var result = await _service.CreateAccountAsync(dto);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    // PUT api/<AccountController>/5
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UpdateAccountDto updateAccountDto)
    {
        if (updateAccountDto.Id != id) return BadRequest();
        var result = await _service.UpdateAccountAsync(updateAccountDto);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAccountAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }
}