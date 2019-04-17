using Chat.Model.Entity.Moment;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Chat.Service
{
    public class MomentService
    {
        private readonly MomentRepository momentDal = SingletonProvider<MomentRepository>.Instance;
        private readonly UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        public ResponseContext<GetMomentsResponse> GetMoments(RequestContext<GetMomentsRequest> request)
        {
            var rtn = new ResponseContext<GetMomentsResponse>()
            {
                Content = new GetMomentsResponse()
                {
                    MomentList=new List<MomentType>()
                }
            };

            var moments = momentDal.GetMomentList();
            foreach(var item in moments.OrderByDescending(a=>a.CreateTime))
            {
                var userInfo = userInfoDal.GetUserInfoByUId(item.UId);
                if (userInfo == null)
                {
                    continue;
                }
                var dto = new MomentType()
                {
                    MomentId = item.MomentId.ToString(),
                    UId = item.UId,
                    DispalyName = userInfo.NickName,
                    HeadImgPath = userInfo.HeadPhotoPath.GetImgPath(),
                    PublishTime = item.CreateTime.GetDateDesc(),
                    TextContent = item.TextContent
                };

                var imgList = momentDal.GetMomentImgList(item.MomentId);
                if (imgList.NotEmpty())
                {
                    dto.ImgContents=new List<string>();
                    foreach(var img in imgList)
                    {
                        dto.ImgContents.Add(img.ImgPath.GetImgPath());
                    }
                }

                var discussList = momentDal.GetMomentDiscussList(item.MomentId);
                if (discussList.NotEmpty())
                {
                    dto.CommentCount = discussList.Count;
                }

                var supportList = momentDal.GetMomentSupportList(item.MomentId);
                if (supportList.NotEmpty())
                {
                    dto.SupportCount = supportList.Count;
                    dto.HasSupport = supportList.Exists(a => a.UId == request.Content.UId);
                }

                //是否已经关注对方
                dto.HasAttention = userInfoDal.GetFriend(request.Content.UId, item.UId)!= null;

                rtn.Content.MomentList.Add(dto);
            }

            if (rtn.Content.MomentList.NotEmpty()&&request.Content.PageIndex>0)
            {
                //每次返回5条
                int pageIndex = request.Content.PageIndex;
                rtn.Content.MomentList = rtn.Content.MomentList.Skip(5 *(pageIndex-1)).Take(5).ToList();
            }
            return rtn;
        }
        
        public ResponseContext<PublishMomentResponse> PublishMoment(RequestContext<PublishMomentRequest> request)
        {
            var rtn = new ResponseContext<PublishMomentResponse>()
            {
                Content = new PublishMomentResponse()
            };

            var moment = new MomentContent()
            {
                MomentId = Guid.NewGuid(),
                UId = request.Content.UId,
                TextContent = request.Content.TextContent,
                MomentState = MomentStateEnum.Default,
                CreateTime = DateTime.Now
            };

            bool success = true;
            if (request.Content.ImgContents.NotEmpty())
            {
                foreach (var item in request.Content.ImgContents)
                {
                    var img = new MomentImg()
                    {
                        ImgId = Guid.NewGuid(),
                        MomentId = moment.MomentId,
                        ImgPath = item.ImgPath,
                        ImgMime = item.ImgMime,
                        ImgLength = item.ImgLength,
                        CreateTime = DateTime.Now
                    };
                    success=momentDal.InsertMomentImg(img);
                    if (!success)
                    {
                        break;
                    }
                }
            }
            if (success)
            {
                success = momentDal.InsertMomentContent(moment);
            }

            rtn.Content.IsExecuteSuccess = success;
            if (!success)
            {
                rtn.Head = new ResponseHead(false,ErrCodeEnum.InsertError);
            }
            return rtn;
        }

        public ResponseContext<MomentDetailResponse> MomentDetail(RequestContext<MomentDetailRequest> request)
        {
            #region 初始化
            var rtn = new ResponseContext<MomentDetailResponse>();
            var moment = momentDal.GetMoment(request.Content.MomentId);
            if (moment == null)
            {
                return rtn;
            }
            var userInfo = userInfoDal.GetUserInfoByUId(moment.UId);
            if (userInfo == null)
            {
                return rtn;
            }
            #endregion
            
            #region 动态正文
            rtn.Content = new MomentDetailResponse()
            {
                TextContent = moment.TextContent,
                PublishTime = moment.CreateTime.GetDateDesc(),
                ImgContents= GetImgContents(moment.MomentId),
                DispalyName = userInfo.NickName,
                HeadImgPath = userInfo.HeadPhotoPath.GetImgPath()
            };
            #endregion
            
            #region 动态点赞
            var supportList = momentDal.GetMomentSupportList(moment.MomentId);
            if (supportList.NotEmpty())
            {
                rtn.Content.SupportCount = supportList.Count;
                rtn.Content.HasSupport = supportList.Exists(a => a.UId == request.Content.UId);
            }
            #endregion

            #region 动态评论
            var discussList = momentDal.GetMomentDiscussList(moment.MomentId);
            if (discussList.NotEmpty())
            {
                rtn.Content.CommentCount = discussList.Count;
                rtn.Content.DiscussList = new List<DiscussType>();

                //评论点赞信息
                foreach(var item in discussList)
                {
                    var user= userInfoDal.GetUserInfoByUId(item.UId);
                    var dto = new DiscussType()
                    {
                        DiscussId = item.DiscussId,
                        UId = item.UId,
                        DispalyName = user.NickName,
                        HeadImgPath = user.HeadPhotoPath.GetImgPath(),
                        DiscussContent = item.DiscussContent,
                        DiscussTime = item.CreateTime.GetDateDesc()
                    };

                    var discussSupport = momentDal.GetDiscussSupportList(item.DiscussId);
                    if (discussSupport.NotEmpty())
                    {
                        dto.SupportCount = discussSupport.Count;
                        dto.HasSupport = discussSupport.Exists(a => a.UId == request.Content.UId);
                    }
                    rtn.Content.DiscussList.Add(dto);
                }
            }
            return rtn;
            #endregion
        }

        public ResponseContext<MomentDiscussResponse> MomentDiscuss(RequestContext<MomentDiscussRequest> request)
        {
            var response = new ResponseContext<MomentDiscussResponse>()
            {
                Content =new MomentDiscussResponse()
            };

            var entity = new MomentDiscuss()
            {
                DiscussId = Guid.NewGuid(),
                MomentId = request.Content.MomentId,
                UId = request.Content.UId,
                DiscussContent = request.Content.DiscussContent,
                CreateTime = DateTime.Now
            };

            response.Content.IsExecuteSuccess = momentDal.InsertMomentDiscuss(entity);

            return response;
        }

        public ResponseContext<MySpaceResponse> MySpace(RequestContext<MySpaceRequest> request)
        {
            #region 初始化
            var rtn = new ResponseContext<MySpaceResponse>();

            var userInfo = userInfoDal.GetUserInfoByUId(request.Content.PartnerUId);
            if (userInfo == null)
            {
                return rtn;
            }
            #endregion

            #region 填充响应体
            var age = Convert.ToDateTime(userInfo.BirthDate).GetAgeByBirthdate().ToString();
            var constellation = Convert.ToDateTime(userInfo.BirthDate).GetConstellation();
            var location = CommonHelper.GetLocation(userInfo.Province, userInfo.City, "");
            var career = string.Format("{0} · {1}", userInfo.SchoolName,"程序员");
            string basicInfo = string.Format("{0} · {1}岁 · {2} · {3}", userInfo.Gender==GenderEnum.Man?"男":"女", age, constellation, location);

            rtn.Content = new MySpaceResponse()
            {
                BackgroundImg = userInfo.BackgroundImg.GetImgPath(),
                HeadImgPath = userInfo.HeadPhotoPath.GetImgPath(),
                NickName = userInfo.NickName,
                Gender = userInfo.Gender.ToDescription(),
                Signature = userInfo.Signature,
                BasicInfo= basicInfo,
                EducationAndCareer = career,
                IsSelf=request.Content.UId== request.Content.PartnerUId
            };

            //发布的动态
            var moments = momentDal.GetMomentsByUId(request.Content.PartnerUId);
            if (moments.NotEmpty())
            {
                rtn.Content.MomentList = moments.OrderByDescending(a=>a.CreateTime).Select(a => new MySpaceMomentType()
                {
                    MomentId=a.MomentId.ToString(),
                    PublishTime=a.CreateTime.GetDateDesc(),
                    TextContent=a.TextContent.Trim(),
                    ImgContents= GetImgContents(a.MomentId)
                }).ToList();

                rtn.Content.MomentList.RemoveAll(a => a.TextContent.IsNullOrEmpty() && a.ImgContents.IsNullOrEmpty());
            }

            var friend = userInfoDal.GetFriend(request.Content.UId, request.Content.PartnerUId);
            rtn.Content.HasAttention = friend != null&& !friend.IsDelete;

            return rtn;
            #endregion
        }

        public ResponseContext<SupportMomentResponse> SupportMoment(RequestContext<SupportMomentRequest> request)
        {
            var response = new ResponseContext<SupportMomentResponse>()
            {
                Content = new SupportMomentResponse()
            };

            if (request.Content.IsSupport)
            {
                var support = new MomentSupport()
                {
                    SupportId = Guid.NewGuid(),
                    MomentId = request.Content.MomentId,
                    UId = request.Content.UId,
                    CreateTime = DateTime.Now
                };
                response.Content.IsExecuteSuccess= momentDal.InsertMomentSupport(support);
            }
            else
            {
                response.Content.IsExecuteSuccess = momentDal.DeleteMomentSupport(request.Content.MomentId, request.Content.UId);
            }
            return response;
        }

        public ResponseContext<SupportDiscussResponse> SupportDiscuss(RequestContext<SupportDiscussRequest> request)
        {
            var response = new ResponseContext<SupportDiscussResponse>()
            {
                Content = new SupportDiscussResponse()
            };

            if (request.Content.IsSupport)
            {
                var support = new DiscussSupport()
                {
                    DiscussSupportId = Guid.NewGuid(),
                    DiscussId = request.Content.DiscussId,
                    UId = request.Content.UId,
                    CreateTime = DateTime.Now
                };
                response.Content.IsExecuteSuccess = momentDal.InsertDiscussSupport(support);
            }
            else
            {
                response.Content.IsExecuteSuccess = momentDal.DeleteDiscussSupport(request.Content.DiscussId, request.Content.UId);
            }
            return response;
        }

        public ResponseContext<DeleteImgResponse> DeleteImg(RequestContext<DeleteImgRequest> request)
        {
            var response = new ResponseContext<DeleteImgResponse>()
            {
                Content = new DeleteImgResponse()
            };

            string path = JsonSettingHelper.AppSettings["SaveMomentImg"]+request.Content.ImgPath;

            try
            {
                File.Delete(path);
                response.Content.IsExecuteSuccess = true;
            } 
            catch(Exception ex)
            {
                Log.Error("DeleteImg", "删除图片失败，path" + path, ex,request.Head);
            }
            return response;
        }

        /// <summary>
        /// 获取动态图片
        /// </summary>
        /// <param name="momentId">动态Id</param>
        /// <returns></returns>
        private List<string> GetImgContents(Guid momentId)
        {
            var imgList = momentDal.GetMomentImgList(momentId);
            if (imgList.NotEmpty())
            {
                var rtn = new List<string>();
                foreach (var img in imgList)
                {
                    rtn.Add(img.ImgPath.GetImgPath());
                }
                return rtn;
            }
            return null;
        }
    }
}
