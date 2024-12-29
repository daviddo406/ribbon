using System.Data;
using Terminal.Gui;

namespace Ribbon;

public class MyWindow : Window
{
    public static string UserName;
    
    TableView TableView;

    public MyWindow()
    {
        Title = $"Ribbon ({Application.QuitKey} to quit)";


        TableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = 50,
            Height = 10,
        };

        // Add the views to the Window
        // Add (usernameLabel, userNameText, passwordLabel, passwordText, btnLogin);
    }

    public void AddData(DataTable table)
    {
        TableView.Table = new DataTableSource(table);
    }

    public void Show()
    {
        Application.Run<MyWindow> ().Dispose ();
        Add(TableView);

        // Before the application exits, reset Terminal.Gui for clean shutdown
        Application.Shutdown ();
    }
}