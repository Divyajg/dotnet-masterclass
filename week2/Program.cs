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
        return "Invalid inputs";
    }

    if(operation == "add")
    {
        return $"The addition of {a} and {b} is {parseda+parsedb}";
    }
    else if (operation == "sub")
    {
        return $"The substraction of {a} and {b} is {parseda-parsedb}";
    }
    else if (operation == "multiply")
    {
        return $"The multiplication of {a} and {b} is {parseda*parsedb}";
    } 
    else{
        return "Invalid Operator";
    }
});

app.MapGet("/excercise2", (string input) =>
{
    var inputIsValid = int.TryParse(input, out var parsedInput);

string AddNumbers(int parsedInput)
{
     var count=0;
   
    while (parsedInput > 0)
    {
     count = count + parsedInput % 10;
     parsedInput = parsedInput / 10;
    }
    
    return $"The count of {input} is {count}";
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
    List<char> no_repeats = new List<char>();
          
    foreach (char letter in sentence)
    {  
        unique.Add(letter);
    }   

    no_repeats = unique.Distinct().ToList();
    no_repeats.Sort(); 

    return no_repeats;
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
