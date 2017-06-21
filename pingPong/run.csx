using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
#    log.Info("Request Headers" + req.Headers.ToString());
#    log.Info("Request Properties" + req.Properties.ToString());
#    log.Info("Request Content" + req.Content.ToString());
    string jsonContent = await req.Content.ReadAsStringAsync();
    log.Info("Request: " + jsonContent);

    return req.CreateResponse(HttpStatusCode.OK, "pong");
}