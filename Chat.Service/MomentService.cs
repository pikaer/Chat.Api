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
        private UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        public ResponseContext<GetMomentsResponse> GetMoments(RequestContext<GetMomentsRequest> request)
        {
            var rtn = new ResponseContext<GetMomentsResponse>()
            {
                Content = new GetMomentsResponse()
            };
            
            //测试数据
            if (SwitchControl.TestIsOpen())
            {
                switch (request.Content.MomentType)
                {
                    case MomentTypeEnum.Attention:
                        rtn.Content.MomentList = GetAttentionMomentsTestData();
                        break;
                    case MomentTypeEnum.Newest:
                        rtn.Content.MomentList = GetNewestMomentsTestData();
                        break;
                    case MomentTypeEnum.Recommend:
                    default:
                        rtn.Content.MomentList = GetRecommendMomentsTestData();
                        break;
                }
                return rtn;
            }
            
            var momentList = new List<MomentType>();
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
                    dto.DiscussList = discussList.Select(a=>new DiscussType
                    {
                        UId= item.UId,
                        PartnerUId=a.PartnerUId,
                        DiscussContent=a.DiscussContent,
                        CreateTimeDesc=a.CreateTime.GetDateDesc()
                    }).ToList();

                    dto.CommentCount = discussList.Count.ToString();
                }

                var supportList = momentDal.GetMomentSupportList(item.MomentId);
                if (supportList.NotEmpty())
                {
                    dto.SupportCount = supportList.Count.ToString();
                }
            }
            return rtn;
        }

        /// <summary>
        /// 发布动态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        /// 获取动态测试数据
        /// </summary>
        private List<MomentType> GetRecommendMomentsTestData()
        {
            var rtn = new List<MomentType>();

            for(int i =1; i <= 30; i++)
            {
                var dto = new MomentType()
                {
                    MomentId=Guid.NewGuid().ToString(),
                    HeadImgPath= "pikaer".ToHeadImagePath(),
                    DispalyName=string.Format("我是第{0}块小饼干",i),
                    PublishTime=string.Format("{0}分钟之前", i*3),
                    TextContent= string.Format("第{0}块小饼干在{1}分钟之前发布了Moment", i,i * 3),
                    HasSupport=i%3==0,
                    SupportCount=(i*7).ToString(),
                    CommentCount=(i*9).ToString()
                };

                var count = new Random().Next(0, 10);
                if (count > 0)
                {
                    dto.ImgContents = new List<string>();
                    for (int j = 1; j < count; j++)
                    {
                        var path = string.Format("test{0}", j).ToMomentImagePath();
                        dto.ImgContents.Add(path);
                    }
                }
               
                rtn.Add(dto);
            }
            return rtn;
        }

        /// <summary>
        /// 获取动态测试数据
        /// </summary>
        private List<MomentType> GetNewestMomentsTestData()
        {
            var rtn = new List<MomentType>();

            for (int i = 1; i <= 30; i++)
            {
                var dto = new MomentType()
                {
                    MomentId = Guid.NewGuid().ToString(),
                    HeadImgPath = "pikaer".ToHeadImagePath(),
                    DispalyName = string.Format("第{0}个小星星", i),
                    PublishTime = string.Format("{0}分钟之前", i * 6),
                    TextContent = string.Format("第{0}个小星星在{1}分钟之前发布了Moment", i, i * 6),
                    HasSupport = i % 3 == 0,
                    SupportCount = (i * 7).ToString(),
                    CommentCount = (i * 9).ToString()
                };

                var count = new Random().Next(0, 10);
                if (count > 0)
                {
                    dto.ImgContents = new List<string>();
                    for (int j = 1; j < count; j++)
                    {
                        var path = string.Format("test{0}", j).ToMomentImagePath();
                        dto.ImgContents.Add(path);
                    }
                }

                rtn.Add(dto);
            }
            return rtn;
        }

        /// <summary>
        /// 获取动态测试数据
        /// </summary>
        private List<MomentType> GetAttentionMomentsTestData()
        {
            var rtn = new List<MomentType>();

            for (int i = 1; i <= 30; i++)
            {
                var dto = new MomentType()
                {
                    MomentId = Guid.NewGuid().ToString(),
                    HeadImgPath = "pikaer".ToHeadImagePath(),
                    DispalyName = string.Format("第{0}个皮卡丘", i),
                    PublishTime = string.Format("{0}分钟之前", i * 8),
                    TextContent = string.Format("第{0}个皮卡丘在{1}分钟之前发布了Moment", i, i * 8),
                    HasSupport = i % 3 == 0,
                    SupportCount = (i * 7).ToString(),
                    CommentCount = (i * 9).ToString()
                };

                var count = new Random().Next(0, 10);
                if (count > 0)
                {
                    dto.ImgContents = new List<string>();
                    for (int j = 1; j < count; j++)
                    {
                        var path = string.Format("test{0}", j).ToMomentImagePath();
                        dto.ImgContents.Add(path);
                    }
                }

                rtn.Add(dto);
            }
            return rtn;
        }
    }
}
