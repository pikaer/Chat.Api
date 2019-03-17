using Chat.Model.Entity.Moment;
using Chat.Utility;
using Dapper;
using System;
using System.Collections.Generic;

namespace Chat.Repository
{
    public class MomentRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        private readonly string SELECT_MomentContent = "SELECT MomentId,UId,TextContent,MomentState,CreateTime,UpdateTime FROM dbo.moment_MomentContent ";
        private readonly string SELECT_MomentDiscuss = "SELECT DiscussId,MomentId,UId,DiscussContent,CreateTime,UpdateTime FROM dbo.moment_MomentDiscuss ";
        private readonly string SELECT_MomentImg = "SELECT ImgId,MomentId,ImgPath,CompressPath,ImgLength,ImgMime,CreateTime,UpdateTime FROM dbo.moment_MomentImg ";
        private readonly string SELECT_MomentSupport = "SELECT SupportId,MomentId,UId,CreateTime FROM dbo.moment_MomentSupport ";
        private readonly string SELECT_DiscussSupport = "SELECT DiscussSupportId ,DiscussId,UId,CreateTime FROM dbo.moment_DiscussSupport ";

        public bool InsertMomentContent(MomentContent entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.moment_MomentContent(MomentId,UId,TextContent,MomentState,CreateTime)
                                     VALUES (@MomentId,@UId,@TextContent,@MomentState,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertMomentContent", "插入动态内容到数据库失败", ex);
                    return false;
                }
            }
        }

        public bool InsertMomentDiscuss(MomentDiscuss entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.moment_MomentDiscuss(DiscussId,MomentId,UId,DiscussContent,CreateTime)
                                     VALUES (@DiscussId,@MomentId,@UId,@DiscussContent,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertMomentDiscuss", "插入动态评论内容到数据库失败", ex);
                    return false;
                }
            }
        }

        public bool InsertMomentImg(MomentImg entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.moment_MomentImg(ImgId,MomentId ,ImgPath,CompressPath,ImgLength,ImgMime,CreateTime)
                                     VALUES (@ImgId,@MomentId,@ImgPath,@CompressPath ,@ImgLength,@ImgMime,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertMomentImg", "插入动态图片到数据库失败", ex);
                    return false;
                }
            }
        }

        public bool InsertMomentSupport(MomentSupport entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.moment_MomentSupport(SupportId,MomentId,UId,CreateTime)
                                VALUES (@SupportId ,@MomentId,@UId,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertMomentContent", "插入动态点赞信息到数据库失败", ex);
                    return false;
                }
            }
        }

        public bool InsertDiscussSupport(DiscussSupport entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.moment_DiscussSupport (DiscussSupportId,DiscussId ,UId,CreateTime)
                                VALUES (@DiscussSupportId ,@DiscussId ,@UId,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertDiscussSupport", "插入评论点赞信息到数据库失败", ex);
                    return false;
                }
            }
        }

        public List<MomentContent> GetMomentList()
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = SELECT_MomentContent+ "Order by CreateTime desc";
                    return Db.Query<MomentContent>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetMoments", "从数据库获取动态信息异常", ex);
                    return null;
                }
            }
        }

        public MomentContent GetMoment(Guid momentId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0}Where MomentId = '{1}'", SELECT_MomentContent, momentId.ToString());
                    return Db.QueryFirstOrDefault<MomentContent>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetMoments", "从数据库获取动态信息异常", ex);
                    return null;
                }
            }
        }

        public List<MomentImg> GetMomentImgList(Guid momentId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0}Where MomentId = '{1}'", SELECT_MomentImg, momentId.ToString());
                    return Db.Query<MomentImg>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetMomentImgs", "从数据库获取动态图片异常，MomentId=" + momentId.ToString(), ex);
                    return null;
                }
            }
        }

        public List<MomentDiscuss> GetMomentDiscussList(Guid momentId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0}Where MomentId='{1}'", SELECT_MomentDiscuss, momentId.ToString());
                    return Db.Query<MomentDiscuss>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetMomentDiscuss", "从数据库获取动态评论信息异常，MomentId=" + momentId.ToString(), ex);
                    return null;
                }
            }
        }

        public List<MomentSupport> GetMomentSupportList(Guid momentId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0}Where MomentId='{1}'", SELECT_MomentSupport, momentId.ToString());
                    return Db.Query<MomentSupport>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetMomentSupportList", "从数据库获取动态点赞信息异常，MomentId="+momentId.ToString(), ex);
                    return null;
                }
            }
        }

        public List<DiscussSupport> GetDiscussSupportList(Guid discussId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0}Where DiscussId='{1}'", SELECT_DiscussSupport, discussId.ToString());
                    return Db.Query<DiscussSupport>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetDiscussSupportList", "从数据库获取动态点赞信息异常，DiscussId=" + discussId.ToString(), ex);
                    return null;
                }
            }
        }

        public bool DeleteDiscussSupport(Guid discussId, long uId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"DELETE FROM dbo.moment_DiscussSupport WHERE DiscussId=@DiscussId And UId=@UId";
                    return Db.Execute(sql, new { DiscussId = discussId, UId = uId }) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("DeleteMomentSupport", "从数据库删除动态点赞信息异常，DiscussId=" + discussId.ToString(), ex);
                    return false;
                }
            }
        }

        public bool DeleteMomentSupport(Guid momentId, long partnerUId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"DELETE FROM dbo.moment_MomentSupport WHERE MomentId=@MomentId And UId=@UId";
                    return Db.Execute(sql,new { MomentId = momentId , PartnerUId = partnerUId }) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("DeleteMomentSupport", "从数据库删除动态点赞信息异常，MomentId=" + momentId.ToString(), ex);
                    return false;
                }
            }
        }
    }
}
