using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoCommand : Interpreter, ICommands
{
    public void Command()
    {
        string text = string.Empty;

        for (int i = 1; i < interpreter.args.Length; i++)
        {
            text += interpreter.args[i] + " ";
            Debug.Log(text);
        }

        interpreter.response.Add(text);
    }
}
