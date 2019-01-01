using System;
using System.Diagnostics;
using System.Web;

namespace PhotoSharing.Web.Modules
{
    public class CustomHttpModule : IHttpModule
    {
        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.RequestCompleted += RequestCompleted;
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RequestCompleted(object sender, EventArgs e)
        {
            Debug.WriteLine("Context_RequestCompleted event fired.");
        }

        /// <summary>
        /// Begins the request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("Context_BeginRequest event fired.");
        }
    }
}