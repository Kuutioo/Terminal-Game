using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailCommand : ICommands
{
    public string Name { get; set; } = "email";
    public string Example { get; set; } = "email";
    public string Description { get; set; } = "Shows email";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    private Dictionary<string, Action> emailArguments = new Dictionary<string, Action>();

    public TerminalResponseBundle Execute()
    {
        emailArguments["open"] = OpenEmail;

        Response.Clear();
        Response.ClearCommandLine();

        ShowEmails();

        if (Arguments.Length > 0)
        {
            for (int i = 0; i < Arguments.Length; i++)
            {
                string argument = Arguments[i].ToString();
                if (emailArguments.ContainsKey(argument))
                {
                    emailArguments[argument]();
                    return Response;
                }
            }
        }


        return Response;
    }

    private void ShowEmails()
    {
        Response.LoadTitle("ascii_gorillamail.txt", "green", 1);
        if (Response.showNewEmail)
            Response.GenerateEmails(UnityEngine.Random.Range(2, 5));
        else
        {
            for (int i = 0; i < Response.emailFrom.Count; i++)
            {
                string from = Response.emailFrom[i];
                string subject = Response.emailSubject[i];

                Response.EmailInterfaceEntry(from, subject);
            }
        }
    }

    private void OpenEmail()
    {
        Response.AddSpacing(1);
        for(int i = 0; i < Response.emailFrom.Count; i++)
        {
            if (Arguments[1].ToString() == i.ToString())
            {
                Response.EmailEntry(Response.emailFrom[i], "mark@gorillamail.com", Response.emailSubject[i], "124.234.665");
            }
        }
        Response.AddSpacing(1);
    }
}