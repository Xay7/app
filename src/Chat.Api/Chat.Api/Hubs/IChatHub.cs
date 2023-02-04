namespace ChatApi.Hubs
{
    public interface IChatHub
    {
        Task AddChatroom(AddChatroomRequest request);
        Task AddToGroup(string groupName);
        Task GetChatrooms(AddChatroomRequest request);
        Task RemoveFromGroup(string groupName);
        Task SendMessage(string user, string message, string groupName);
        Task SendPrivateMessage(string user, string message);
    }
}