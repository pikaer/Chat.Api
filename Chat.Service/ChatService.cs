using Chat.Repository;
using Infrastructure;

namespace Chat.Service
{
    public class ChatService
    {
        public static ChatService Instance = SingletonProvider<ChatService>.Instance;
        private readonly ChatRepository chatDal = ChatRepository.Instance;
    }
}
