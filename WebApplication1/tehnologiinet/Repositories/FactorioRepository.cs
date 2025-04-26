using System.Drawing.Printing;
using tehnologiinet.Entities;
using System.Text.Json;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Linq;


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

    public Item GetItemById(long Id)
    {
        return GetAllItemsFromDb().FirstOrDefault(x => x.Id == Id)!;
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

    public Item GetItemByName(string name)
    {
        return GetAllItemsFromDb().FirstOrDefault(x => x.Name == name)!;
    }

    public long GetItemIdByName(string name)
    {
        using (var db = new AppDbContext())
        {
            var item = db.Items.FirstOrDefault(x => x.Name == name);
            if (item != null)
            {
                return item.Id;
            }
            else
            {
                // Handle the case where the item is not found
                return -1; // or throw an exception, or return a default value
            }
        }
    }

// private List<Recipe> LoadRecipesFromJson()
    public List<Recipe> LoadRecipesFromJson()
    {
        var file_path = "Factorio/recipe.json";
        
        if (!File.Exists(file_path))
        {
            //return string empty
            string empty = "empty";
            Recipe recipe = new Recipe();
            recipe.Id = 1;
            recipe.ItemId = 0;
            recipe.Ingredients = new List<Ingredient>();
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 1;
            ingredient.Amount = 1;
            ingredient.ItemId = 1;
            ingredient.RecipeId = 1;
            //return new List<Production>(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText(file_path);
        var jsonData = JObject.Parse(json);

        var recipeData = new List<Recipe>();
        //var ingredients = new List<Ingredient>();

        var recipeId = 1;
        var ingredientId = 1;

        var itemChace = new Dictionary<string, Item>();
        var recipeChace = new Dictionary<string, Recipe>();
        
        foreach (var topLevelKey in jsonData.Properties())
        {
            var recipeObject = topLevelKey.Value;
            var recipeName = topLevelKey.Name;

            var ingrToken = recipeObject["ingredients"];
            
            var ingredients = new List<Ingredient>(); // 👈 RESET ingredients for each recipe

            if (ingrToken is JArray ingrArray)
            {
                foreach (var ingr in ingrArray)
                {
                    var itemName = (string)ingr["name"];

                    if (!itemChace.ContainsKey(itemName))
                    {
                        var item = GetItemByName(itemName);
                        if (item == null)
                        {
                            Console.WriteLine($"Item '{itemName}' not found in database.");
                            continue;
                        }
                        itemChace[itemName] = item;
                    }

                    var itemId = itemChace[itemName].Id;

                    var ingredient = new Ingredient
                    {
                        Id = ingredientId++,
                        ItemId = itemId,
                        Amount = (int)ingr["amount"]
                    };

                    ingredients.Add(ingredient);
                }
            }

            var resultsToken = recipeObject["results"];
            if (resultsToken is JArray resultsArray)
            {
                foreach (var res in resultsArray)
                {
                    if (!itemChace.ContainsKey(recipeName))
                    {
                        var item = GetItemByName(recipeName);
                        if (item == null)
                        {
                            Console.WriteLine($"Item '{recipeName}' not found in database.");
                            continue;
                        }

                        itemChace[recipeName] = item;
                    }

                    var recipe = new Recipe
                    {
                        Id = recipeId++,
                        ItemId = itemChace[recipeName].Id,
                        Ingredients = ingredients,
                        Amount = (int)res["amount"]
                    };

                    foreach (var ingr in recipe.Ingredients)
                    {
                        ingr.RecipeId = recipe.Id;
                    }

                    recipeData.Add(recipe);
                }
            }

    /*        // This part is commented out because it seems to be incomplete and not used in the current context
            // You can uncomment and modify it as needed
            foreach (var property in dictionary_sample.EnumerateObject())
            {

                if (property.Name == "ingredients")
                {
                    foreach (var ingrElement in property.Value.EnumerateArray())
                    {
                        //Console.WriteLine(ingrElement.ToString());
                        var itemName = ingrElement.EnumerateObject().First(p => p.Name == "name").Value.GetString();
                        var itemId = GetItemIdByName(itemName);
                        
                        Console.WriteLine(property.Value);
                        
                        var recipe = new Recipe
                        {
                            Id = recipeId++,
                            ItemId = itemId,
                            Ingredients = new List<Ingredient>()
                        };

                        recipeData.Add(recipe);
                    }

                    using (var db = new AppDbContext())
                    {
                        foreach (var ingr in ingredients)
                        {
                            db.Ingredients.Add(ingr);
                        }

                        db.SaveChanges();
                    }
                }
                else if (property.Name == "results")
                {
                    foreach (var resElement in property.Value.EnumerateArray())
                    {
                        var recipe = new Recipe
                        {
                            Amount = resElement.EnumerateObject().First(p => p.Name == "amount").Value.GetInt32()
                        };

                        recipeData.Add(recipe);
                    }
                }
            }*/
        }

        return recipeData;
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
        {
            return db.Consumptions.ToList();
        }
    }

    private List<Item> GetAllItemsFromDb()
    {
        using(var db = new AppDbContext())
        {
            return db.Items.ToList();
        }
    }

    public Consumption GetConsumptionByItem(Item Item)
    {
        throw new NotImplementedException();
    }
}