using CleanProject.Data;
using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using CleanProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(AppDbContext context, IClientServices services) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
    {
        var result = await services.AllClientAsync();
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> Get(Guid id)
    {
        var result = await services.GetClientByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }


    [HttpPost]
    public async Task<ActionResult> Post(ClientDto client)
    {
        var result = await services.CreateClientAsync(client);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    // PUT api/<AccountController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Client client, Guid id)
    {
        if (id != client.Id) return BadRequest("Ids do not match");
        var result = await services.UpdateClientAsync(client);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await services.DeleteClientAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    [HttpPost(nameof(Withdraw))]
    public async Task<ActionResult<AccountDto>> Withdraw([FromBody] ClientWithdrawDto dto)
    {
        var result = await services.Withdraw(dto);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }

    [HttpPost(nameof(Deposit))]
    public async Task<ActionResult<AccountDto>> Deposit(ClientDepositDto dto)
    {
        var result = await services.Deposit(dto);
        if (result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }
}