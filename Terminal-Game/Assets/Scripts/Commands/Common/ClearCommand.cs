using UnityEngine;
using System.Collections;
public class ClearCommand : ICommands
{
    public string Name { get; set; } = "clear";
    public string Example { get; set; } = "clear";
    public string Description { get; set; } = "Clears the terminal";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        Response.ClearCommandLine();

        return Response;
    }
}
