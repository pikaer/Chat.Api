using Infrastructure.Utility;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        public static UserInfoRepository Instance = SingletonProvider<UserInfoRepository>.Instance;

        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        private readonly string SELECT_USERINFO = "SELECT UserId,OpenId,Gender,NickName,BirthDate,ProvinceId,CityId,HomeProvinceId,HomeCityId,SchoolName,EntranceDate,SchoolType,LiveState,Mobile,WeChatNo,HeadPhotoPath,Signature,CreateTime,UpdateTime  FROM dbo.user_UserInfo ";
    }
}
