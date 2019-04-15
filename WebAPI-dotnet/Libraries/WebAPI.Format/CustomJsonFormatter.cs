using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace WebAPI.Format
{
    /// <summary>
    /// Set the response data format to json
    /// </summary>
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// 當Request從瀏覽器發出時，回傳JSON；來自Fiddler或Postman這類工具時，根據其Accept Header中的設定來回傳對應格式。
        /// </summary>
        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="headers"></param>
        /// <param name="mediaType"></param>
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
