using System.Drawing.Printing;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using tehnologiinet.Repositories;
using tehnologiinet.Entities;
//using tehnologiinet.Models;

namespace tehnologiinet.Controllers;

[Route("Factorio")]
[ApiController]
[EnableCors]
public class GamesController : ControllerBase
{
    
    private readonly IFactorioRepository _factorioRepository;

    // connect to database
    private readonly AppDbContext _context;

    // Injecting repository via constructor
    public GamesController(IFactorioRepository factorioRepository, AppDbContext context)
    {
        _factorioRepository = factorioRepository;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetGamePage()
    {
        // Serve the firstpage.html file from wwwroot
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/tictactoe", "firstpage.html"), "text/html");
    }

    [HttpGet("LoadProductionFromJson")]
    public IActionResult LoadProduction()
    {
        return Ok(_factorioRepository.LoadProductionFromJson());
    }

    [HttpGet("LoadRecipesFromJson")]
    public IActionResult LoadRecipes()
    {
        return Ok(_factorioRepository.LoadRecipesFromJson());
    }

    [HttpGet("LoadItemsFromJson")]
    public IActionResult LoadItems()
    {
        using (var db = new AppDbContext())
        {
            foreach (var item in _factorioRepository.LoadItemsFromJson())
            {
                    db.Items.Add(item);
            }

            db.SaveChanges();
        }
        return Ok(_factorioRepository.LoadItemsFromJson());
    }


    /*[HttpGet]
    public IActionResult FilterItemsByType([FromQuery] string type)
    {
        var items = _factorioRepository.FilterItemsByType(type);
        if (items.Count == 0)
        {
            return NotFound();
        }
        return Ok(items);
    }*/

    //[HttpPost("update-result")]
    //public IActionResult UpdateResult([FromBody] MatchResult result)
    //{
       // Console.WriteLine("Received update-result request");

       // var factorio = _factorioRepository.GetFactorioByName(result.Name!);
        
        //if (student == null)
        //{
            //return NotFound("Student not found");
        //}

        //_studentsRepository.UpdateStudent(student); // Ensure this updates the data source

       // return Ok(User);
    //}

    // DTO to match the expected JSON body
    public class MatchResult
    {
        public string? Username { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
    }
}