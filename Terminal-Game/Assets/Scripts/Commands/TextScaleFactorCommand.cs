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

        try
        {
            bool isNumeric = float.TryParse(Arguments[0].ToString(), NumberStyles.Currency, en, out scaleFactorValue);
            if (isNumeric)
            {
                canvasScaler.scaleFactor = scaleFactorValue;
                Response.Entry("Changed Scale Factor to: " + scaleFactorValue.ToString().Replace(",", "."), "green");
            }
            else
            {
                Response.Entry("Please enter a floating point number as an argument", "red");
            }
        }
        catch
        {
            Response.Entry("Please enter an argument in the form of a floating point number", "red");
        }
        
        return Response;
    }
}
