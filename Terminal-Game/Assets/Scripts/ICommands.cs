using System.Collections;
using System.Collections.Generic;
public interface ICommands
{
    // Name of the command
    string Name { get; set; }

    // Description of what it does
    string Description { get; set; }

    // Additional arguments. We handle this in the Interpreter.cs
    object[] Arguments { get; set; }

    // Reference to our TermnialResponseBundle so we can use some methods from there if we need to
    TerminalResponseBundle Response { get; set; }

    // Our command Execute() method. It returns Response so we can easily handle that in the TerminalManager.cs
    TerminalResponseBundle Execute();
}