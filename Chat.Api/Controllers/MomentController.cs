using Chat.Interface;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 动态(首页)接口集合
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MomentController : BaseController
    {
        private readonly string MODULE = "MomentController";
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 获取动态
        /// </summary>
        [HttpPost]
        public JsonResult GetMoments()
        {
            RequestContext<GetMomentsRequest>request=null;
            ResponseContext<GetMomentsResponse>response=null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetMomentsRequest>>();
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
                response = api.GetMoments(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetMoments", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetMoments", request?.Head, response?.Head, request, response);
            }
        }
        
        /// <summary>
        /// 发布动态
        /// </summary>
        [HttpPost]
        public JsonResult PublishMoment()
        {
            RequestContext<PublishMomentRequest> request = null;
            ResponseContext<PublishMomentResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<PublishMomentRequest>>();
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
                response = api.PublishMoment(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "PublishMoment", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "PublishMoment", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 上传动态图片
        /// </summary>
        [HttpPost]
        public JsonResult UpLoadImg()
        {
            var response = new ResponseContext<UpLoadImgResponse>()
            {
                Head = new ResponseHead(true, ErrCodeEnum.Success, "上传成功")
            };
            try
            {
                var uploadfile = Request.Form.Files[0];

                var filePath = JsonSettingHelper.AppSettings["SaveMomentImg"];

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (uploadfile == null)
                {
                    response.Head = new ResponseHead()
                    {
                        Success = false,
                        Code = ErrCodeEnum.Failure,
                        Msg = "上传文件失败"
                    };
                    return new JsonResult(response);
                }

                //文件后缀
                var fileExtension = Path.GetExtension(uploadfile.FileName);

                //判断后缀是否是图片
                const string fileFilt = "|.gif|.jpg|.php|.jsp|.jpeg|.png|";
                if (fileExtension == null)
                {
                    response.Head = new ResponseHead()
                    {
                        Success = false,
                        Code = ErrCodeEnum.Failure,
                        Msg = "上传的文件没有后缀"
                    };
                    return new JsonResult(response);
                }
                if (!fileFilt.Contains(fileExtension))
                {
                    response.Head = new ResponseHead()
                    {
                        Success = false,
                        Code = ErrCodeEnum.Failure,
                        Msg = "上传的文件不是图片"
                    };
                    return new JsonResult(response);
                }

                //判断文件大小    
                long length = uploadfile.Length;
                if (length > 1024 * 1024 * 10) //1M
                {
                    response.Head = new ResponseHead()
                    {
                        Success = false,
                        Code = ErrCodeEnum.Failure,
                        Msg = "上传的文件不能大于10M"
                    };
                    return new JsonResult(response);
                }

                var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                var saveName = strDateTime + strRan + fileExtension;

                using (FileStream fs = System.IO.File.Create(filePath + saveName))
                {
                    uploadfile.CopyTo(fs);
                    fs.Flush();
                }

                response.Content = new UpLoadImgResponse()
                {
                    ImgPath = saveName,
                    ImgLength= length,
                    ImgMime= fileExtension

                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpLoadImg", ex);
            }
        }

        /// <summary>
        /// 删除已上传的图片
        /// </summary>
        [HttpPost]
        public JsonResult DeleteImg()
        {
            RequestContext<DeleteImgRequest> request = null;
            ResponseContext<DeleteImgResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<DeleteImgRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null|| request.Content.ImgPath.IsNullOrEmpty())
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                response = api.DeleteImg(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "DeleteImg", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "DeleteImg", request?.Head, response?.Head, request, response);
            }
        }
    }
}