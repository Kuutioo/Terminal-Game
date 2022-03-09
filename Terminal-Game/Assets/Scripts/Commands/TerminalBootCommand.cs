using System.Collections;
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
        ClearCommandLine();
        Response.Clear();

        Response.Add("Initializing terminal");


        return Response;
    }

    private void ClearCommandLine()
    {
        GameObject commandLine = GameObject.Find("Command Line Container");

        foreach (Transform child in commandLine.transform)
        {
            Object.Destroy(child.gameObject);
        }
        commandLine.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 0);
    }
}
