using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
var app = builder.Build();

app.MapGet("/", ([FromServices] IMealService mealSharingService) =>
{
    var result = mealSharingService.ListMeals();
    return result;
});

app.MapPost("/", ([FromServices] IMealService mealSharingService, Meal meal) =>
{
    mealSharingService.AddMeal(meal);
});

app.Run();

public interface IMealService
{
    void AddMeal(Meal meal);
    List<Meal> ListMeals();
}
public class Meal
{
    public string Headline { get; set; }
    public string Url { get; set; }
    public string Body { get; set; }
    public string Location { get; set; }
    public int Price { get; set; }
}

public class FileMealService : IMealService
{
    public void AddMeal(Meal meal)
    {
        if (!File.Exists("meals.json"))
        {
            File.WriteAllText("meals.json", "[]");
        }
        var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText(@"meals.json"));

        meals.Add(meal);
        File.WriteAllText("meals.json", System.Text.Json.JsonSerializer.Serialize(meals));
    }

    public List<Meal> ListMeals()
    {
        var Meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText("meals.json"));
        return Meals;
    }
}

