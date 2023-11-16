using ObjCRuntime;
using Serilog;
using UIKit;

namespace Xivotec.CleanArchitecture.Presentation
{
    public class Program
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Runtime.MarshalManagedException += MacCatalystEnvGlobalExceptionHandler;

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, typeof(AppDelegate));
        }

        private static void MacCatalystEnvGlobalExceptionHandler(object sender, MarshalManagedExceptionEventArgs e)
        {
            Log.Logger.Error($"An unhandled exception occurred: {e.Exception.Message}");
        }
    }
}