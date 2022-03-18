using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleFactorCommand : ICommands
{
    public string Name { get; set; } = "text_size";
    public string Example { get; set; } = "text_size 0.6";
    public string Description { get; set; } = "Changes size of the terminal text";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        GameObject UI = GameObject.Find("UI");
        CanvasScaler canvasScaler = UI.GetComponent<CanvasScaler>();

        var en = new CultureInfo("en-US");

        try
        {
            bool isNumeric = float.TryParse(Arguments[0].ToString(), NumberStyles.Currency, en, out float scaleFactorValue);

            if (scaleFactorValue! >= 0.0 && scaleFactorValue! <= 1.0)
            {
                if (isNumeric)
                {
                    canvasScaler.matchWidthOrHeight = scaleFactorValue;
                    Response.Entry("Changed Scale Factor to: " + scaleFactorValue.ToString().Replace(",", "."), "green");
                }
            }
            else
            {
                Response.Entry("Please enter a floating point number between 0.0 - 1.0", "red");
            }
        }
        catch
        {
            Response.Entry("Please enter an argument in the form of a floating point number", "red");
        }

        return Response;
    }
}
