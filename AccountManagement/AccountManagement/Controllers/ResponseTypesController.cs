using AccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace AccountManagement.Controllers
{
    public class ResponseTypesController : ApiController
    {
        //VOID Return type.
        public void Post()
        {
            //Save to database and return nothing.
        }

       //HttpResponseMessage with string Content
        public HttpResponseMessage PutData()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Successfully Updated");
            response.Content = new StringContent("hello", Encoding.Unicode);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

            /*
            HTTP/1.1 200 OK
            Cache-Control: max-age=1200
            Content-Length: 10
            Content-Type: text/plain; charset=utf-16
            Server: Microsoft-IIS/8.0
            Date: Mon, 27 Jan 2014 08:53:35 GMT

            hello
            */
        }

        //HttpResponseMessage with business model [BOOK].
        public HttpResponseMessage Get()
        {
            // Get a list of products from a database.
            IEnumerable<Book> books = new List<Book>();

            // Write the list to the response body.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, books);
            return response;
        }


    }
}
