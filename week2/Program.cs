var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Class excercises:
app.MapGet("/excercise1", (string a, string b, string operation) =>
{
    var IsaValid = int.TryParse(a, out var parseda);
    var IsbValid = int.TryParse(b, out var parsedb);
    
    if(!IsaValid || !IsbValid)
    {
        Results.BadRequest("Invalid inputs") ;
    }

    if(operation == "add")
    {
        Results.Ok($"The addition of {a} and {b} is {parseda+parsedb}");
    }
    else if (operation == "sub")
    {
        Results.Ok($"The substraction of {a} and {b} is {parseda-parsedb}");
    }
    else if (operation == "multiply")
    {
        Results.Ok($"The multiplication of {a} and {b} is {parseda*parsedb}");
    } 
    else
    {
        Results.BadRequest("Invalid Operator");
    }
});

string AddNumbers(int parsedInput)
{
     var count=0;
   
    while (parsedInput > 0)
    {
     count = count + parsedInput % 10;
     parsedInput = parsedInput / 10;
    }
    
    return $"The count of {parsedInput} is {count}";
}

string CountCapitalLetters(string input)
{
    var count=0;

    foreach (var character in input)
    {
        if(char.IsUpper(character))
        {
            count++;
        }
    }
    return $"The number of capital letters in {input} is {count}";
}

app.MapGet("/excercise2", (string input) =>
{
    var inputIsValid = int.TryParse(input, out var parsedInput);


    if(inputIsValid)
    {
         return AddNumbers(parsedInput);
    }
    else{
        return CountCapitalLetters(input);
    }
    
});

app.MapGet("/excercise3", () =>
{
    string input = "The cool breeze whispered through the trees";
    string sentence = input.Replace(" ", "").ToLower();
    
    var unique =  new List<char>(); 
          
    foreach (char letter in sentence)
    {  
        unique.Add(letter);
    }   

    unique.Distinct().ToList();
    unique.Sort(); 

    return unique;
});

//Homework excercise:
app.MapGet("/excercise4", () => 
{
    string input = "The quick brown fox Jumps over the lazy dog";

    var words = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var frequencyDict = new Dictionary<string, int>();
    
    foreach (var word in words)
    {
        if (frequencyDict.ContainsKey(word))
        {
            frequencyDict[word]++;
        }
        else
        {
            frequencyDict[word] = 1;
        }
    }

    return frequencyDict;
});

app.Run();
