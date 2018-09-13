using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Chat.Model.Enum
{
    public enum ChatContentEnum
    {
        /// <summary>
        ///文本
        /// </summary>
        [Description("文本")]
        Text = 0,

        /// <summary>
        ///表情
        /// </summary>
        [Description("表情")]
        Expression = 1,

        /// <summary>
        ///视频
        /// </summary>
        [Description("图片")]
        Picture = 2
        
    }
}
