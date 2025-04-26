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
        var productions = _factorioRepository.LoadProductionFromJson();
        
        using (var db = new AppDbContext())
        {
            foreach (var production in productions)
            {
                var exists = db.Productions.Any(p => p.Id == production.Id);
                if (!exists)
                {
                    db.Productions.Add(production);
                }
            }

            db.SaveChanges();
        }

        return Ok(productions);
    }

    [HttpGet("LoadConsumtionFromJson")]
    public IActionResult LoadConsumption()
    {
        var consumptions = _factorioRepository.LoadConsumptionFromJson();
        
        using (var db = new AppDbContext())
        {
            foreach (var consumption in consumptions)
            {
                var exists = db.Consumptions.Any(c => c.Id == consumption.Id);
                if (!exists)
                {
                    db.Consumptions.Add(consumption);
                }
            }

            db.SaveChanges();
        }

        return Ok(consumptions);
    }

    [HttpGet("LoadRecipesFromJson")]
    public IActionResult LoadRecipes()
    {
        var recipes = _factorioRepository.LoadRecipesFromJson();

        using (var db = new AppDbContext())
        {
            foreach (var recipe in recipes)
            {
                var exists = db.Recipes.Any(r => r.Id == recipe.Id);
                if (!exists)
                {
                    db.Recipes.Add(recipe);
                }
            }

            db.SaveChanges();
        }

        return Ok(recipes);
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