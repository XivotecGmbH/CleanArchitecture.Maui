using Android.App;
using Android.Runtime;
using Serilog;

namespace Xivotec.CleanArchitecture.Presentation
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvGlobalExceptionHandler;
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        private void AndroidEnvGlobalExceptionHandler(object sender, RaiseThrowableEventArgs e)
        {
            Log.Logger.Error($"An unhandled exception occurred: {e.Exception.Message}");
            e.Handled = true;
        }
    }
}