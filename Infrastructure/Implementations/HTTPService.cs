using System.Text;
using System.Text.Json;
using Application.Interfaces;

namespace Infrastructure.Implementations;

public class HTTPService : IRequest
{
    private readonly HttpClient _httpClient;
    public HTTPService()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<Stream> Fetch(string url, object payload)
    {
        var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );
        using var response = await _httpClient.PostAsync(url, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStreamAsync();
        }

        throw new HttpRequestException($"Ошибка HTTP запроса в {response}");
    }

    public void AddHeader(Dictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}