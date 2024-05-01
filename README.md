# WeatherAPI

## STUDENTS: Try this before looking at the finished code

Go to https://open-meteo.com/en/docs and familiarize yourself with the UI.

### CUSTOMIZING THE ENDPOINT
1. Change to your desired timezone (beside the longitude field)
2. This walkthrough will help you get just the current weather conditions, so uncheck everything under 'Hourly Weather Variables' for now
3. Under 'Current Weather' select 'Temperature (2 m)' and 'Is Day or Night'
4. Change any units under 'Settings' you'd like
5. Beneath the chart under 'API Response' is your custom API URL, or endpoint, based on your options. Paste it in a new tab to see the response (JSON). Newer versions of Chrome even let you format it nicely for easier reading, or you can use Postman or jsonviewer.stack.hu for reformatting

### CALLING THE API AND CREATING CLASSES FOR DESERIALIZATION
1. Paste the following code into Program.cs:
```
using (HttpClient client = new HttpClient())
{
    try
    {
        string apiUrl = "";

        HttpResponseMessage response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ResponseObject responseObject = JsonSerializer.Deserialize<ResponseObject>(responseBody);

        Console.WriteLine($"Temperature: {responseObject.current.temperature_2m}");
        Console.WriteLine($"Is Day: {responseObject.current.is_day}");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Request error: {ex.Message}");
    }
}

// Classes must be structured match the data structure of the API response
public class CurrentUnits
{
    public string time { get; set; }
    public string interval { get; set; }
    public string temperature_2m { get; set; }
    public string is_day { get; set; }
}

public class Current
{

}

public class ResponseObject
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public double elevation { get; set; }
    public CurrentUnits current_units { get; set; } = new();
    public Current current { get; set; } = new();
}
```

2. Add your open-meteo.com endpoint as the value of the `string apiUrl` variable
3. Complete the `Current` class based on the "current" object in the API response; be sure to match Property types

The program should now run successfully. If not, see the code in this repository for hints.

### IMPROVEMENTS
1. Move classes to their own files
2. Instead of printing the value of Current.is_day, use the data to customize a greeting for users
3. Our class properties need to abide by the naming convention, but renaming them now would break deserialization. See this page for a fix: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/customize-properties?pivots=dotnet-8-0
4. Allow users to check the current temperature in several different places. Use https://www.latlong.net/ and a Dictionary<TKey, TValue> to get latitude and longitude variables and dynamically add them to the apiUrl string. Consider what data types make the most sense for the Dictionary (do we need a custom type for TValue?)
5. Add weather codes to your endpoint. Use the API documentation and a Dictionary to tell users the current weather outlook