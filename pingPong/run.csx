using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
#    log.Info("Request Headers" + req.Headers.ToString());
#    log.Info("Request Properties" + req.Properties.ToString());
#    log.Info("Request Content" + req.Content.ToString());
    string documentContents;
    using (Stream receiveStream = req.InputStream)
    {
        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
        {
            documentContents = readStream.ReadToEnd();
        }
    }
    return Log.Info("Request body: " +documentContents);
    return req.CreateResponse(HttpStatusCode.OK, "pong");
}