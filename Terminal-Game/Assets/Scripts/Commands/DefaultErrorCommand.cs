using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultErrorCommand : ICommands
{
    public string Name { get; set; }
    public string Example { get; set; }
    public string Description { get; set; } = "If not recognized command, display this message";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        Response.Entry("Invalid command, please type help to see a list of commands", "red");

        return Response;
    }
}
