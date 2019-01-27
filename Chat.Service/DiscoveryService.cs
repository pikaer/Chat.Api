using Infrastructure.Utility;

namespace Chat.Service
{
    public class DiscoveryService
    {
        public static DiscoveryService Instance = SingletonProvider<DiscoveryService>.Instance;
    }
}
