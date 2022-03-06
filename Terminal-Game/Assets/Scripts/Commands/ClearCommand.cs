using UnityEngine;

public class ClearCommand : Interpreter, ICommands
{
    public void Command()
    {
        GameObject commandLine = GameObject.Find("Command Line Container");

        foreach (Transform child in commandLine.transform)
        {
            if (!child.gameObject.CompareTag("InputLine"))
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
