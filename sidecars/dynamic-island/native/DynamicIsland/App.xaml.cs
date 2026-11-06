using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace DynamicIsland;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        AppDomain.CurrentDomain.UnhandledException += (s, args) =>
        {
            File.WriteAllText("crash.log", args.ExceptionObject?.ToString());
        };
        DispatcherUnhandledException += (s, args) =>
        {
            File.WriteAllText("crash_dispatcher.log", args.Exception.ToString());
        };
        base.OnStartup(e);
    }
}
