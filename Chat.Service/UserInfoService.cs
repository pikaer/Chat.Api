using Infrastructure;

namespace Chat.Service
{
    public class UserInfoService
    {
        public static UserInfoService Instance = SingletonProvider<UserInfoService>.Instance;
    }
}
