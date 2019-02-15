using Infrastructure;

namespace Chat.Service
{
    public class DiscoveryService
    {
        public static DiscoveryService Instance = SingletonProvider<DiscoveryService>.Instance;
    }
}
