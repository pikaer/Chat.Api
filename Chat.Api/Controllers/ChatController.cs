using Chat.Model.DTO.Chat;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Service;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 我的消息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private ChatService _chatService = new ChatService();

        /// <summary>
        /// 获取聊天列表
        /// </summary>
        [HttpPost]
        public JsonResult GetChatList()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<ChatListRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var res = _chatService.GetChatList(request.Content);
                var response = new ResponseContext<List<ChatListDTO>>(res);
                return new JsonResult(response);
            }
            catch
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError);
            }
        }
        
        /// <summary>
        /// 清理未读提示
        /// </summary>
        [HttpPost]
        public JsonResult ClearUnRead()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<CommonRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var res = _chatService.ClearUnRead(request.Content);
                var response = new ResponseContext<bool>(res);
                return new JsonResult(response);
            }
            catch
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError);
            }
        }

        /// <summary>
        /// 获取聊天内容列表
        /// </summary>
        [HttpPost]
        public JsonResult GetChatContentList()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<CommonRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var res = _chatService.GetChatContentList(request.Content);
                var response = new ResponseContext<List<ChatContentListDTO>>(res);
                return new JsonResult(response);
            }
            catch
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError);
            }
        }
    }
}