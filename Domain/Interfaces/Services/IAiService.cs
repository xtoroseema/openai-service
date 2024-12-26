using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IAiService
{
    public Task<Message> Generate(Message message);
}