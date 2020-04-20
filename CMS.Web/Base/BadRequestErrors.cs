using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HVITCore.Controllers
{
    public class BadRequestErrors : IHttpActionResult
    {
        private List<string> messages;
        private HttpRequestMessage request;

        public BadRequestErrors(List<string> messages, HttpRequestMessage request)
        {
            this.messages = messages;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = request.CreateResponse(HttpStatusCode.BadRequest, messages);
            return Task.FromResult(response);
        }
    }
}