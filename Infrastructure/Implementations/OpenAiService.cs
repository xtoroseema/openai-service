using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DTOs;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Implementations;

using Domain.Interfaces.Services;

public class OpenAiService : IAiService
{
    private readonly IConfiguration _configuration;
    private readonly IRequest _requestService;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly string _assistantId;
    public OpenAiService(IConfiguration configuration, IRequest requestService)
    {
        _requestService = requestService;
        _configuration = configuration;
        _assistantId = _configuration["OpenAI:AssistantId"];
        _apiKey = _configuration["OpenAI:ApiKey"];
        _baseUrl = _configuration["OpenAI:BaseUrl"];
    }

    public async Task<Message> Generate(Message message)
    {
        _requestService.AddHeader(new Dictionary<string, string>() {{"Authorization", $"Bearer {_apiKey}"}});

        var requestBody = new
        {
            assistant = _assistantId,
            messages = new[]
            {
                new { role = "user", content = message.Text }
            },
            stream = true
        };

        return SendRequest(requestBody).Result;
    }

    private async Task<Message> SendRequest(object payload)
    {
        Task<Stream> stream = _requestService.Fetch($"{_baseUrl}/beta/assistants/completions", payload);

        using var reader = new StreamReader(stream.Result);

        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (line.StartsWith("data:"))
            {
                var jsonData = line.Substring(5).Trim(); 

                if (!string.IsNullOrWhiteSpace(jsonData) && jsonData != "[DONE]")
                {
                    try
                    {
                        var parsed = JsonSerializer.Deserialize<BetaAssistantResponse>(jsonData);
                        return new Message
                        {
                            Text = parsed?.Choices?[0]?.Delta?.Content
                        };
                    }
                    catch (JsonException)
                    {
                        throw new JsonException();
                    }
                }
            }
        }
        throw new Exception("Ошибка обработки ответа от ИИ");
    }
}