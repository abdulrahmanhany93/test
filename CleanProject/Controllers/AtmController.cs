using CleanProject.Data.Interfaces;
using CleanProject.Domain.DTO;
using CleanProject.Domain.Entities;
using Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtmController(AppDbContext context, IAtmRepository repository) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Atm>>> Get()
    {
        var accounts = await repository.GetAllAsync();
        return Ok(accounts);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountDto>> Get(Guid id)
    {
        var account = await repository.GetByIdAsync(id);
        if (account == null) return NotFound();
        return Ok(account);
    }


    [HttpPost]
    public async Task<ActionResult> Post(double ammount)
    {
        var newAtm = new Atm
        {
            Amount = ammount,
            CreatedAt = DateTime.Now
        };


        await repository.CreateAsync(newAtm);
        return Ok();
    }

    // PUT api/<AccountController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await repository.DeleteAsync(id);
        return Ok();
    }
}