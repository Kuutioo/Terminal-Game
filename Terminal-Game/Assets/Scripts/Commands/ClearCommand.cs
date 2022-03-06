using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCommand : Interpreter, ICommands
{
    [SerializeField]
    private GameObject commandLineContainer;

    public void Command()
    {
        foreach (Transform child in commandLineContainer.transform)
        {
            if (child.gameObject.CompareTag("InputLine"))
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }
}
