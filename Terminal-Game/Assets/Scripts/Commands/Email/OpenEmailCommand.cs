using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEmailCommand : ICommands
{
    public string Name { get; set; } = "open";
    public string Example { get; set; } = "email open 1";
    public string Description { get; set; } = "Opens desired email where last argument is the number of the email";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();


    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        Response.Add("Brrrrr");

        return Response;
    }
}
