using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PhotoSharingTests.Doubles
{

    public class FakeHttpContextForRouting : HttpContextBase
    {
        FakeHttpRequestForRouting _request;
        FakeHttpResponseForRouting _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeHttpContextForRouting"/> class.
        /// </summary>
        /// <param name="appPath">The application path.</param>
        /// <param name="requestUrl">The request URL.</param>
        public FakeHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
        {
            _request = new FakeHttpRequestForRouting(appPath, requestUrl);
            _response = new FakeHttpResponseForRouting();
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.
        /// </summary>
        public override HttpRequestBase Request
        {
            get { return _request; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.
        /// </summary>
        public override HttpResponseBase Response
        {
            get { return _response; }
        }
    }

    public class FakeHttpRequestForRouting : HttpRequestBase
    {
        string _appPath;
        string _requestUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeHttpRequestForRouting"/> class.
        /// </summary>
        /// <param name="appPath">The application path.</param>
        /// <param name="requestUrl">The request URL.</param>
        public FakeHttpRequestForRouting(string appPath, string requestUrl)
        {
            _appPath = appPath;
            _requestUrl = requestUrl;
        }

        public override string ApplicationPath
        {
            get { return _appPath; }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return _requestUrl; }
        }

        public override string PathInfo
        {
            get { return ""; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }
    }

    public class FakeHttpResponseForRouting : HttpResponseBase
    {
        /// <summary>
        /// Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state, and returns the combined path.
        /// </summary>
        /// <param name="virtualPath">The virtual path of a resource.</param>
        /// <returns>
        /// The virtual path, with the session ID inserted.
        /// </returns>
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }

}
