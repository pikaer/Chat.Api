using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;

namespace Chat.Service
{
    public class UserInfoService
    {
        private UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        public ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request)
        {
            var response = new ResponseContext<GetUserInfoResponse>();
            try
            {
                var entity = userInfoDal.GetUserInfo(request.Content.UId);
                if (entity == null)
                {
                    return response;
                }
                response.Data = new GetUserInfoResponse
                {
                    Gender = (int)entity.Gender,
                    NickName = entity.NickName,
                    BirthDate = entity.BirthDate,
                    Province = entity.Province,
                    City = entity.City,
                    Area = entity.Area,
                    HomeProvince = entity.HomeProvince,
                    HomeCity = entity.HomeCity,
                    HomeArea = entity.HomeArea,
                    SchoolName = entity.SchoolName,
                    EntranceDate = entity.EntranceDate,
                    SchoolType = (int)entity.SchoolType,
                    LiveState = (int)entity.LiveState,
                    Mobile = entity.Mobile,
                    WeChatNo = entity.WeChatNo,
                    HeadPhotoPath = entity.HeadPhotoPath,
                    Signature = entity.Signature,
                };
            }
            catch(Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetUserInfo","获取用户信息异常", ex,request.Head);
            }
            return response;
        }

        public ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request)
        {
            var response = new ResponseContext<SetUserInfoResponse>();
            try
            {
                var data = request.Content;
                var entity = new UserInfo()
                {
                    OpenId = data.OpenId,
                    Gender = (GenderEnum)data.Gender,
                    NickName = data.NickName,
                    BirthDate = data.BirthDate,
                    Province = data.Province,
                    City = data.City,
                    Area = data.Area,
                    HomeProvince = data.HomeProvince,
                    HomeCity = data.HomeCity,
                    HomeArea = data.HomeArea,
                    SchoolName = data.SchoolName,
                    EntranceDate = data.EntranceDate,
                    SchoolType = (SchoolTypeEnum)data.SchoolType,
                    LiveState = (LiveStateEnum)data.LiveState,
                    Mobile = data.Mobile,
                    WeChatNo = data.WeChatNo,
                    Signature = data.Signature,
                    CreateTime = DateTime.Now
                };
                response.Data.ExcuteResult = userInfoDal.InsertUserInfo(entity);
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.InsertError);
                Log.Error("SetUserInfo", "存入用户信息异常", ex, request.Head);
            }
            return response;
        }

        public ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request)
        {
            var response = new ResponseContext<GetUserPreferenceResponse>();
            try
            {
                var entity = userInfoDal.GetUserPreference(request.Content.UId);
                if (entity == null)
                {
                    return response;
                }

                response.Data = new GetUserPreferenceResponse
                {
                    PreferGender = (int)entity.PreferGender,
                    PreferPlace = (int)entity.PreferPlace,
                    PreferHome = (int)entity.PreferHome,
                    PreferAge = (int)entity.PreferAge,
                    PreferSchoolType = (int)entity.PreferSchoolType,
                    PreferLiveState = (int)entity.PreferLiveState
                };
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetUserPreference", "获取用户偏好设置异常", ex, request.Head);
            }
            return response;
        }

        public ResponseContext<SetUserPreferenceResponse> SetUserPreference(RequestContext<SetUserPreferenceRequest> request)
        {
            var response = new ResponseContext<SetUserPreferenceResponse>();
            try
            {
                var data = request.Content;
                var entity = new UserPreference()
                {
                    UId = data.UId,
                    PreferGender = (GenderEnum)data.PreferGender,
                    PreferPlace = (PartnerPlaceEnum)data.PreferPlace,
                    PreferHome = (PartnerHomeEnum)data.PreferHome,
                    PreferAge = (PartnerAgeEnum)data.PreferAge,
                    PreferSchoolType = (SchoolTypeEnum)data.PreferSchoolType,
                    PreferLiveState = (LiveStateEnum)data.PreferLiveState,
                    CreateTime = DateTime.Now
                };
                response.Data.ExcuteResult = userInfoDal.InsertUserPreference(entity);
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.InsertError);
                Log.Error("SetUserPreference", "存入用户偏好设置异常", ex, request.Head);
            }
            return response;
        }
    }
}
