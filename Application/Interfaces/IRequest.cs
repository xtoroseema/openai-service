namespace Application.Interfaces;

public interface IRequest
{
    public Task<Stream> Fetch(string url, object payload);

    public void AddHeader(Dictionary<string, string> headers);
}