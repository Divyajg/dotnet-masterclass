var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var client = new HttpClient();

//a get method that gets a response from an endpoint
app.MapGet("/", async () =>
{
    var result = await client.GetFromJsonAsync<UsersData>("https://reqres.in/api/users?page=2");
    return result;
});

//a post method that posts to an endpoint e.g. /api/users
app.MapPost("/", async () =>
{
    var result = await client.PostAsJsonAsync("https://reqres.in/api/users", new User(18, 31, "D", "J", "G"));
    return Results.Ok(result.IsSuccessStatusCode);
});

//A POST endpoint that calls POST users/add with a record with FirstName, LastName and Age
app.MapPost("/users/add", async (User user) =>
{
    var response = await client.PostAsJsonAsync("https://dummyjson.com/users/add", user);
    var createdUser = await response.Content.ReadFromJsonAsync<User>();
    var result = new
    {
        StatusCode = response.StatusCode,
        data = createdUser,
    };
    return Results.Ok(result);
});

//A POST endpoint that that calls Post products/add with a record with Title and Price (this simulates creating a product)
app.MapPost("/products/add", async (Product product) =>
{
    var response = await client.PostAsJsonAsync("https://dummyjson.com/products/add", product);
    var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
    var result = new
    {
        StatusCode = response.StatusCode,
        data = createdProduct,
    };
    return Results.Ok(result);
});


//A POST endpoint that takes a lists of ids and retrieves all of the users with those ids from the GET users (Id, FirstName, LastName and Age)
app.MapPost("/users/list", async (IdArray Array) =>
{
    var result = await GetUsers(Array);
    return Results.Ok(result);
});

async Task<object> GetUsers(IdArray array)
{
    var userList = new List<User>();
    foreach (var id in array.ids)
    {
        var user = await GetUserById(id);
        userList.Add(user);
    }
    return userList;
}

//A POST endpoint that takes a lists of ids and retrieves all of the products with those ids GET products(Id, Title)
app.MapPost("/products/list", async (IdArray Array) =>
{
    var result = await GetProducts(Array);
    return Results.Ok(result);
});

async Task<object> GetProducts(IdArray array)
{
    var ProductList = new List<Product>();
    foreach (var id in array.ids)
    {
        var product = await GetProductById(id);
        ProductList.Add(product);
    }
    return ProductList;
}

//A GET endpoint that gets a user based on an id
app.MapGet("/users", async ( int id) =>
{
    var result = await GetUserById(id);
    return Results.Ok(result);
});

//A GET endpoint that gets a product based on an id
app.MapGet("/products", async ( int id) =>
{
    var result = await GetProductById(id);
    return Results.Ok(result);
});

//A PUT endpoint that updates a user based on an id and the body of the request
app.MapPut("/users/update", async (int id, User user) =>
{
    var response = await client.PutAsJsonAsync($"https://dummyjson.com/users/put/{id}", user);
    var UpdatedUser = await response.Content.ReadFromJsonAsync<User>();
    var result = new
    {
        StatusCode = response.StatusCode,
        data = UpdatedUser,
    };
    return response;
});

//A PUT endpoint that updates a product based on an id and the body of the request
app.MapPut("/products/update", async (int id, Product product) =>
{
    var response = await client.PutAsJsonAsync($"https://dummyjson.com/products/put/{id}", product);
    var UpdatedProduct = await response.Content.ReadFromJsonAsync<Product>();
    var result = new
    {
        StatusCode = response.StatusCode,
        data = UpdatedProduct,
    };
    return Results.Ok(result);
});

//A DELETE endpoint that deletes a user based on an id
app.MapDelete("/users/delete", async (int id) =>
{
    var response = await client.DeleteAsync($"https://dummyjson.com/users/delete/{id}");
    return response;
});

//A DELETE endpoint that deletes a product based on an id
app.MapDelete("/products/delete", async (int id) =>
{
    var response = await client.DeleteAsync($"https://dummyjson.com/products/delete/{id}");
    return response;
});

async Task<User> GetUserById(int id)
{
    var result = await client.GetAsync($"https://dummyjson.com/users/{id}");
    var response = await result.Content.ReadFromJsonAsync<User>();
    return response;
}

async Task<Product> GetProductById(int id)
{
    var result = await client.GetAsync($"https://dummyjson.com/products/{id}");
    var response = await result.Content.ReadFromJsonAsync<Product>();
    return response;
}

app.Run();


record User(int Id, int Age, string Email, string FirstName, string LastName);
record Product(string Title, decimal Price);
record IdArray(int[] ids);
record UsersData(List<User> User);