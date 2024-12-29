using Terminal.Gui;

namespace Ribbon;

public class MyWindow : Window
{
    public static string UserName;
    
    public MyWindow()
    {
        Title = $"Ribbon ({Application.QuitKey} to quit)";

        
        // Add the views to the Window
        // Add (usernameLabel, userNameText, passwordLabel, passwordText, btnLogin);
    }

    public void Show()
    {
        Application.Run<MyWindow> ().Dispose ();
        
        // Before the application exits, reset Terminal.Gui for clean shutdown
        Application.Shutdown ();
    }
}