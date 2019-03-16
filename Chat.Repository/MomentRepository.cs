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
        private readonly string SELECT_MomentDiscuss = "SELECT DiscussId,MomentId,UId,PartnerUId,DiscussContent,CreateTime,UpdateTime FROM dbo.moment_MomentDiscuss ";
        private readonly string SELECT_MomentImg = "SELECT ImgId,MomentId,ImgPath,CompressPath,ImgLength,ImgMime,CreateTime,UpdateTime FROM dbo.moment_MomentImg ";
        private readonly string SELECT_MomentSupport = "SELECT SupportId,MomentId,UId,PartnerUId,CancelSupport,CreateTime,UpdateTime FROM dbo.moment_MomentSupport ";

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
                    var sql = @"INSERT INTO dbo.moment_MomentDiscuss(DiscussId,MomentId,UId,PartnerUId,DiscussContent,CreateTime)
                                     VALUES (@DiscussId,,@MomentId, ,@UId, ,@PartnerUId,,@DiscussContent,,@CreateTime)";
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
                    var sql = @"INSERT INTO dbo.moment_MomentSupport(SupportId,MomentId,UId ,PartnerUId,CancelSupport,CreateTime)
                                VALUES (@SupportId ,@MomentId,@UId,@PartnerUId,@CancelSupport,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertMomentContent", "插入动态点赞信息到数据库失败", ex);
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
                    var sql = SELECT_MomentContent;
                    return Db.Query<MomentContent>(sql).AsList();
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
                    var sql = SELECT_MomentImg+ "MomentId=@MomentId";
                    return Db.Query<MomentImg>(sql,new { MomentId= momentId }).AsList();
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
                    var sql = SELECT_MomentDiscuss + "MomentId=@MomentId";
                    return Db.Query<MomentDiscuss>(sql, new { MomentId = momentId }).AsList();
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
                    var sql = SELECT_MomentSupport + "MomentId=@MomentId";
                    return Db.Query<MomentSupport>(sql, new { MomentId = momentId }).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetMomentSupportList", "从数据库获取动态点赞信息异常，MomentId="+momentId.ToString(), ex);
                    return null;
                }
            }
        }
    }
}
