using Infrastructure;

namespace Chat.Repository
{
    public class DiscoveryRepository: BaseRepository
    {
        public static DiscoveryRepository Instance = SingletonProvider<DiscoveryRepository>.Instance;
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }
    }
}
