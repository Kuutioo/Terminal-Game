using System.Collections.Generic;
public class EchoCommand : ICommands
{
    public string Name { get; set; } = "echo";
    public string Example { get; set; } = "echo 'user text here'";
    public string Description { get; set; } = "Print text to terminal";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();
        string text = string.Empty;

        for (int i = 0; i < Arguments.Length; i++)
        {
            text += Arguments[i] + " ";
        }

        Response.Add(text);
        return Response;
    }
}
