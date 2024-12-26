using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases;

public class GenerateAnswerUseCase
{
    private readonly IAiService _aiService;

    public GenerateAnswerUseCase(IAiService aiService)
    {
        _aiService = aiService;
    }
    public async Task<ResponseMessageDTO> Execute(RequestMessageDTO message)
    {
        Message answer = await _aiService.Generate(new Message()
        {
            Text = message.Message
        });
        return new ResponseMessageDTO()
        {
            Message = answer.Text
        };
    }
}