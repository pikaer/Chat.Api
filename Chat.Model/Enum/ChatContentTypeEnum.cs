using System.ComponentModel;

namespace Chat.Model.Enum
{
    /// <summary>
    /// 聊天内容类别
    /// </summary>
    public enum ChatContentTypeEnum
    {
        [Description("文字")]
        Text = 0,

        [Description("表情")]
        Expression = 1,

        [Description("图片")]
        ImgContent = 2
    }
}
