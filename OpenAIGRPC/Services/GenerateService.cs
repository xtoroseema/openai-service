using Application.DTOs;
using Application.UseCases;
using Grpc.Core;

namespace OpenAIGRPC.Services;

public class GenerateService : ResponseService.ResponseServiceBase
{
    private readonly GenerateAnswerUseCase _useCase;

    public GenerateService(GenerateAnswerUseCase useCase)
    {
        _useCase = useCase;
    }
    public async override Task<MessageResponse> GenerateResponse(MessageRequest request, ServerCallContext context)
    {
        var result = await _useCase.Execute(new RequestMessageDTO() {Message = request.Message});
        return new MessageResponse()
        {
            Message = request.Message
        };
    }
}