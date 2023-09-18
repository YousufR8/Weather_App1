using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Enter the name of the city:");
        string cityName = Console.ReadLine();

        try
        {
            string apiKey = "YOUR_API_KEY";
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                    
                    Console.WriteLine($"City: {weatherData.Name}");
                    Console.WriteLine($"Temperature: {weatherData.Main.Temp}°C");
                    Console.WriteLine($"Weather: {weatherData.Weather[0].Description}");
                }
                else
                {
                    Console.WriteLine("Error fetching weather data.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

class WeatherData
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public WeatherInfo[] Weather { get; set; }
}

class MainData
{
    public float Temp { get; set; }
}

class WeatherInfo
{
    public string Description { get; set; }
}
