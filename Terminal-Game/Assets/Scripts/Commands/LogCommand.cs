using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCommand : ICommands
{
    public string Name { get; set; } = "log";
    public string Example { get; set; } = "log entity";
    public string Description { get; set; } = "Add seen entity to your log";
    public object[] Arguments { get; set; }
    public TerminalResponseBundle Response { get; set; } = new TerminalResponseBundle();

    private List<Enum> entityLogList = new List<Enum>();

    public TerminalResponseBundle Execute()
    {
        Response.Clear();

        if (Arguments.Length > 0)
        {
            foreach (Entities entity in Enum.GetValues(typeof(Entities)))
            {
                if (Arguments[0].ToString() == entity.ToString())
                {
                    entityLogList.Add(entity);
                    Response.Entry("Added log: " + entity.ToString(), "green");

                    return Response;
                }
            }
            Response.Entry("Entity invalid", "red");
        }
    

        return Response;
    }
}

public enum Entities
{
    Walker,
    Crawler,
    Speeder
}