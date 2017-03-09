using AccountManagement.Filters;
using AccountManagement.Models;
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

        //Content type from Fiddler Content-type: """""application/json ----- OR---- application/x-www-form-urlencoded"""""
        //if you have binary (non-alphanumeric) data (or a significantly sized payload) to transmit, use multipart/form-data. Otherwise, use application/x-www-form-urlencoded.
        // POST api/values
        public void Post([FromBody]string value)
        {

        }

        [Route("Post1")]
        public void Post1([FromBody]string value)
        {

        }

        /*POST With JSON
          Fiddler:
          Content-type: application/Json
          
            Body:
            {  
             UserId: "125334",
             UserName: "Girish Holla",
             Class: 25
            } 
         */


        //POST api/Values/AddUser
        [Route("AddUsers")]
        public IHttpActionResult AddUser(User user)
        {
            return Ok();
        }

        //POST api/Values/AddUser1
        //Check the modelState. Model validation. "ModelState.IsValid"
        [Route("AddUsers1")]
        public HttpResponseMessage AddUsers1(User user)
        {
            HttpResponseMessage response = null;
            if(ModelState.IsValid)
            {
                 response = Request.CreateResponse(HttpStatusCode.Created, user);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }
            return response;
        }

        //POST api/Values/AddUser2
        //Check the modelState. Model validation. "ModelState.IsValid"
        [Route("AddUsers2")]
        [CustomValidateModelAttribute]
        public HttpResponseMessage AddUsers2(User user)
        {
            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        //POST api/Values/AddUsersAndGetResponse
        [Route("AddUsersAndGetResponse")]
        public HttpResponseMessage AddUserNdGetResp(User user)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, user);
            return response;
        }

        // PUT api/values/5
        //Also need to send the formbody with "something text" and Content-Type= application/json
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

    GET:
    Can have any 1 of Get() OR GetSomething(): Always maps to the one prefixed with Get().

    If More than 1 Get Prefix, gives 500 (Internal Server Error).

    So can have [Route("GetSomething")] on one of them and use both Get() and GetSomething(). (NOTE: Make sure the action Name should Prefix with Get. Otherwise it will not work.

    api/values/GetSomething ---- [Route("GetSomething")] ---- ActionName GetSomething(). However the Route name need not have 'GET' prefix. it can be [Route("Something")]

    You can call the non 'Get' prefix action by decorating with [HttpGet] on the action.

    Passing parameter is also is similar. Need to have {id} in the Route. [Route("UserId/{id}")]

    -- Getting user details by Id [Route("UserId/{id}/UserDetails")] -- 'GET api/values/UserId/5/UserDetails'

　

    POST:
    - By default /api/Values with POST maps to default prefix with "Post" action.
    - If More than 1 Post Prefix, gives 500 (Internal Server Error).
    - So can have [Route("GetSomething")] on one of them and use both Post() and PostSomething().
    - Note: if none of the action matches, then WebApi matches with the default Post action.
    - If Passing parameter like (key=value) from request body [FromBody] is required. For JSON it is not require.
    - Content type from Fiddler Content-type: """""application/x-www-form-urlencoded"""""
    - if you have binary (non-alphanumeric) data (or a significantly sized payload) to transmit, 
    use multipart/form-data. Otherwise, use application/x-www-form-urlencoded.
    - No need to mention [HttpPost] for the post methods. Default is POST. Just add a Route[()]
    - For JSON input [FromBody] is not required in parameter.
        
https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-and-action-selection

　
    CUSTOM MODEL VALIDATION:
    - Create CustomValidateModelAttribute : ActionFilterAttribute
    - Decorate with this attribute on top of action []. 
    - OR can do it for all the actions @ the WebApiConfig.  config.Filters.Add(new CustomValidateModelAttribute());
    - Example : AddUsers2()

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
