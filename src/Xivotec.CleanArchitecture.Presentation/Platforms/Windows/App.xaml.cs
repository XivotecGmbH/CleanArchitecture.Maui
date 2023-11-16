using Serilog;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Xivotec.CleanArchitecture.Presentation.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Current.UnhandledException += WindowsEnvGlobalExceptionHandler;
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        private void WindowsEnvGlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Error($"An unhandled exception occurred: {e.Message}");
            e.Handled = true;
        }
    }
}