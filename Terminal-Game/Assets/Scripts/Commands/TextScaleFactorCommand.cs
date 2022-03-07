using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleFactorCommand : ICommands
{
    public string Name { get; set; } = "text_size";
    public string Example { get; set; } = "text_size 1.2";
    public string Description { get; set; } = "Changes size of the terminal text";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        GameObject UI = GameObject.Find("UI");
        CanvasScaler canvasScaler = UI.GetComponent<CanvasScaler>();

        var en = new CultureInfo("en-US");

        float scaleFactorValue;
        float.TryParse(Arguments[0].ToString(), NumberStyles.Currency, en, out scaleFactorValue);

        canvasScaler.scaleFactor = scaleFactorValue;
        Response.Add("Changed Scale Factor to: " + scaleFactorValue.ToString().Replace(",", "."));
        
        return Response;
    }
}
