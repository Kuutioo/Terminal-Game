using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsciiTestCommand : ICommands
{
    public string Name { get; set; } = "ascii_test";
    public string Example { get; set; } = "ascii_test";
    public string Description { get; set; } = "Renders the word 'test' in ascii";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        Response.LoadTitle("ascii_test.txt", "red", 2);

        return Response;
    }
}
