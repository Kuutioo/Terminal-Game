using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailCommand : ICommands
{
    public string Name { get; set; } = "email";
    public string Example { get; set; } = "email";
    public string Description { get; set; } = "Shows email";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();


    public TerminalResponseBundle Execute()
    {
        Response.Clear();
        //Response.ClearCommandLine();

        Response.EmailInterfaceEntry("dsikdkadak@gmail.com", "Hack this now!");

        if(Arguments.ToString() == "open")
        {
            Response.EmailEntry("dsikdkadak@gmail.com", "mark@gmail.com", "Hack this now!", "You need to hack this url right now. Hurry!");
        }

        return Response;
    }
}
