namespace Chat.Repository
{
    public class MomentRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }
    }
}
