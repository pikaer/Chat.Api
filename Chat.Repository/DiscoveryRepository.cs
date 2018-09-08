namespace Chat.Repository
{
    public class DiscoveryRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.MyChat;
        }
    }
}
