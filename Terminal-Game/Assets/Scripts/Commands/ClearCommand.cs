using System.Collections.Generic;
using UnityEngine;
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

        GameObject commandLine = GameObject.Find("Command Line Container");

        foreach (Transform child in commandLine.transform)
        {
            if (!child.gameObject.CompareTag("InputLine"))
            {
                Object.Destroy(child.gameObject);
            }
        }
        return Response;
    }
}
