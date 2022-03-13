using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TextIntensityCommand : ICommands
{
    public string Name { get; set; } = "text_intensity";
    public string Example { get; set; } = "text_intensity 0.5";
    public string Description { get; set; } = "Change the intensity of the text";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        GameObject pp = GameObject.Find("Post-Processing");
        Volume v = pp.GetComponent<Volume>();

        var en = new CultureInfo("en-US");

        try
        {
            bool isBloom = v.profile.TryGet(out Bloom b);
            bool isNumeric = float.TryParse(Arguments[0].ToString(), NumberStyles.Currency, en, out float intensityValue);

            if (intensityValue <= 1.0)
            {
                if (isBloom && isNumeric)
                {
                    b.intensity.value = intensityValue;
                    Response.Entry("Changed Intensity value to: " + intensityValue.ToString().Replace(",", "."), "green");
                }
            }
            else
            {
                Response.Entry("Please enter a floating point number between 0.0 - 5.0", "red");
            }
        }
        catch
        {
            Response.Entry("Please enter an argument in the form of a floating point number", "red");
        }

        return Response;
    }
}
