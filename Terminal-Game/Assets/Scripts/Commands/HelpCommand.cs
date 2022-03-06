using System.Collections.Generic;
public class HelpCommand : Interpreter, ICommands
{
    public void Command()
    {
        ListEntry("echo", "Echoes input", "red", "yellow");
        ListEntry("clear", "Clears terminal", "red", "yellow");
        ListEntry("exit", "Exit program", "red", "yellow");
    }
}

