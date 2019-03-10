using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;

namespace Chat.Service
{
    public class MomentService
    {
        private readonly MomentRepository momentDal = SingletonProvider<MomentRepository>.Instance;

        public ResponseContext<GetMomentsResponse> GetMoments(RequestContext<GetMomentsRequest> request)
        {
            var rtn = new ResponseContext<GetMomentsResponse>()
            {
                Content = new GetMomentsResponse()
            };

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
