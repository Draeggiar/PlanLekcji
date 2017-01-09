using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Plan_lekcji.Extensions
{
    public static class HtmlHelperExtension
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper,
            object model)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }
    }
}