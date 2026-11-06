using Microsoft.UI.Xaml;

namespace DynamicIslandWinUI;

public partial class App : Application
{
    public static Window? Window { get; private set; }

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Window = new MainWindow();
        Window.Activate();
    }
}
