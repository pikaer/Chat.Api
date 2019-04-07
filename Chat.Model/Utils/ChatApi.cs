using Chat.Model.Enum;
using System;
using System.Collections.Generic;

namespace Chat.Model.Utils
{
    /**
    * 以下类都是和前端交互的实体，后面的只要是接口直接返回的类
    * 都放在此处，和DTO,Entity完全分离。强制要求和前端交互的类不得直接引用DTO,Entity中的类
    * */

    #region GetUserInfo
    public class GetUserInfoRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetUserInfoResponse
    {
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 生日（2018-08-20）
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户所在区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 用户家乡所在省份
        /// </summary>
        public string HomeProvince { get; set; }

        /// <summary>
        /// 用户家乡所在城市
        /// </summary>
        public string HomeCity { get; set; }

        /// <summary>
        /// 用户家乡所在区
        /// </summary>
        public string HomeArea { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 入学日期（2017-09）
        /// </summary>
        public string EntranceDate { get; set; }

        /// <summary>
        /// 学校类型（详情见SchoolTypeEnum）
        /// </summary>
        public int SchoolType { get; set; }

        /// <summary>
        /// 状态(详情见LiveStateEnum）
        /// </summary>
        public int LiveState { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatNo { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }
    }
    #endregion

    #region GetUserSimpleInfo
    public class GetUserSimpleInfoRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetUserSimpleInfoResponse
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户账号，外露给前端
        /// </summary>
        public long UNo { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadPhotoPath { get; set; }

        /// <summary>
        /// 关注数量
        /// </summary>
        public int AttentionCount { get; set; }

        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int FansCount { get; set; }

        /// <summary>
        /// 访客数量
        /// </summary>
        public int VisitorCount { get; set; }

    }
    #endregion

    #region SetUserInfo
    public class SetUserInfoRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 小程序端-用户唯一标示
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }
    }

    public class SetUserInfoResponse
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public bool ExcuteResult { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }
    #endregion

    #region UpdateUserInfo
    public class UpdateUserInfoRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 生日（2018-08-20）
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户所在区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 用户家乡所在省份
        /// </summary>
        public string HomeProvince { get; set; }

        /// <summary>
        /// 用户家乡所在城市
        /// </summary>
        public string HomeCity { get; set; }

        /// <summary>
        /// 用户家乡所在区
        /// </summary>
        public string HomeArea { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 入学日期（2017-09）
        /// </summary>
        public string EntranceDate { get; set; }

        /// <summary>
        /// 学校类型
        /// </summary>
        public int SchoolType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int LiveState { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatNo { get; set; }
    }

    public class UpdateUserInfoResponse
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public bool ExcuteResult { get; set; }
    }
    #endregion

    #region GetUserPreference
    public class GetUserPreferenceRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetUserPreferenceResponse
    {
        /// <summary>
        /// 用户性别偏好
        /// </summary>
        public int PreferGender { get; set; }

        /// <summary>
        /// 用户希望对方所在地偏好
        /// </summary>
        public int PreferPlace { get; set; }

        /// <summary>
        /// 用户希望对方家乡偏好
        /// </summary>
        public int PreferHome { get; set; }

        /// <summary>
        /// 用户希望对方年龄偏好
        /// </summary>
        public int PreferAge { get; set; }

        /// <summary>
        /// 用户希望对方学校类型偏好
        /// </summary>
        public int PreferSchoolType { get; set; }

        /// <summary>
        /// 用户希望对方状态偏好
        /// </summary>
        public int PreferLiveState { get; set; }
    }
    #endregion

    #region UpdateUserPreference
    public class UpdateUserPreferenceRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 用户性别偏好
        /// </summary>
        public int PreferGender { get; set; }

        /// <summary>
        /// 用户希望对方所在地偏好
        /// </summary>
        public int PreferPlace { get; set; }

        /// <summary>
        /// 用户希望对方家乡偏好
        /// </summary>
        public int PreferHome { get; set; }

        /// <summary>
        /// 用户希望对方年龄偏好
        /// </summary>
        public int PreferAge { get; set; }

        /// <summary>
        /// 用户希望对方学校类型偏好
        /// </summary>
        public int PreferSchoolType { get; set; }

        /// <summary>
        /// 用户希望对方状态偏好
        /// </summary>
        public int PreferLiveState { get; set; }
    }

    public class UpdateUserPreferenceResponse
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public bool ExcuteResult { get; set; }
    }
    #endregion

    #region UpdateGoldCoin
    public class UpdateGoldCoinRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 金币变化量
        /// </summary>
        public int AlertCoinNum { get; set; }
    }

    public class UpdateGoldCoinResponse
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public bool ExcuteResult { get; set; }
    }
    #endregion

    #region GoldCoinHistory
    public class GetGoldCoinDetailsRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetGoldCoinDetailsResponse
    {
        /// <summary>
        /// 金币详情条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 金币详情列表
        /// </summary>
        public List<GoldCoinDetailsType> GoldCoinDetaislList { get; set; }
    }

    public class GoldCoinDetailsType
    {
        /// <summary>
        /// 金币变动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 金币变化量
        /// </summary>
        public string AlertCoinNum { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
    #endregion

    #region GetGoldCoinNumber
    public class GetGoldCoinNumberRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetGoldCoinNumberResponse
    {
        /// <summary>
        /// 金币总数
        /// </summary>
        public int TotalCoin { get; set; }
    }
    #endregion

    #region GetChatList
    public class GetChatListRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetChatListResponse
    {
        /// <summary>
        /// 未读消息总数量（>99条时展示99+）
        /// </summary>
        public string TotalUnReadCount { get; set; }

        /// <summary>
        /// 聊天列表集合
        /// </summary>
        public List<ChatListType> ChatList { get; set; }
    }

    public class ChatListType
    {
        /// <summary>
        /// 好友UId
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 用户展示名称（当备注为空时，为用户昵称，否则是备注）
        /// </summary>
        public string DispalyName { get; set; }

        /// <summary>
        /// 最后对话内容
        /// </summary>
        public string LatestChatContent { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentTypeEnum ChatContentType { get; set; }

        /// <summary>
        /// 最后对话时间
        /// </summary>
        public string LatestChatTime { get; set; }

        /// <summary>
        /// 未读消息数量（>99条时展示99+）
        /// </summary>
        public string UnReadCount { get; set; }
    }
    #endregion

    #region GetChatContentList
    public class GetChatContentListRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 好友UId
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 页码（分页传递数据）
        /// </summary>
        public int PageIndex { get; set; }
    }

    public class GetChatContentListReponse
    {
        
        /// <summary>
        /// 聊天详细内容
        /// </summary>
        public List<ChatContentDetail> ChatContentList { get; set; }
    }

    public class ChatContentDetail
    {
        /// <summary>
        /// 是否用户本人发出的消息
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 聊天内容
        /// </summary>
        public string ChatContent { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentTypeEnum ChatContentType { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtensionInfo { get; set; }

        /// <summary>
        /// 聊天时间
        /// </summary>
        public string ChatTime { get; set; }

        /// <summary>
        /// 创建时间(主要用于排序）
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否展示聊天时间(一般每隔10分钟会展示一次时间）
        /// </summary>
        public bool IsDisplayChatTime { get; set; }
    }
    #endregion

    #region DeleteChat
    /// <summary>
    /// 删除会话
    /// </summary>
    public class DeleteChatRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 好友UId
        /// </summary>
        public long PartnerUId { get; set; }
    }

    public class DeleteChatResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }

        /// <summary>
        /// 目前剩余未读条数
        /// </summary>
        public string CurrentTotalUnReadCount { get; set; }
    }
    #endregion
    
    #region ClearUnReadCount
    /// <summary>
    /// 清除未读条数
    /// </summary>
    public class ClearUnReadCountRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 好友UId
        /// </summary>
        public long PartnerUId { get; set; }
    }

    public class ClearUnReadCountResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }

        /// <summary>
        /// 目前剩余未读条数
        /// </summary>
        public string CurrentTotalUnReadCount { get; set; }
    }
    #endregion

    #region SendMessage
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendMessageRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 好友UId
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 聊天内容
        /// </summary>
        public string ChatContent { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentTypeEnum ChatContentType { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtensionInfo { get; set; }
    }

    public class SendMessageResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region GetMoments
    /// <summary>
    /// 获取动态请求体
    /// </summary>
    public class GetMomentsRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 动态类别
        /// </summary>
        public MomentTypeEnum MomentType { get; set; }

        /// <summary>
        /// 页码（分页传递数据）
        /// </summary>
        public int PageIndex { get; set; }
    }

    public class GetMomentsResponse
    {
        public List<MomentType> MomentList { get; set; }
    }

    public class MomentType
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public string MomentId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 用户展示名称（当备注为空时，为用户昵称，否则是备注）
        /// </summary>
        public string DispalyName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 图片内容
        /// </summary>
        public List<string> ImgContents { get; set; }
        
        /// <summary>
        /// 是否已经点赞
        /// </summary>
        public bool HasSupport { get; set; }

        /// <summary>
        /// 是否已经关注该用户
        /// </summary>
        public bool HasAttention { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int SupportCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
    }
    #endregion

    #region PublishMoment
    /// <summary>
    /// 发布动态
    /// </summary>
    public class PublishMomentRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 图片内容
        /// </summary>
        public List<UpLoadImgResponse> ImgContents { get; set; }
    }

    public class PublishMomentResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region UpLoadImg
    public class UpLoadImgResponse
    {
        /// <summary>
        /// 文件存储名
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 文件原图大小
        /// </summary>
        public long ImgLength { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string ImgMime { get; set; }
    }
    #endregion

    #region DeleteImg
    public class DeleteImgRequest
    {
        public string ImgPath { get; set; }
    }

    public class DeleteImgResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region SupportMoment
    public class SupportMomentRequest
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }
        
        /// <summary>
        /// 点赞人的Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 是否点赞（是：点赞，false：取消点赞）
        /// </summary>
        public bool IsSupport { get; set; }
    }

    public class SupportMomentResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region SupportDiscuss
    public class SupportDiscussRequest
    {
        /// <summary>
        /// 动态评论Id
        /// </summary>
        public Guid DiscussId { get; set; }

        /// <summary>
        /// 点赞人的Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 是否点赞（是：点赞，false：取消点赞）
        /// </summary>
        public bool IsSupport { get; set; }
    }

    public class SupportDiscussResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region MomentDiscuss
    public class MomentDiscussRequest
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }

        /// <summary>
        /// 评论者UId
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string DiscussContent { get; set; }
    }

    public class MomentDiscussResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region MomentDetail
    public class MomentDetailRequest
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }

        /// <summary>
        /// 当前登录人UId
        /// </summary>
        public long UId { get; set; }
    }

    public class MomentDetailResponse
    {
        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 用户展示名称（当备注为空时，为用户昵称，否则是备注）
        /// </summary>
        public string DispalyName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 图片内容
        /// </summary>
        public List<string> ImgContents { get; set; }

        /// <summary>
        /// 动态评论
        /// </summary>
        public List<DiscussType> DiscussList { get; set; }

        /// <summary>
        /// 是否已经点赞
        /// </summary>
        public bool HasSupport { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int SupportCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
}

    public class DiscussType
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid DiscussId { get; set; }

        /// <summary>
        /// 发表该评论的用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 用户展示名称（当备注为空时，为用户昵称，否则是备注）
        /// </summary>
        public string DispalyName { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string DiscussContent { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public string DiscussTime { get; set; }

        /// <summary>
        /// 我是否已经点赞
        /// </summary>
        public bool HasSupport { get; set; }

        /// <summary>
        /// 改评论点赞数
        /// </summary>
        public int SupportCount { get; set; }
    }
    #endregion

    #region MySpace
    public class MySpaceRequest
    {
        public long UId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerUId { get; set; }
    }

    public class MySpaceResponse
    {
        /// <summary>
        /// 空间背景图片
        /// </summary>
        public string BackgroundImg { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImgPath { get; set; }

        /// <summary>
        /// 我的昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 基础信息
        /// </summary>
        public string BasicInfo { get; set; }

        /// <summary>
        /// 教育和职业
        /// </summary>
        public string EducationAndCareer { get; set; }

        /// <summary>
        /// 是否已经关注
        /// </summary>
        public bool HasAttention { get; set; }

        /// <summary>
        /// 是否是本人
        /// </summary>
        public bool IsSelf { get; set; }

        /// <summary>
        /// 已经发布的动态
        /// </summary>
        public List<MySpaceMomentType> MomentList { get; set; }
    }

    public class MySpaceMomentType
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public string MomentId { get; set; }
        
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 图片内容
        /// </summary>
        public List<string> ImgContents { get; set; }
    }
    #endregion

    #region SetVisitor
    public class SetVisitorRequest
    {
        public long UId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerUId { get; set; }
    }

    public class SetVisitorResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region UpdateAttentionState
    public class UpdateAttentionStateRequest
    {
        /// <summary>
        /// 主动发起关注的Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 被关注者Id
        /// </summary>
        public long PartnerUId { get; set; }
    }

    public class UpdateAttentionStateResponse
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsExecuteSuccess { get; set; }
    }
    #endregion

    #region GetFriends
    public class GetFriendsRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 好友类别
        /// </summary>
        public FriendTypeEnum FriendType { get; set; }
    }

    public class GetFriendsResponse
    {
        public List<FriendResponseType> Friends { get; set; }
    }

    public class FriendResponseType
    {
        /// <summary>
        /// 该好友的Uid
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 展示名称（备注为空时为用户昵称）
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadPhotoPath { get; set; }

        /// <summary>
        /// 用户星座
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// 是否已经关注
        /// </summary>
        public bool HasAttention { get; set; }

        /// <summary>
        /// 时间标记
        /// </summary>
        public string TimeRemark { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum Gender { get; set; }
    }
    #endregion
}
