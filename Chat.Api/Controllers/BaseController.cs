using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Chat.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string GetInputString()
        {
            Stream req = Request.Body;
            req.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            if (!string.IsNullOrEmpty(json))
            {
                while (json.IndexOf("\\/", StringComparison.Ordinal) != -1) json = json.Replace("\\/", "/");
            }

            return json;
        }
    }
}