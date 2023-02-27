var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "This is the homework for week1!");

//task1
app.MapGet("/string-manipulation", (string input) => {
string reversed = ReverseString(input); 
string ReverseString( string s )
{
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}
return $"Reversed input value {input}: {reversed}";
// For input = "world", response: "dlrow"
});

//task2
app.MapGet("/string-math", (string input) => {
    string sentence=input.ToLower();
int vowelCount = GetVowelCount(sentence); 

int GetVowelCount( string s)
{
    int total=0;
for (int i = 0; i < s.Length; i++)
    {
        if (s[i]  == 'a' || s[i] == 'e' || s[i] == 'i' || s[i] == 'o' || s[i] == 'u')
        {
            total++;
        }
    }
       return total;
}
return $"Vowel count in the input value {input}: {vowelCount}";
// For input = "Intellectualization", vowelCount: "9"
});

//task3
app.MapGet("/math-array", () =>{
int[] arr = new[] { 271, -3, 1, 14, -100, 13, 2, 1, -8, -59,  -1852, 41, 5 };
int[] result = GetResult(arr);

    int[] GetResult(int[] arr) 
{
    var newArray = new int[2];
    var positiveResult=1;
    var negativeResult=0;
    
    for(int i = 0; i < arr.Length; i++)
    {
        if(arr[i] < 0)
        {
            negativeResult += arr[i];
        }
        else{
            positiveResult *= arr[i];
        }
    }
    newArray[0] = negativeResult;
    newArray[1] = positiveResult;
    
    return newArray;
}
return $"Sum of negative numbers: {result[0]}. Multiplication of positive numbers: {result[1]}";
});

//task4
app.MapGet("/fibo", (int n) => 
{
int nthNumber = Fibonacci(n); 
int Fibonacci(int number)
{
 int firstnumber = 0, secondnumber = 1, result = 0;
if (n == 0) return 0; 
if (n == 1) return 1; 
for (int i = 2; i<= n; i++) 
{
result = firstnumber + secondnumber;
firstnumber = secondnumber;
secondnumber = result;
}
return result;
}
return $"{n}th fibonacci number is: {nthNumber}";
});

app.Run();
