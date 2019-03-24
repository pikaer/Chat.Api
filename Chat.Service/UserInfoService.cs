using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;
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

            var entity = new Visitor()
            {
                UId = request.Content.UId,
                PartnerUId = request.Content.PartnerUId,
                CreateTime = DateTime.Now
            };
            response.Content.IsExecuteSuccess = userInfoDal.InsertVisitor(entity);
            return response;
        }

        public ResponseContext<GetFriendsResponse> GetFriends(RequestContext<GetFriendsRequest> request)
        {
            var response = new ResponseContext<GetFriendsResponse>()
            {
                Content=new GetFriendsResponse()
            };
            
            switch (request.Content.FriendType)
            {
                case FriendTypeEnum.Default:
                    response.Content.Friends = GetRealFriends(request.Content.UId);
                    break;
                case FriendTypeEnum.Attentin:
                    response.Content.Friends = GetAttentionFriends(request.Content.UId);
                    break;
                case FriendTypeEnum.Fans:
                    response.Content.Friends = GetFansFriends(request.Content.UId);
                    break;
                case FriendTypeEnum.Visitor:
                    response.Content.Friends = GetVisitorFriends(request.Content.UId);
                    break;
            }

            return response;
        }
        
        public ResponseContext<UpdateAttentionStateResponse> UpdateAttentionState(RequestContext<UpdateAttentionStateRequest> request)
        {
            var response = new ResponseContext<UpdateAttentionStateResponse>()
            {
                Content = new UpdateAttentionStateResponse()
                {
                    IsExecuteSuccess = true
                }
            };

            if(request.Content.UId== request.Content.PartnerUId)
            {
                return response;
            }

            var friend = userInfoDal.GetFriend(request.Content.UId, request.Content.PartnerUId);
            if (friend == null)
            {
                var entity = new Friend()
                {
                    UId = request.Content.UId,
                    PartnerUId = request.Content.PartnerUId,
                    CreateTime = DateTime.Now
                };
                response.Content.IsExecuteSuccess = userInfoDal.InsertFriend(entity);
            }
            else
            {
                friend.IsDelete = !friend.IsDelete;
                friend.UpdateTime = DateTime.Now;
                response.Content.IsExecuteSuccess = userInfoDal.UpdateFriend(friend);
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
                    response.Content.VisitorCount = visitors.Count;
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

        /// <summary>
        /// 获取互相关注的朋友
        /// </summary>
        private List<FriendResponseType> GetRealFriends(long uId)
        {
            var rtn = new List<FriendResponseType>();
            var friends = userInfoDal.GetFriendsByUid(uId);
            if (friends.NotEmpty())
            {
                foreach (var item in friends.OrderByDescending(a => a.CreateTime))
                {
                    var friend = userInfoDal.GetFriend(item.PartnerUId, item.UId);
                    if (friend != null)
                    {
                        var userInfo = userInfoDal.GetUserInfoByUId(item.PartnerUId);
                        if (userInfo == null)
                        {
                            continue;
                        }
                        rtn.Add(new FriendResponseType()
                        {
                            PartnerUId= item.PartnerUId,
                            DisplayName = item.RemarkName.IsNullOrEmpty() ? userInfo.NickName : item.RemarkName,
                            HeadPhotoPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                            Constellation = Convert.ToDateTime(userInfo.BirthDate).GetConstellation(),
                            Gender = userInfo.Gender,
                            HasAttention = true
                        });
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// 我关注的
        /// </summary>
        private List<FriendResponseType> GetAttentionFriends(long uId)
        {
            var rtn = new List<FriendResponseType>();

            var friends = userInfoDal.GetFriendsByUid(uId);
            if (friends.NotEmpty())
            {
                foreach (var item in friends.OrderByDescending(a => a.CreateTime))
                {
                    var friend = userInfoDal.GetFriend(item.PartnerUId, item.UId);
                    if (friend == null)
                    {
                        var userInfo = userInfoDal.GetUserInfoByUId(item.PartnerUId);
                        if (userInfo == null)
                        {
                            continue;
                        }

                        rtn.Add(new FriendResponseType()
                        {
                            PartnerUId= item.PartnerUId,
                            DisplayName = userInfo.NickName,
                            HeadPhotoPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                            Constellation = Convert.ToDateTime(userInfo.BirthDate).GetConstellation(),
                            Gender = userInfo.Gender,
                            HasAttention = true
                        });
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// 别人关注我的
        /// </summary>
        private List<FriendResponseType> GetFansFriends(long uId)
        {
            var rtn = new List<FriendResponseType>();

            var friends = userInfoDal.GetFriendsByUid(uId, false);
            if (friends.NotEmpty())
            {
                foreach (var item in friends.OrderByDescending(a => a.CreateTime))
                {
                    var friend = userInfoDal.GetFriend(uId, item.UId);
                    if (friend == null)
                    {
                        var userInfo = userInfoDal.GetUserInfoByUId(item.UId);
                        if (userInfo == null)
                        {
                            continue;
                        }

                        rtn.Add(new FriendResponseType()
                        {
                            PartnerUId= item.UId,
                            DisplayName = userInfo.NickName,
                            HeadPhotoPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                            Constellation = Convert.ToDateTime(userInfo.BirthDate).GetConstellation(),
                            Gender = userInfo.Gender,
                            HasAttention = false
                        });
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// 我的访客
        /// </summary>
        private List<FriendResponseType> GetVisitorFriends(long uId)
        {
            var rtn = new List<FriendResponseType>();

            var visitors = userInfoDal.GetVisitors(uId);
            if (visitors.NotEmpty())
            {
                foreach (var item in visitors.OrderByDescending(a => a.CreateTime))
                {
                    var userInfo = userInfoDal.GetUserInfoByUId(item.UId);
                    if (userInfo == null)
                    {
                        continue;
                    }

                    var nickName = string.Empty;
                    var friend = userInfoDal.GetFriend(uId, item.UId);
                    if (friend != null && !friend.RemarkName.IsNullOrEmpty())
                    {
                        nickName = friend.RemarkName;
                    }
                    else
                    {
                        nickName = userInfo.NickName;
                    }

                    rtn.Add(new FriendResponseType()
                    {
                        PartnerUId= item.UId,
                        DisplayName = nickName,
                        HeadPhotoPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                        Constellation = Convert.ToDateTime(userInfo.BirthDate).GetConstellation(),
                        Gender = userInfo.Gender,
                        HasAttention = friend != null,
                        TimeRemark = item.CreateTime.GetDateDesc()
                    });
                }
            }

            return rtn;
        }

    }
}
