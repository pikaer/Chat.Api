using Chat.Repository;
using Infrastructure;

namespace Chat.Service
{
    public class ChatService
    {
        private readonly ChatRepository chatDal = SingletonProvider<ChatRepository>.Instance;
    }
}
