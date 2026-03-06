using Newtonsoft.Json.Linq;

namespace Portfolio.Services;

public class LocationService
{
    public async Task<string> GetLocationAsync(string ipAddress)
    {
        try
        {

            using HttpClient client = new HttpClient();
            string url = $"https://ipinfo.io/{ipAddress}/json";
            var response = await client.GetStringAsync(url);
            var json = JObject.Parse(response);
            string location = $"{json["city"]}, {json["region"]}, {json["country"]}";

            return location;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching location: " + ex.Message);
            return "Unknown";
        }
    }
}
