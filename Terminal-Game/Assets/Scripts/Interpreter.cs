using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interpreter
{
    private Dictionary<string, ICommands> commandsDictionary = new Dictionary<string, ICommands>()
    {
        {"help", new HelpCommand()},
        {"clear", new ClearCommand()},
        {"echo", new EchoCommand()},
        {"text_size", new TextScaleFactorCommand()}
    };

    public List<string> oldInputs = new List<string>();

    public TerminalResponseBundle Interpret(string userInput)
    {
        oldInputs.Add(userInput);
        oldInputs.Reverse();

        string command = userInput.Split()[0];
        object[] args = userInput.Split().Skip(1).ToArray();

        if (commandsDictionary.ContainsKey(command))
        {
            ICommands c = commandsDictionary[command];
            c.Arguments = args;
            return c.Execute();
        }
        else
        {
            return new DefaultErrorCommand().Execute();
        }
    }
}