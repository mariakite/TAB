using System.Web.Script.Serialization;
using System.Web.Mvc;

public static class JSONHelper
{
    public static MvcHtmlString toJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return new MvcHtmlString(serializer.Serialize(obj));
    }

    public static MvcHtmlString toJSON(object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return new MvcHtmlString(serializer.Serialize(obj));
    }
}

public static class XMLHelper
{
    public static string toXML(object obj)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        try
        {
            serializer.Serialize(stream, obj);
            stream.Position = 0;
            doc.Load(stream);
            return doc.InnerXml;
        }
        catch
        {
            throw;
        }
        finally
        {
            stream.Close();
            stream.Dispose();
        }
    }
}