﻿using Chat.Model.Entity.Moment;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Chat.Service
{
    public class MomentService
    {
        private readonly MomentRepository momentDal = SingletonProvider<MomentRepository>.Instance;
        private UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

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
            foreach(var item in moments)
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
                    HeadImgPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                    PublishTime = item.CreateTime.GetDateDesc(),
                    TextContent = item.TextContent
                };

                var imgList = momentDal.GetMomentImgList(item.MomentId);
                if (imgList.NotEmpty())
                {
                    dto.ImgContents=new List<string>();
                    foreach(var img in imgList)
                    {
                        dto.ImgContents.Add(img.ImgPath.ToMomentImagePath());
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
                rtn.Content.MomentList.Add(dto);
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
                DispalyName = userInfo.NickName,
                HeadImgPath = userInfo.HeadPhotoPath.ToHeadImagePath()
            };
            #endregion

            #region 动态图片
            var imgList = momentDal.GetMomentImgList(moment.MomentId);
            if (imgList.NotEmpty())
            {
                rtn.Content.ImgContents = new List<string>();
                foreach (var img in imgList)
                {
                    rtn.Content.ImgContents.Add(img.ImgPath.ToMomentImagePath());
                }
            }
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
                        HeadImgPath = user.HeadPhotoPath.ToHeadImagePath(),
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

        public ResponseContext<MySpaceResponse> MySpace(RequestContext<MySpaceRequest> request)
        {
            #region 初始化
            var rtn = new ResponseContext<MySpaceResponse>();
            var userInfo = userInfoDal.GetUserInfoByUId(request.Content.UId);
            if (userInfo == null)
            {
                return rtn;
            }
            var moments = momentDal.GetMomentsByUId(request.Content.UId);
            #endregion

            return rtn;
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
    }
}
