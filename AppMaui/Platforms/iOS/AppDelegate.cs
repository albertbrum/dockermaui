using Foundation;

namespace AppMaui;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override AppMaui CreateAppMaui() => MauiProgram.CreateAppMaui();
}
