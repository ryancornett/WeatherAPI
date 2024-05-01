using System.Text.Json;

using (HttpClient client = new HttpClient())
{
    try
    {
        string apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,is_day&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch";

        HttpResponseMessage response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ResponseObject responseObject = JsonSerializer.Deserialize<ResponseObject>(responseBody);

        Console.WriteLine($"Temperature: {responseObject.current.temperature_2m}");
        Console.WriteLine($"Is Day: {responseObject.current.is_day}");
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Request error: {e.Message}");
    }
}


public class CurrentUnits
{
    public string time { get; set; }
    public string interval { get; set; }
    public string temperature_2m { get; set; }
    public string is_day { get; set; }
}

public class Current
{
    public string time { get; set; }
    public int interval { get; set; }
    public double temperature_2m { get; set; }
    public int is_day { get; set; }
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

