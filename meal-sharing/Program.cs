var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => 
{   

});

app.Run();

public interface IMealService
{
    void AddMeal(Meal meal);
    List<Meal> ListMeals();
}
public class Meal
{
    public string Headline { get; set;}
    public string Url { get; set;}
    public string Body { get; set;}
    public string Location { get; set;}
    public int Price { get; set;}
}

public class FileMealService : IMealService
{
    public void AddMeal(Meal meal)
    {
        var meals = new List<Meal>();
        var meal1 = new Meal 
        {
            Headline = "headline", 
            Url = "url", 
            Body = "body", 
            Location = "location", 
            Price = 89
        };
        meals.Add(meal1);
        
        var mealJson = System.Text.Json.JsonSerializer.Serialize(meal1);

        File.WriteAllText("meals.json", mealJson);
    }

    public List<Meal> ListMeals()
    {
        var Meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText("meals.json"));
        return Meals;
    }
}

