using System.Collections.Generic;
public class HelpCommand : ICommands
{
    public string Name { get; set; } = "help";
    public string Example { get; set; } = "help";
    public string Description { get; set; } = "Displays a list of help commands";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {

        Response.Clear();

        Response.HelpEntry(Name, Description, Example, "yellow", "yellow", "yellow");
        Response.HelpEntry("echo", "Echoes input", "echo hello world", "yellow", "yellow", "yellow");
        Response.HelpEntry("clear", "Clears termnial of text", "clear", "yellow", "yellow", "yellow");
        Response.HelpEntry("text_size", "Changes size of the text (1.0 - 2.1)", "text_size 1.2", "yellow", "yellow", "yellow");

        return Response;
    }
}



