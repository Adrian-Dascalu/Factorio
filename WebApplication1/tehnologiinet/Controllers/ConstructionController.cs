using System.Drawing.Printing;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using tehnologiinet.Repositories;
using tehnologiinet.Entities;
using Microsoft.AspNetCore.Authorization;
//using tehnologiinet.Models;

namespace tehnologiinet.Controllers;

[Route("Factorio")]
[ApiController]
[EnableCors]
public class ConstructionController : ControllerBase
{
    private readonly IFactorioRepository _factorioRepository;

    // connect to database
    private readonly AppDbContext _context;

    // Injecting repository via constructor
    public ConstructionController(IFactorioRepository factorioRepository, AppDbContext context)
    {
        _factorioRepository = factorioRepository;
        _context = context;
    }

    [HttpPost("AddToConstruction/{id}")]
    [Authorize]
    public IActionResult AddToConstruction(long id)
    {
        var item = _factorioRepository.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }

        // Check if the item is already in construction
        var existingItem = _context.Constructions.FirstOrDefault(c => c.ItemId == id);
        if (existingItem != null)
        {
            return BadRequest("Item is already in construction.");
        }
        
        // Create a new construction entry
        var construction = new Construction
        {
            ItemId = item.Id,
            Name = item.Name,
            TotalQuantity = 1, 
        };

        _context.Constructions.Add(construction);
        _context.SaveChanges();


        return Ok(item);
    }
}