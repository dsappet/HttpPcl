using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HttpPcl.CustomExceptions;
using HttpPcl.Utilities;

namespace HttpPcl
{
    public class HttpAccess
    {
        public static async Task<string> HttpPostAsync(string uri, string body, Dictionary<string, string> requestHeaders = null) // headers is an optional parameter 
        {

            using (HttpClient hc = new HttpClient())
            {
                hc.Timeout = TimeSpan.FromSeconds(10);
                string resultString = null;
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                // Loop through the headers dictionary and add the key value pairs
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                //try
                //{
                var postResult = await hc.PostAsync(uri, content).ConfigureAwait(false); // . result

                switch (postResult.StatusCode)
                {
                    case HttpStatusCode.OK:
                        // Everything is good return
                        break;
                    case HttpStatusCode.Unauthorized:
                        // Throw custom unauthorized exception
                        throw new ApiUnauthorizedException();

                    default:
                        throw new ApiGenericException();

                        #region un-used_codes

                        //case HttpStatusCode.Accepted:
                        //    break;
                        //case HttpStatusCode.Ambiguous:
                        //    break;
                        //case HttpStatusCode.BadGateway:
                        //    break;
                        //case HttpStatusCode.BadRequest:
                        //    break;
                        //case HttpStatusCode.Conflict:
                        //    break;
                        //case HttpStatusCode.Continue:
                        //    break;
                        //case HttpStatusCode.Created:
                        //    break;
                        //case HttpStatusCode.ExpectationFailed:
                        //    break;
                        //case HttpStatusCode.Forbidden:
                        //    break;
                        //case HttpStatusCode.Found:
                        //    break;
                        //case HttpStatusCode.GatewayTimeout:
                        //    break;
                        //case HttpStatusCode.Gone:
                        //    break;
                        //case HttpStatusCode.HttpVersionNotSupported:
                        //    break;
                        //case HttpStatusCode.InternalServerError:
                        //    break;
                        //case HttpStatusCode.LengthRequired:
                        //    break;
                        //case HttpStatusCode.MethodNotAllowed:
                        //    break;
                        //case HttpStatusCode.Moved:
                        //    break;
                        //case HttpStatusCode.NoContent:
                        //    break;
                        //case HttpStatusCode.NonAuthoritativeInformation:
                        //    break;
                        //case HttpStatusCode.NotAcceptable:
                        //    break;
                        //case HttpStatusCode.NotFound:
                        //    break;
                        //case HttpStatusCode.NotImplemented:
                        //    break;
                        //case HttpStatusCode.NotModified:
                        //    break;

                        //case HttpStatusCode.PartialContent:
                        //    break;
                        //case HttpStatusCode.PaymentRequired:
                        //    break;
                        //case HttpStatusCode.PreconditionFailed:
                        //    break;
                        //case HttpStatusCode.ProxyAuthenticationRequired:
                        //    break;
                        //case HttpStatusCode.RedirectKeepVerb:
                        //    break;
                        //case HttpStatusCode.RedirectMethod:
                        //    break;
                        //case HttpStatusCode.RequestedRangeNotSatisfiable:
                        //    break;
                        //case HttpStatusCode.RequestEntityTooLarge:
                        //    break;
                        //case HttpStatusCode.RequestTimeout:
                        //    break;
                        //case HttpStatusCode.RequestUriTooLong:
                        //    break;
                        //case HttpStatusCode.ResetContent:
                        //    break;
                        //case HttpStatusCode.ServiceUnavailable:
                        //    break;
                        //case HttpStatusCode.SwitchingProtocols:
                        //    break;
                        //case HttpStatusCode.UnsupportedMediaType:
                        //    break;
                        //case HttpStatusCode.Unused:
                        //    break;
                        //case HttpStatusCode.UpgradeRequired:
                        //    break;
                        //case HttpStatusCode.UseProxy:
                        //    break;
                        //default:
                        //    throw new ArgumentOutOfRangeException();

                        #endregion
                }

                resultString = await postResult.Content.ReadAsStringAsync();

                //}catch (Exception ex)
                //{
                //    // Some other excetion happened here. 
                //    var msg = ex.ToString();
                //    throw new ApiGenericException("Error in http call. Inner Exception:  " + msg);
                //}
                return resultString;
            }
            //              Stream vs = await result;
            //            StreamReader am = new StreamReader(vs);

            // return await am.ReadToEndAsync();


        }

        // Overload to pass in a class object as a parameter instead
        public static async Task<string> HttpGetAsync<T>(string uri, T argument = default(T),
            Dictionary<string, string> requestHeaders = null) // headers is an optional parameter
        {
            var arguments = HttpUtilities.BuildArguments(argument);
            return await HttpGetAsync(uri, arguments, requestHeaders);
        } 
        public static async Task<string> HttpGetAsync(string uri, string arguments = null, Dictionary<string, string> requestHeaders = null) // headers is an optional parameter 
        {
            // Let's build the uri string 
            var builder = new StringBuilder();
            builder = builder.Append(uri.Trim());
            if (arguments != null)
            {
                if (builder.ToString().EndsWith("/")) builder = builder.Remove(builder.Length - 1, 1); // remove trailing slash if present

                builder = builder.Append(arguments);
            }

            using (HttpClient hc = new HttpClient())
            {
                hc.Timeout = TimeSpan.FromSeconds(10);
                string resultString = null;
                //  var content = new StringContent(body, Encoding.UTF8, "application/json");
                // Loop through the headers dictionary and add the key value pairs
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var getResult = await hc.GetAsync(builder.ToString()); // PostAsync(uri, content).ConfigureAwait(false); // . result

                switch (getResult.StatusCode)
                {
                    case HttpStatusCode.OK:
                        // Everything is good return
                        break;
                    case HttpStatusCode.Unauthorized:
                        // Throw custom unauthorized exception
                        throw new ApiUnauthorizedException();

                    default:
                        throw new ApiGenericException();

                        #region un-used_codes

                        //case HttpStatusCode.Accepted:
                        //    break;
                        //case HttpStatusCode.Ambiguous:
                        //    break;
                        //case HttpStatusCode.BadGateway:
                        //    break;
                        //case HttpStatusCode.BadRequest:
                        //    break;
                        //case HttpStatusCode.Conflict:
                        //    break;
                        //case HttpStatusCode.Continue:
                        //    break;
                        //case HttpStatusCode.Created:
                        //    break;
                        //case HttpStatusCode.ExpectationFailed:
                        //    break;
                        //case HttpStatusCode.Forbidden:
                        //    break;
                        //case HttpStatusCode.Found:
                        //    break;
                        //case HttpStatusCode.GatewayTimeout:
                        //    break;
                        //case HttpStatusCode.Gone:
                        //    break;
                        //case HttpStatusCode.HttpVersionNotSupported:
                        //    break;
                        //case HttpStatusCode.InternalServerError:
                        //    break;
                        //case HttpStatusCode.LengthRequired:
                        //    break;
                        //case HttpStatusCode.MethodNotAllowed:
                        //    break;
                        //case HttpStatusCode.Moved:
                        //    break;
                        //case HttpStatusCode.NoContent:
                        //    break;
                        //case HttpStatusCode.NonAuthoritativeInformation:
                        //    break;
                        //case HttpStatusCode.NotAcceptable:
                        //    break;
                        //case HttpStatusCode.NotFound:
                        //    break;
                        //case HttpStatusCode.NotImplemented:
                        //    break;
                        //case HttpStatusCode.NotModified:
                        //    break;

                        //case HttpStatusCode.PartialContent:
                        //    break;
                        //case HttpStatusCode.PaymentRequired:
                        //    break;
                        //case HttpStatusCode.PreconditionFailed:
                        //    break;
                        //case HttpStatusCode.ProxyAuthenticationRequired:
                        //    break;
                        //case HttpStatusCode.RedirectKeepVerb:
                        //    break;
                        //case HttpStatusCode.RedirectMethod:
                        //    break;
                        //case HttpStatusCode.RequestedRangeNotSatisfiable:
                        //    break;
                        //case HttpStatusCode.RequestEntityTooLarge:
                        //    break;
                        //case HttpStatusCode.RequestTimeout:
                        //    break;
                        //case HttpStatusCode.RequestUriTooLong:
                        //    break;
                        //case HttpStatusCode.ResetContent:
                        //    break;
                        //case HttpStatusCode.ServiceUnavailable:
                        //    break;
                        //case HttpStatusCode.SwitchingProtocols:
                        //    break;
                        //case HttpStatusCode.UnsupportedMediaType:
                        //    break;
                        //case HttpStatusCode.Unused:
                        //    break;
                        //case HttpStatusCode.UpgradeRequired:
                        //    break;
                        //case HttpStatusCode.UseProxy:
                        //    break;
                        //default:
                        //    throw new ArgumentOutOfRangeException();

                        #endregion
                }
                resultString = await getResult.Content.ReadAsStringAsync();
                return resultString;
            }
        }


        public static string HttpGet<T>(string uri, T argument = default(T),
            Dictionary<string, string> requestHeaders = null) // headers is an optional parameter 
        {
            var arguments = HttpUtilities.BuildArguments(argument);
            return HttpGet(uri, arguments, requestHeaders);
            //return HttpGetAsync(uri, argument, requestHeaders).Result;
        }

        public static string HttpGet(string uri, string arguments = null, Dictionary<string, string> requestHeaders = null) // headers is an optional parameter 
        {
            #region replaced with working code
            //return HttpGetAsync(uri, arguments, requestHeaders).Result;
            #endregion
            // Let's build the uri string 
            var builder = new StringBuilder();
            builder = builder.Append(uri.Trim());
            if (arguments != null)
            {
                if (builder.ToString().EndsWith("/")) builder = builder.Remove(builder.Length - 1, 1); // remove trailing slash if present
                builder = builder.Append(arguments);

            }

            using (HttpClient hc = new HttpClient())
            {
                hc.Timeout = TimeSpan.FromSeconds(10);
                string resultString = null;
                //  var content = new StringContent(body, Encoding.UTF8, "application/json");
                // Loop through the headers dictionary and add the key value pairs
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                HttpResponseMessage getResult = new HttpResponseMessage();
                try
                {
                    getResult = hc.GetAsync(builder.ToString()).Result;
                    // PostAsync(uri, content).ConfigureAwait(false); // . result
                }
                catch (Exception ex)
                {
                    // something bad happened
                    // TODO Log an error
                }
                switch (getResult.StatusCode)
                {
                    case HttpStatusCode.OK:
                        // Everything is good return
                        break;
                    case HttpStatusCode.Unauthorized:
                        // Throw custom unauthorized exception
                        throw new ApiUnauthorizedException();

                    default:
                        throw new ApiGenericException();

                        #region un-used_codes

                        //case HttpStatusCode.Accepted:
                        //    break;
                        //case HttpStatusCode.Ambiguous:
                        //    break;
                        //case HttpStatusCode.BadGateway:
                        //    break;
                        //case HttpStatusCode.BadRequest:
                        //    break;
                        //case HttpStatusCode.Conflict:
                        //    break;
                        //case HttpStatusCode.Continue:
                        //    break;
                        //case HttpStatusCode.Created:
                        //    break;
                        //case HttpStatusCode.ExpectationFailed:
                        //    break;
                        //case HttpStatusCode.Forbidden:
                        //    break;
                        //case HttpStatusCode.Found:
                        //    break;
                        //case HttpStatusCode.GatewayTimeout:
                        //    break;
                        //case HttpStatusCode.Gone:
                        //    break;
                        //case HttpStatusCode.HttpVersionNotSupported:
                        //    break;
                        //case HttpStatusCode.InternalServerError:
                        //    break;
                        //case HttpStatusCode.LengthRequired:
                        //    break;
                        //case HttpStatusCode.MethodNotAllowed:
                        //    break;
                        //case HttpStatusCode.Moved:
                        //    break;
                        //case HttpStatusCode.NoContent:
                        //    break;
                        //case HttpStatusCode.NonAuthoritativeInformation:
                        //    break;
                        //case HttpStatusCode.NotAcceptable:
                        //    break;
                        //case HttpStatusCode.NotFound:
                        //    break;
                        //case HttpStatusCode.NotImplemented:
                        //    break;
                        //case HttpStatusCode.NotModified:
                        //    break;

                        //case HttpStatusCode.PartialContent:
                        //    break;
                        //case HttpStatusCode.PaymentRequired:
                        //    break;
                        //case HttpStatusCode.PreconditionFailed:
                        //    break;
                        //case HttpStatusCode.ProxyAuthenticationRequired:
                        //    break;
                        //case HttpStatusCode.RedirectKeepVerb:
                        //    break;
                        //case HttpStatusCode.RedirectMethod:
                        //    break;
                        //case HttpStatusCode.RequestedRangeNotSatisfiable:
                        //    break;
                        //case HttpStatusCode.RequestEntityTooLarge:
                        //    break;
                        //case HttpStatusCode.RequestTimeout:
                        //    break;
                        //case HttpStatusCode.RequestUriTooLong:
                        //    break;
                        //case HttpStatusCode.ResetContent:
                        //    break;
                        //case HttpStatusCode.ServiceUnavailable:
                        //    break;
                        //case HttpStatusCode.SwitchingProtocols:
                        //    break;
                        //case HttpStatusCode.UnsupportedMediaType:
                        //    break;
                        //case HttpStatusCode.Unused:
                        //    break;
                        //case HttpStatusCode.UpgradeRequired:
                        //    break;
                        //case HttpStatusCode.UseProxy:
                        //    break;
                        //default:
                        //    throw new ArgumentOutOfRangeException();

                        #endregion
                }
                try
                {
                    resultString = getResult.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    resultString = "An exception has occured: " + ex.ToString();
                    throw new ApiGenericException(ex.ToString());
                }

                return resultString;
            }
        }
    }
}
