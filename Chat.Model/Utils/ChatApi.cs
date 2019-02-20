namespace Chat.Model.Utils
{
    /**
    * 以下类都是和前端交互的实体，后面的只要是接口直接返回的类
    * 都放在此处，和DTO,Entity完全分离
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
        /// 头像路径
        /// </summary>
        public string HeadPhotoPath { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }
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
    public class GetGoldCoinHistoryRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }
    }

    public class GetGoldCoinHistoryResponse
    {
        /// <summary>
        /// 金币变动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 金币变化量
        /// </summary>
        public int AlertCoinNum { get; set; }

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

    
}
