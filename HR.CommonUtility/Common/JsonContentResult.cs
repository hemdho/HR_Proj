using Microsoft.AspNetCore.Mvc;

namespace HR.CommonUtility
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}
