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

    private Dictionary<string, ICommands> emailArguments = new Dictionary<string, ICommands>()
    {
        {"open", new OpenEmailCommand()}
    };


    public TerminalResponseBundle Execute()
    {
        Response.Clear();
        Response.ClearCommandLine();

        if (Arguments.Length > 0)
        {
            for(int i = 0; i < Arguments.Length; i++)
            {
                string argument = Arguments[i].ToString();
                if (emailArguments.ContainsKey(argument))
                {
                    ICommands c = emailArguments[argument];
                    return c.Execute();
                }
            }
        }

        Response.LoadTitle("ascii_gorillamail.txt", "green", 1);
        Response.EmailInterfaceEntry("rkfngis@gorillamail.com", "Hack this now!");

        return Response;
    }
}