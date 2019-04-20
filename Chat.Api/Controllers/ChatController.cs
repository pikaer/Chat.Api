using Chat.Interface;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 我的消息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private readonly string MODULE = "ChatController";
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 获取聊天列表
        /// </summary>
        [HttpPost]
        public JsonResult GetChatList()
        {
            RequestContext<GetChatListRequest> request = null;
            ResponseContext<GetChatListResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetChatListRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.GetChatList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetChatList", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetChatList", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 获取聊天对话详情列表
        /// </summary>
        [HttpPost]
        public JsonResult GetChatContentList()
        {
            RequestContext<GetChatContentListRequest> request = null;
            ResponseContext<GetChatContentListReponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetChatContentListRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null|| request.Content.UId<=0|| request.Content.PartnerUId<=0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.GetChatContentList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetChatContentList", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetChatContentList", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 获取未读聊天列表
        /// </summary>
        [HttpPost]
        public JsonResult GetUnReadContentList()
        {
            RequestContext<GetUnReadContentListRequest> request = null;
            ResponseContext<GetUnReadContentListReponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetUnReadContentListRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null || request.Content.UId <= 0 || request.Content.PartnerUId <= 0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.GetUnReadContentList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetUnReadContentList", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetUnReadContentList", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 删除会话
        /// </summary>
        [HttpPost]
        public JsonResult DeleteChat()
        {
            RequestContext<DeleteChatRequest> request = null;
            ResponseContext<DeleteChatResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<DeleteChatRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null || request.Content.UId <= 0 || request.Content.PartnerUId <= 0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.DeleteChat(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "DeleteChat", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "DeleteChat", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 清除未读消息
        /// </summary>
        [HttpPost]
        public JsonResult ClearUnReadCount()
        {
            RequestContext<ClearUnReadCountRequest> request = null;
            ResponseContext<ClearUnReadCountResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<ClearUnReadCountRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null || request.Content.UId <= 0 || request.Content.PartnerUId <= 0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                 response = api.ClearUnReadCount(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "ClearUnReadCount", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "ClearUnReadCount", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        [HttpPost]
        public JsonResult SendMessage()
        {
            RequestContext<SendMessageRequest> request = null;
            ResponseContext<SendMessageResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<SendMessageRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null || request.Content.UId <= 0 || request.Content.PartnerUId <= 0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.SendMessage(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SendMessage", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "SendMessage", request?.Head, response?.Head, request, response);
            }
        }
    }
}