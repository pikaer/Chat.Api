using Infrastructure;

namespace Chat.Repository
{
    public class ChatRepository : BaseRepository
    {
        public static ChatRepository Instance = SingletonProvider<ChatRepository>.Instance;
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

       
    }
}
