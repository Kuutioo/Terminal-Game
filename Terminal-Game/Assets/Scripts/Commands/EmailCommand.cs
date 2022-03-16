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

    private List<string> emailFrom = new List<string>();
    private List<string> emailSubject = new List<string>();
    private List<string> emailText = new List<string>();

    private bool showNewEmail = true;

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
        if (showNewEmail)
            GenerateEmails(UnityEngine.Random.Range(2, 5));
        else
        {
            for (int i = 0; i < emailFrom.Count; i++)
            {
                string from = emailFrom[i];
                string subject = emailSubject[i];

                Response.EmailInterfaceEntry(from, subject);
            }
        }
    }

    private void OpenEmail()
    {
        Response.AddSpacing(1);
        for(int i = 0; i < emailFrom.Count; i++)
        {
            if (Arguments[1].ToString() == i.ToString())
            {
                Response.EmailEntry(emailFrom[i], "mark@gorillamail.com", emailSubject[i], emailText[i]);
            }
        }
        Response.AddSpacing(1);
    }

    private string GenerateIP()
    {
        System.Random _random = new System.Random(Guid.NewGuid().GetHashCode());
        return string.Format("{0}.{1}.{2}.{3}", _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
    }

    private void GenerateEmails(int emailNumber)
    {
        List<string> subjects = new List<string>()
        {
            {"Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"}
            /*{"Hack this ip and get me their address"},
            {"Hack this ip and get me their Drivers license ID"}*/
        };

        for (int i = 0; i < emailNumber; i++)
        {
            string from = string.Empty;

            // Generate random IP
            emailText.Add(GenerateIP());

            // Random subject from the list
            string subject = subjects[UnityEngine.Random.Range(0, subjects.Count)];

            // Generate email address
            for (int j = 0; j < UnityEngine.Random.Range(4, 6); j++)
            {
                from += Response.GetRandomCharAZ().ToString();
            }
            from += "@gorillamail.com";

            Response.EmailInterfaceEntry(from, subject);
            emailFrom.Add(from);
            emailSubject.Add(subject);
        }

        showNewEmail = false;
    }
}