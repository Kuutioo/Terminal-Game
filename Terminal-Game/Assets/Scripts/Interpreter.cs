using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    Dictionary<string, ICommands> commandsDictionary = new Dictionary<string, ICommands>();

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

    public List<string> response = new List<string>();
    public Interpreter interpreter;
    public string[] args;

    private void Start()
    {
        interpreter = GetComponent<Interpreter>();

        commandsDictionary.Add("help", gameObject.GetComponent<HelpCommand>());
        commandsDictionary.Add("clear", gameObject.GetComponent<ClearCommand>());
        commandsDictionary.Add("echo", gameObject.GetComponent<EchoCommand>());
    }

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        args = userInput.Split();

        ICommands commands;
        if(commandsDictionary.TryGetValue(args[0], out commands))
        {
            commands.Command();
        }

        return response;

        /*
        else
        {
            Entry(userInput + " is not recognized as an internal or external command Please type help for a list of commands", "red");

            return response;
        }*/
    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    public void Entry(string a, string color)
    {
        interpreter.response.Add(ColorString(a, colors[color]));
    }

    public void ListEntry(string a, string b, string colorA, string colorB)
    {
        interpreter.response.Add(ColorString(a, colors[colorA]) + ": " + ColorString(b, colors[colorB]));
    }
}
