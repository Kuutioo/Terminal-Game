using System.Collections;
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

        Response.Entry("Common", "yellow");
        Response.HelpEntry(Name, Description, Example, "yellow", "yellow", "yellow");
        Response.HelpEntry("echo", "Echoes input", "echo hello world", "yellow", "yellow", "yellow");
        Response.HelpEntry("clear", "Clears termnial of text", "clear", "yellow", "yellow", "yellow");

        Response.AddSpacing(1);
        Response.Entry("Text", "yellow");
        Response.HelpEntry("text_size", "Changes size of the text (1.0 - 2.5)", "text_size 1.2", "yellow", "yellow", "yellow");
        Response.HelpEntry("text_intensity", "Change the intensity of the text (0.0 - 1.0)", "text_intensity 0.5", "yellow", "yellow", "yellow");

        Response.AddSpacing(1);
        Response.Entry("Email", "yellow");
        Response.HelpEntry("email", "Opens email inbox", "email", "yellow", "yellow", "yellow");
        Response.HelpEntry("email open", "Opens specific email", "email open 0", "yellow", "yellow", "yellow");

        Response.AddSpacing(1);
        Response.Entry("Other", "yellow");
        Response.HelpEntry("ascii_test", "Renders the word 'test' in ascii", "ascii_test", "yellow", "yellow", "yellow");

        return Response;
    }
}


