using ms_cards.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ms_cards.Controllers;

[ApiController]
[Route("[controller]")]
public class TarjetasController : ControllerBase
{
    private readonly AppDbContext _db;

    public TarjetasController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Tarjetas.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tarjeta = await _db.Tarjetas.FindAsync(id);
        return tarjeta == null ? NotFound() : Ok(tarjeta);
    }
}