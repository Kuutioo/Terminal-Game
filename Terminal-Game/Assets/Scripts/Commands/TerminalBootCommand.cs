using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBootCommand : ICommands
{
    public string Name { get; set; } = "boot";
    public string Example { get; set; } = "boot";
    public string Description { get; set; } = "Boots terminal";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        return Response;
    }
}
