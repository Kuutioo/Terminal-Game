using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class TerminalResponseBundle
{
    public List<string> response = new List<string>();

    private Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"black", "#021b21"},
        {"grey", "#555d71"},
        {"red", "#ff0000"},
        {"yellow", "#ffcb00"},
        {"blue", "#9ed9d8"},
        {"purple", "#d926ff"},
        {"orange", "#ef5847"},
        {"white", "#ffffff"},
        {"green", "#00ff15"}
    };

    /// <summary>
    /// Easily add color to strings
    /// </summary>
    /// <param name="s"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }


    /// <summary>
    /// Add a single entry to the command line with text and a color
    /// </summary>
    /// <param name="a"></param>
    /// <param name="color"></param>
    public void Entry(string a, string color)
    {
        response.Add(ColorString(a, colors[color]));
    }

    /// <summary>
    /// Add a two entries on to the command line with texts and colors
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="colorA"></param>
    /// <param name="colorB"></param>
    public void ListEntry(string a, string b, string colorA, string colorB)
    {
        response.Add(ColorString(a, colors[colorA]) + ColorString(b, colors[colorB]));
    }

    /// <summary>
    /// Add three entries with padding for the "help" command
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="colorA"></param>
    /// <param name="colorB"></param>
    /// <param name="colorC"></param>
    public void HelpEntry(string a, string b, string c, string colorA, string colorB, string colorC)
    {
        response.Add(string.Format("Name: " + ColorString(a.PadRight(15), colors[colorA]) + " " + "Description: " + ColorString(b.PadRight(50), colors[colorB]) + " " + "Example: " + ColorString(c, colors[colorC])));
    }

    public void EmailEntry(string from, string to, string subject, string text)
    {
        Entry("From: " + from, "green");
        Entry("To: " + to, "green");
        AddSpacing(1);

        Entry("Subject: " + subject, "green");
        Entry("Text: " + text, "green");
    }

    public void EmailInterfaceEntry(string from, string subject)
    {
        response.Add(string.Format("From: " + ColorString(from.PadRight(25), colors["green"]) + "Subject: " + ColorString(subject, colors["green"])));
    }

    /// <summary>
    /// Clears the command line
    /// </summary>
    public void ClearCommandLine()
    {
        GameObject commandLine = GameObject.Find("Command Line Container");

        foreach (Transform child in commandLine.transform)
        {
            if (!child.gameObject.CompareTag("InputLine"))
            {
                Object.Destroy(child.gameObject);
            }
        }
        commandLine.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 15);
    }

    /// <summary>
    /// Load an ascii title
    /// </summary>
    /// <param name="path"></param>
    /// <param name="color"></param>
    /// <param name="spacing"></param>
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

