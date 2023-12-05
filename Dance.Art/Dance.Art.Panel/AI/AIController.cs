using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
{
    [ApiController]
    [Route("")]
    public class AIController : ControllerBase
    {
        [HttpGet]
        [Route(".well-known/ai-plugin.json")]
        public FileStreamResult AIPlugin()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server", ".well-known", "ai-plugin.json");
            this.Response.Headers.Add("Access-Control-Allow-Origin", "https://yiyan.baidu.com");
            this.Response.Headers.Add("Connection", "close");

            return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read), "application/json");
        }

        [HttpGet]
        [Route(".well-known/openapi.yaml")]
        public FileStreamResult OpenApi()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server", ".well-known", "openapi.yaml");
            this.Response.Headers.Add("Access-Control-Allow-Origin", "https://yiyan.baidu.com");
            this.Response.Headers.Add("Connection", "close");

            return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read), "text/yaml");
        }

        [HttpGet]
        [Route("logo.png")]
        public FileStreamResult Logo()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server", "logo.png");
            this.Response.Headers.Add("Access-Control-Allow-Origin", "https://yiyan.baidu.com");
            this.Response.Headers.Add("Connection", "close");
            this.Response.Headers.Add("Cache-Control", "no-cache");

            return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read), "image/png");
        }
    }
}
