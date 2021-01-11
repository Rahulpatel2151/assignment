using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace HMS.WebApi
{
    public class BasicAuthentication:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                try
                {
                    string credentials = actionContext.Request.Headers.Authorization.Parameter;
                    string decoded_credentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
                    string[] finalCredentials = decoded_credentials.Split(',');
                    string username = finalCredentials[0];
                    string password = finalCredentials[1];
                    if (username == "user" && password == "password")//Used Static Login (DB is not used)
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                    }
                }
                catch
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}