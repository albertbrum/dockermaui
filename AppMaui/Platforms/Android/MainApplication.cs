using Android.App;
using Android.Runtime;

namespace AppMaui;

[Application]
public class MainApplication : AppMauilication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override AppMaui CreateAppMaui() => MauiProgram.CreateAppMaui();
}
