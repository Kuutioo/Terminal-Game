using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class TerminalResponseBundle
{
    public List<string> response = new List<string>();

    private readonly Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"black", "#021b21"},
        {"grey", "#555d71"},
        {"red", "#ff0000"},
        {"yellow", "#ffcb00"},
        {"blue", "#9ed9d8"},
        {"purple", "#d926ff"},
        {"orange", "#ef5847"},
        {"green", "#00ff15"}
    };

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
        response.Add(ColorString(a, colors[colorA]) + ColorString(b, colors[colorB]));
    }

    public void HelpEntry(string a, string b, string c, string colorA, string colorB, string colorC)
    {
        response.Add(string.Format("Name: " + ColorString(a.PadRight(15), colors[colorA]) + " " + "Description: " + ColorString(b.PadRight(50), colors[colorB]) + " " + "Example: " + ColorString(c, colors[colorC])));
    }

    public void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));

        AddSpacing(spacing);

        while (!file.EndOfStream)
        {
            response.Add(ColorString(file.ReadLine(), colors[color]));
        }

        AddSpacing(spacing);

        file.Close();
    }

    public void AddSpacing(int spacing)
    {
        for (int i = 0; i < spacing; i++)
        {
            response.Add("");
        }
    }

    public void Add(string line)
    {
        response.Add(line);
    }

    public void Clear()
    {
        response.Clear();
    }
}

