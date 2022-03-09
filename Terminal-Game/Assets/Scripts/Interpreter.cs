using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    private Dictionary<string, ICommands> commandsDictionary = new Dictionary<string, ICommands>()
    {
        {"help", new HelpCommand()},
        {"clear", new ClearCommand()},
        {"echo", new EchoCommand()},
        {"text_size", new TextScaleFactorCommand()},
        {"text_intensity", new TextIntensityCommand()},
        {"ascii_test", new AsciiTestCommand()}
    };

    private TerminalManager terminalManager;

    private List<string> oldInputs = new List<string>();

    private void Awake()
    {
        terminalManager = GetComponent<TerminalManager>();
        oldInputs.Add(string.Empty);
    }

    public List<string> GetOldInputs()
    {
        return oldInputs;
    }

    public TerminalResponseBundle Interpret(string userInput)
    {
        if (oldInputs.Any() && oldInputs.Last() != userInput)
        {
            oldInputs.Add(userInput);
        }
        
        terminalManager.CommandIndex = oldInputs.Count;

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