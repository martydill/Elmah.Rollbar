using System;
using System.Diagnostics;
using System.Web;
using RollbarSharp;

namespace Elmah.Rollbar
{
    public sealed class RollbarErrorModule : HttpModuleBase
    {
        private HttpApplication _application;

        protected override bool SupportDiscoverability
        {
            get { return true; }
        }

        protected override void OnInit(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            _application = application;
            _application.Error += OnError;
            ErrorSignal.Get(_application).Raised += OnErrorRaised;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            ErrorSignal.Get(_application).Raised -= OnErrorRaised;
            _application.Error -= OnError;
        }

        private void OnError(object sender, EventArgs e)
        {
            SendToRollbar(_application.Server.GetLastError());
        }

        private void OnErrorRaised(object sender, ErrorSignalEventArgs args)  
        {
            SendToRollbar(args.Exception);
        }

        private void SendToRollbar(Exception error)
        {
            try
            {
                if (error == null)
                    throw new ArgumentNullException("error");

                (new RollbarClient()).SendException(error);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
        }
    }
}