using Nancy;
using Nancy.Responses;
using server.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.modules
{
  public class helloworld : NancyModule
  {
    public helloworld()
    {
      Get["/", ctx=> isNotApiClient(ctx.Request)] = p => View["index.html"];
      Get["/"] = p => "welcome to the nancy api";
      //Get["/logs"] = p => new JsonResponse(new server.models.log { Message = "started server.", Severity = models.Severity.Info }, new DefaultJsonSerializer());
      ////Get["/logs/{id}"] = p => new JsonResponse(new server.models.log { Message = "your log id is " + p.id, Severity = models.Severity.Info }, new DefaultJsonSerializer());
      //Get["/logs/{id}"] = p => Response.AsJson<server.models.log>(new log { Message = "requesting for log id " + p.id }, HttpStatusCode.OK); // here we did not have to specify a serializer

      //Post["/logs"] = p =>
      //  {
      //    var message = this.Request.Form.Message;
      //    var severity = this.Request.Form.Severity;
      //    var id = this.Request.Form.Id;
      //    string url = this.Context.Request.Url + "/" + id;
      //    Console.WriteLine("received message id:" + id + "," + message + " with severity " + severity);

      //    return new Response() { StatusCode = HttpStatusCode.Accepted }.WithHeader("Location", url);
      //  };
    }

    private bool isNotApiClient(Request request)
    {
      return !request.Headers.UserAgent.ToLower().StartsWith("curl");
    }

  }
}
