using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject directoryLine;
    public GameObject responseLine;
    [Space]

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect scrollRect;
    public GameObject msgList;

    private Interpreter interpreter;

    private void Awake()
    {
        interpreter = new Interpreter();
    }

    private void OnGUI()
    {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            // Store user input
            string userInput = terminalInput.text;

            // Clear input field
            ClearInputField();

            // Instantiate a gameobject with directory prefix
            AddDirectoryLine(userInput);

            // Add the interpretation lines
            //int lines = AddInterpreterLines(interpreter.Interpret(userInput));
            AddInterpreterLines2(interpreter.Interpret2(userInput));

            // Scroll to the bottom of scroll rect
            ScrollToBottom();

            // Move user input line
            userInputLine.transform.SetAsLastSibling();

            // Refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    private void AddDirectoryLine(string userInput)
    {
        // Resize command line container
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 20.0f);

        // Instantiate directory line
        GameObject msg = Instantiate(directoryLine, msgList.transform);

        // Set child index
        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        // Set the text of this new gameobject
        msg.GetComponentsInChildren<Text>()[1].text = userInput;
    }

    private int AddInterpreterLines(List<string> interpretation)
    {
        for(int i = 0; i < interpretation.Count; i++)
        {
            // Instantiate the response line
            GameObject response = Instantiate(responseLine, msgList.transform);

            // Set it to end of all messages
            response.transform.SetAsLastSibling();

            // Get size of messaage list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 20.0f);

            // Set text of response line to be the interpreter string
            response.GetComponentInChildren<Text>().text = interpretation[i];
        }

        return interpretation.Count;
    }

    private TerminalResponseBundle AddInterpreterLines2(TerminalResponseBundle interpretation)
    {
        for (int i = 0; i < interpretation.responses.Count; i++)
        {
            // Instantiate the response line
            GameObject response = Instantiate(responseLine, msgList.transform);

            // Set it to end of all messages
            response.transform.SetAsLastSibling();

            // Get size of messaage list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 20.0f);

            // Set text of response line to be the interpreter string
            response.GetComponentInChildren<Text>().text = interpretation.responses[i];
        }

       return interpretation;
    }

    private void ScrollToBottom()
    {
        scrollRect.verticalNormalizedPosition = 0;
    }

    private void ClearInputField()
    {
        terminalInput.text = string.Empty;
    }
}
