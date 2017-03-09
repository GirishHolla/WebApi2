using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountManagement.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> GetValue()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/Something
        [Route("Something")]
        public IEnumerable<string> GetApiSomeOtherValue()

        {

            return new string[] { "SomeOtherValue1", "SomeOtherValue2" };

        }

        // GET api/values/GetExtraValue
        [Route("GetExtraValue")]
        public IEnumerable<string> GetExtraValue()

        {

            return new string[] { "GetExtraValue1", "GetExtraValue2" };

        }

        // GET api/values/XValue
        [HttpGet]
        [Route("XValue")]
        public IEnumerable<string> XValue()

        {

            return new string[] { "XValue1", "XValue2" };

        }

        // GET api/values/5
        public string Get(int id)

        {

            return "value";

        }

        // GET api/values/GetSomeId/5
        [Route("GetSomeId/{id}")]
        public string GetSomeId(int id)

        {

            return "someId " + id.ToString();

        }

        // GET api/values/UserId/5
        [HttpGet]
        [Route("UserId/{id}")]
        public string UserId(int id)

        {

            return "UserId " + id.ToString();

        }

        // GET api/values/UserId/5/UserDetails
        [HttpGet]
        [Route("UserId/{id}/UserDetails")]
        public string UserDetails(int id)

        {

            return "User details for ...UserId " + id.ToString();

        }

        // POST api/values
        public void Post([FromBody]string value)

        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)

        {

        }

        [Route("DSomeId/{id}")]
        // DELETE api/values/5
        public void Delete(int id)

        {

            //Void always return 204 No Content.

        }
    }
}

/*

Notes:

Can have any 1 of Get() OR GetSomething(): Always maps to the one prefixed with Get().

If More than 1 Get Prefix, gives 500 (Internal Server Error).

So can have [Route("GetSomething")] on one of them and use both Get() and GetSomething(). (NOTE: Make sure the action Name should Prefix with Get. Otherwise it will not work.

api/values/GetSomething ---- [Route("GetSomething")] ---- ActionName GetSomething(). However the Route name need not have 'GET' prefix. it can be [Route("Something")]

You can call the non 'Get' prefix action by decorating with [HttpGet] on the action.

Passing parameter is also is similar. Need to have {id} in the Route. [Route("UserId/{id}")]

-- Getting user details by Id [Route("UserId/{id}/UserDetails")] -- 'GET api/values/UserId/5/UserDetails'

　

https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-and-action-selection

　

Which methods on the controller are considered "actions"? When selecting an action, the framework only looks at public instance methods on the controller. Also, it excludes "special name" methods (constructors, events, operator overloads, and so forth), and methods inherited from the ApiController class.

HTTP Methods. The framework only chooses actions that match the HTTP method of the request, determined as follows:

1.You can specify the HTTP method with an attribute: AcceptVerbs, HttpDelete, HttpGet, HttpHead, HttpOptions, HttpPatch, HttpPost, or HttpPut.

2.Otherwise, if the name of the controller method starts with "Get", "Post", "Put", "Delete", "Head", "Options", or "Patch", then by convention the action supports that HTTP method.

3.If none of the above, the method supports POST.

Parameter Bindings. A parameter binding is how Web API creates a value for a parameter. Here is the default rule for parameter binding:

•Simple types are taken from the URI.

•Complex types are taken from the request body.

https://blogs.msdn.microsoft.com/webdev/2013/10/17/attribute-routing-in-asp-net-mvc-5/

https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2

　

*/
