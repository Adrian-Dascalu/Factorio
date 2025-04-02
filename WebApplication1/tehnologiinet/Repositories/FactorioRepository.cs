using System.Drawing.Printing;
using tehnologiinet.Entities;
using System.Text.Json;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Http.Features;

namespace tehnologiinet.Repositories;

public class FactorioRepository: IFactorioRepository
{
    public List<Production> GetAllProduction()
    {
        return GetAllProductionFromDb();
    }

    public List<Consumption> GetAllConsumption()
    {
        return GetAllConsumptionFromDb();
    }

    public Production GetProductionById(long Id)
    {
        UpdateProduction(new Factorio());
        return GetAllProductionFromDb().FirstOrDefault(x => x.Id == Id)!;
    }

    public Consumption GetConsumptionById(long Id)
    {
        return GetAllConsumptionFromDb().FirstOrDefault(x => x.Id == Id)!;
    }

    public Production GetProductionByItem(Item Item)
    {
        return GetAllProductionFromDb().FirstOrDefault(x => x.Item == Item)!;
    }

    public Consumption GetConsumptionByItem(Item Item)
    {
        return GetAllConsumptionFromDb().FirstOrDefault(x => x.Item == Item)!;
    }

    public void UpdateProduction(Factorio updatedProduction)
    {
        var productions = LoadProductionFromJson();
        var production = GetProductionById(updatedProduction.Id);
    }

    public List<Production> LoadProductionFromJson()
    {
        var file_path = "Factorio/production-nauvis-10min.json";
        
        if (!File.Exists(file_path))
        {
            //return string empty
            string empty = "empty";
            Item item = new Item();
            item.Id = 1;
            item.Name = empty;
            //item.Recipe = null;
            Production production = new Production();
            production.Id = 1;
            production.Item = item;
            
            //return  production;
            
            //return new List<Production>(); // Return empty list if file does not exist
            
        }

        var json = File.ReadAllText(file_path);
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(json);
        var productionData = new List<Production>();

        //get every item from json (data from json is {item1:{sample1: 1, sample2:2}, item2:{sample1:1, sample2:2}})

        // test is a dictionary with the keys of the json data
        var keys = jsonData.Keys;
        //wooden chest, iron chest, steel chest, etc

        foreach(var key in keys) // wooden chest, iron chest
        {
            //get the value of the item
            var dictionary_sample = jsonData[key];
            
            var samples_keys = dictionary_sample.Keys; //id = where item_name == key

            var total_value = 0;

            foreach(var sample_key in samples_keys)
            {
                //get the value of the key
                var val = dictionary_sample[sample_key];
                //add the value to the total value
                total_value += val;
            }

            

            //create a new production object
            Production production = new Production();
            production.Id = 1;
            production.Item = new Item();
            production.Item.Id = 1;
            production.Item.Name = key;
            //production.Item.Recipe = null;
            production.Item.Value = total_value;
            
            //add the production to the list
            productionData.Add(production);
        }

        
        return productionData;
    }

    public List<Item> LoadItemsFromJson()
    {
        var file_path = "Factorio/item.json";
        
        if (!File.Exists(file_path))
        {
            return new List<Item>(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText(file_path);
        
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json);

        var itemData = new List<Item>();

        var keys = jsonData.Keys;

        foreach(var key in keys)
        {
            var dictionary_sample = jsonData[key];
            var item = new Item();
            item.Name = key;
            item.Value = 1;
            //item.Recipe = null;
            itemData.Add(item);
        }

        return itemData;
    }

// private List<Recipe> LoadRecipesFromJson()
    public Recipe LoadRecipesFromJson()
    {
        var file_path = "Factorio/recipe.json";
        
        if (!File.Exists(file_path))
        {
            return new Recipe(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText(file_path);
//         "recipe": 
//   {
//     "recipe-unknown": 
//     {
//       "type": "recipe",
//       "name": "recipe-unknown",
//       "icon": "__core__/graphics/icons/unknown.png",
//       "icon_size": 64,
//       "hidden": true,
//       "ingredients": 
//       {},
//       "results": 
//       {}
//     },
        // json data
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json);

        //search for item in json data
        var item = jsonData.FirstOrDefault(x => x.Key == "item");
        item.Value.TryGetValue("parameter-0", out var parameter0);
        
        
        var recipe = new Recipe();
        recipe.Id = 1;
        recipe.Ingredients = new List<Ingredient>();
        recipe.Value = 1;
        recipe.Ingredients.Add(new Ingredient { Id = 1, Amount = 1, ItemId = 1, RecipeId = 1 });

        return jsonData.Keys.FirstOrDefault() != null ? recipe : new Recipe(); // Return empty list if file does not exist
    }

    private List<Consumption> LoadConsumptionFromJson()
    {
        var file_path = "../Factorio/consumption-nauvis-10min.json";
        
        if (!File.Exists(file_path))
        {
            return new List<Consumption>(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText(file_path);
        return JsonSerializer.Deserialize<List<Consumption>>(json) ?? new List<Consumption>();
    }


    /*public void UpdateStudent(Student updatedStudent)
    {
        var students = LoadStudentsFromJson();
        var student = students.FirstOrDefault(s => s.Id == updatedStudent.Id);

        if (student != null)
        {
            student.Wins = updatedStudent.Wins;
            student.Losses = updatedStudent.Losses;
            SaveStudentsToJson(students); // ✅ Save changes to file
        }
    }*/

    /*private List<Student> LoadStudentsFromJson()
    {
        if (!File.Exists("students.json"))
        {
            return new List<Student>(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText("students.json");
        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }*/
    
    /*private List<Student> GetAllStudentsFromDb()
    {
        List<Student> studentsRetrieved = new List<Student>();
        
        var student1 = new Student
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            CNP = "1234567890123",
            Email = "student@student.ro",
            PhoneNumber = "0722222222",
            Address = "Str. Studentilor",
            City = "Iasi",
            Country = "Romania",
            PostalCode = "700000",
            University = "Universitatea Tehnica",
            Faculty = "Automatica si Calculatoare",
            Specialization = "Calculatoare",
            Wins = 0,
            Losses = 0,
            Username = "john_doe",
        };
        
        var student2 = new Student
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            CNP = "1234567890123",
            Email = "jane.doe@student.ucv.ro",
            PhoneNumber = "0000000000",
            Address = "Str. Studentilor nr. 1",
            City = "Craiova",
            Country = "Romania",
            PostalCode = "700000",
            University = "Universitatea Tehnica",
            Faculty = "Automatica si Calculatoare",
            Specialization = "Calculatoare",
            Wins = 0,
            Losses = 0,
            Username = "jane_doe",
        };
        
        var student3 = new Student
        {
            Id = 3,
            FirstName = "Jane",
            LastName = "Popescu",
            CNP = "1234567890123",
            Email = "jane.popescu@student.ucv.ro",
            PhoneNumber = "0000000000",
            Address = "Str. Studentilor nr. 3",
            City = "Craiova",
            Country = "Romania",
            PostalCode = "700000",
            University = "Universitatea Tehnica",
            Faculty = "Automatica si Calculatoare",
            Specialization = "Automatica si Informatica Aplicata",
            Wins = 0,
            Losses = 0,
            Username = "jane_popescu",
        };
        
        studentsRetrieved.Add(student1);
        studentsRetrieved.Add(student2); 
        studentsRetrieved.Add(student3);

        if (!File.Exists("students.json"))
            SaveStudentsToJson(studentsRetrieved);
        else
        {
            studentsRetrieved = LoadStudentsFromJson();
        }
        
        return studentsRetrieved;
    }*/

    private List<Production> GetAllProductionFromDb()
    {
        using(var db = new AppDbContext())
        {
            return db.Productions.ToList();
        }
    }

    private List<Consumption> GetAllConsumptionFromDb()
    {
        using(var db = new AppDbContext())
        {;
            return db.Consumptions.ToList();
        }
    }
}