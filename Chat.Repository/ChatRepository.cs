using Infrastructure;

namespace Chat.Repository
{
    public class ChatRepository : BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

       
    }
}
