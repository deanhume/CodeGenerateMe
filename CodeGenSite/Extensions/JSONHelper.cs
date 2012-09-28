namespace CodeGenSite.Extensions
{
    using System.Web.Script.Serialization;

    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer {RecursionLimit = recursionDepth};
            return serializer.Serialize(obj);
        }
    }
}