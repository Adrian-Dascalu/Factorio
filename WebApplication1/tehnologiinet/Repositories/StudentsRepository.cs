using tehnologiinet.NewDirectory1;
using System.Text.Json;
using System.Runtime.Versioning;

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

    public Factorio GetProductionById(long Id)
    {
        return GetAllProductionFromDb().FirstOrDefault(x => x.Id == Id)!;
    }

    public Factorio GetConsumptionById(long Id)
    {
        return GetAllConsumptionFromDb().FirstOrDefault(x => x.Id == Id)!;
    }

    public Factorio GetProductionByItem(string item)
    {
        return GetAllProductionFromDb().FirstOrDefault(x => x.Item == item)!;
    }

    public Factorio GetConsumptionByItem(string item)
    {
        return GetAllConsumptionFromDb().FirstOrDefault(x => x.Item == item)!;
    }

    

    private void SaveStudentsToJson(List<Student> students)
    {
        var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

        
        File.WriteAllText("students.json", json);
    }

    public void UpdateStudent(Student updatedStudent)
    {
        var students = LoadStudentsFromJson();
        var student = students.FirstOrDefault(s => s.Id == updatedStudent.Id);

        if (student != null)
        {
            student.Wins = updatedStudent.Wins;
            student.Losses = updatedStudent.Losses;
            SaveStudentsToJson(students); // ✅ Save changes to file
        }
    }

    private List<Student> LoadStudentsFromJson()
    {
        if (!File.Exists("students.json"))
        {
            return new List<Student>(); // Return empty list if file does not exist
        }

        var json = File.ReadAllText("students.json");
        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }
    
     private List<Student> GetAllStudentsFromDb()
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
        }
}