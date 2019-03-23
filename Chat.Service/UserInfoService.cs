using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Linq;

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
                var entity = userInfoDal.GetUserInfoByUId(request.Content.UId);
                if (entity == null)
                {
                    return response;
                }
                response.Content = new GetUserInfoResponse
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

        public ResponseContext<UpdateUserInfoResponse> UpdateUserInfo(RequestContext<UpdateUserInfoRequest> request)
        {
            var response = new ResponseContext<UpdateUserInfoResponse>()
            {
                Content = new UpdateUserInfoResponse()
            };
            var data = request.Content;
            var userInfo = new UserInfo()
            {
                UId = data.UId,
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
                UpdateTime = DateTime.Now
            };
            response.Content.ExcuteResult = userInfoDal.UpdateUserInfo(userInfo);
            return response;
        }

        public ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request)
        {
            var response = new ResponseContext<SetUserInfoResponse>()
            {
                Content=new SetUserInfoResponse()
            };
            try
            {
                var data = request.Content;
                var entity = new UserInfo()
                {
                    OpenId = data.OpenId,
                    Gender = (GenderEnum)data.Gender,
                    NickName = data.NickName,
                    CreateTime = DateTime.Now
                };

                var userInfoEntity = userInfoDal.GetUserInfoByOpenId(request.Content.OpenId);
                if (userInfoEntity == null)
                {
                    //初始编号
                    long tempUNo = 12345678;
                    var maxUNo = userInfoDal.GetMaxUNo();
                    if (maxUNo > 0)
                    {
                        tempUNo= maxUNo+1;
                    }
                    entity.UNo = tempUNo;
                    bool success=userInfoDal.InsertUserInfo(entity);
                    if (success)
                    {
                        response.Content.UId = userInfoDal.GetUserInfoByOpenId(request.Content.OpenId).UId;
                    }
                    response.Content.ExcuteResult = success;
                }
                else
                {
                    entity.UId = userInfoEntity.UId;
                    response.Content.UId = userInfoEntity.UId;
                    response.Content.ExcuteResult =true;
                }
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.InsertError);
                Log.Error("SetUserInfo", "存入用户信息异常", ex, request.Head);
            }
            return response;
        }

        public ResponseContext<SetVisitorResponse> SetVisitor(RequestContext<SetVisitorRequest> request)
        {
            var response = new ResponseContext<SetVisitorResponse>()
            {
                Content = new SetVisitorResponse()
                {
                    IsExecuteSuccess = true
                }
            };

            if (request.Content.UId == request.Content.PartnerUId)
            {
                return response;
            }

            var visitor = userInfoDal.GetVisitor(request.Content.UId, request.Content.PartnerUId);
            if (visitor == null)
            {
                var entity = new Visitor()
                {
                    UId= request.Content.UId,
                    PartnerUId= request.Content.PartnerUId,
                    VisitCount=1,
                    CreateTime=DateTime.Now
                };
                response.Content.IsExecuteSuccess = userInfoDal.InsertVisitor(entity);
            }
            else
            {
                visitor.VisitCount++;
                visitor.UpdateTime = DateTime.Now;
                response.Content.IsExecuteSuccess = userInfoDal.UpdateVisitor(visitor);
            }
            return response;
        }

        public ResponseContext<GetUserSimpleInfoResponse> GetUserSimpleInfo(RequestContext<GetUserSimpleInfoRequest> request)
        {
            var response = new ResponseContext<GetUserSimpleInfoResponse>();
            try
            {
                var entity = userInfoDal.GetUserInfoByUId(request.Content.UId);
                if (entity == null)
                {
                    return response;
                }

                response.Content = new GetUserSimpleInfoResponse
                {
                    NickName = entity.NickName,
                    UNo = entity.UNo,
                    HeadPhotoPath = entity.HeadPhotoPath.ToHeadImagePath()
                };

                //我的关注
                var attentions = userInfoDal.GetFriendsByUid(request.Content.UId);
                if (attentions.NotEmpty())
                {
                    response.Content.AttentionCount = attentions.Count;
                }

                //我的粉丝
                var fans= userInfoDal.GetFriendsByUid(request.Content.UId,false);
                if (fans.NotEmpty())
                {
                    response.Content.FansCount = fans.Count;
                }

                //访客数量
                var visitors= userInfoDal.GetVisitors(request.Content.UId);
                if (visitors.NotEmpty())
                {
                    response.Content.VisitorCount = visitors.Sum(a=>a.VisitCount);
                }
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetUserSimpleInfo", "获取用户简易信息异常", ex, request.Head);
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

                response.Content = new GetUserPreferenceResponse
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

        public ResponseContext<UpdateUserPreferenceResponse> UpdateUserPreference(RequestContext<UpdateUserPreferenceRequest> request)
        {
            var response = new ResponseContext<UpdateUserPreferenceResponse>()
            {
                Content=new UpdateUserPreferenceResponse()
            };
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

                var userPreference = userInfoDal.GetUserPreference(request.Content.UId);
                if(userPreference == null)
                {
                    response.Content.ExcuteResult = userInfoDal.InsertUserPreference(entity);
                }
                else
                {
                    entity.PreferId = userPreference.PreferId;
                    response.Content.ExcuteResult = userInfoDal.UpdateUserPreference(entity);
                }
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
