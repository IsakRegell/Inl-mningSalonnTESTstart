namespace InlämningSalonn.Interfaces
{
    public interface IChatGPT
    {
        Task<string> GetChatGPTResponse(string userMessage, string language);
    }
}
