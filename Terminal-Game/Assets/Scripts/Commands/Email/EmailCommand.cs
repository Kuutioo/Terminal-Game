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
        emailArguments["delete"] = DeleteEmail;

        Response.Clear();
        Response.ClearCommandLine();

        if (Arguments.Length > 0)
        {   
            for(int i = 0; i < Arguments.Length; i++)
            {
                string argument = Arguments[i].ToString();
                if (emailArguments.ContainsKey(argument))
                {
                    emailArguments[argument]();
                    return Response;
                }
            }
        }

        Response.LoadTitle("ascii_gorillamail.txt", "green", 1);
        if (Response.showNewEmail)
            Response.GenerateEmails(UnityEngine.Random.Range(2, 5));
        else
        {
            for(int i = 0; i < Response.emailFrom.Count; i++)
            {
                string from = Response.emailFrom[i];
                string subject = Response.emailSubject[i];

                Response.EmailInterfaceEntry(from, subject);
            }
        }

        return Response;
    }

    private void OpenEmail()
    {
        if (Arguments[1].ToString() == "1")
        {
            Response.EmailEntry(Response.emailFrom[0], "mark@gorillamail.com", Response.emailSubject[0], "124.234.665");
        } 
    }

    private void DeleteEmail()
    {
        Response.Add("delete");
    }
}