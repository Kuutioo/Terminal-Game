using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interpreter
{
    static Dictionary<string, ICommands> commandsDictionary = new Dictionary<string, ICommands>()
    {
        {"help", new HelpCommand()},
        {"clear", new ClearCommand()},
        {"echo", new EchoCommand()}
    };

    Dictionary<string, ITerminalCommand> validInput = new Dictionary<string, ITerminalCommand>()
    {
        {"testing", new Testing()},
    };

    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"black", "#021b21"},
        {"grey", "#555d71"},
        {"red", "#ff5879"},
        {"yellow", "#f2f1b9"},
        {"blue", "#9ed9d8"},
        {"purple", "#d926ff"},
        {"orange", "#ef5847"}
    };

    public static List<string> response = new List<string>();
    public static string[] args;

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        args = userInput.Split();

        ICommands commands;
        if(commandsDictionary.TryGetValue(args[0], out commands))
        {
            commands.Command();
        }
        else
        {
            Entry(args[0] + " is not recognized as an internal or external command. Please type help for a list of commands", "red");
        }

        return response;
    }

    public TerminalResponseBundle Interpret2(string userInput)
    {
        string command = userInput.Split()[0];
        object[] args = userInput.Split().Skip(1).ToArray();

        if (validInput.ContainsKey(command) && validInput[command].IsVisible)
        {
            ITerminalCommand c = validInput[command];
            c.Arguments = args;
            return c.Execute();
        }
        else
        {
            return new TerminalResponseBundle();
        }

    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    public void Entry(string a, string color)
    {
        response.Add(ColorString(a, colors[color]));
    }

    public void ListEntry(string a, string b, string colorA, string colorB)
    {
        response.Add(ColorString(a, colors[colorA]) + ": " + ColorString(b, colors[colorB]));
    }
}

public class TerminalResponseBundle
{
    //Important data
    public List<string> responses = new List<string>();

    //Additional settings
    public bool doesClearTerminal = false;
    public bool isAnimatedImage = false;

    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"black", "#021b21"},
        {"grey", "#555d71"},
        {"red", "#ff5879"},
        {"yellow", "#f2f1b9"},
        {"blue", "#9ed9d8"},
        {"purple", "#d926ff"},
        {"orange", "#ef5847"}
    };


    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    public void Entry(string a, string color)
    {
        responses.Add(ColorString(a, colors[color]));
    }

    public void ListEntry(string a, string b, string colorA, string colorB)
    {
        responses.Add(ColorString(a, colors[colorA]) + ": " + ColorString(b, colors[colorB]));
    }

    //Overload the add method to accept entire objects and also just strings
    public void Add(string line)
    {
        responses.Add(line);
    }

    public void Clear()
    {
        responses.Clear();
    }

}

public interface ITerminalCommand
{

    string Name { get; set; }

    bool IsVisible { get; set; }

    Dictionary<string, string> Examples { get; set; }

    string Description { get; set; }

    object[] Arguments { get; set; }

    TerminalResponseBundle Response { get; set; }

    TerminalResponseBundle Execute();
}

public class Testing : ITerminalCommand
{
    public string Name { get; set; } = "opacity";
    public bool IsVisible { get; set; } = true;
    public string Example { get; set; } = "opacity 10";
    public string Description { get; set; } = "Set the transparency of the terminal backing.";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();
    public List<string> Examples { get; set; }
    Dictionary<string, string> ITerminalCommand.Examples { get; set; }

    public TerminalResponseBundle Execute()
    {
        Response.Clear();
        Debug.Log("We are here!");
        Response.Add("Hello World!");
        Response.ListEntry("echo", "Echoes input", "red", "yellow");
        return Response;
    }
}
