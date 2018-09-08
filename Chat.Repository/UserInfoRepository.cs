namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.MyChat;
        }
    }
}
