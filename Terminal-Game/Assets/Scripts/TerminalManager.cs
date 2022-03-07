using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject directoryLine;
    public GameObject responseLine;
    [Space]

    [Header("Sound Effects")]
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    [Space]
    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect scrollRect;
    public GameObject msgList;

    private Interpreter interpreter;

    private void Awake()
    {
        interpreter = new Interpreter();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            audioSource.clip = audioClips[RandomAudioClip()];
            audioSource.Play();
        }
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
            AddInterpreterLines(interpreter.Interpret(userInput));

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

    private TerminalResponseBundle AddInterpreterLines(TerminalResponseBundle termnialResponseBundle)
    {
        for (int i = 0; i < termnialResponseBundle.response.Count; i++)
        {
            // Instantiate the response line
            GameObject response = Instantiate(responseLine, msgList.transform);

            // Set it to end of all messages
            response.transform.SetAsLastSibling();

            // Get size of messaage list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 20.0f);

            // Set text of response line to be the interpreter string
            response.GetComponentInChildren<Text>().text = termnialResponseBundle.response[i];
        }

       return termnialResponseBundle;
    }

    private int RandomAudioClip()
    {
        return Random.Range(0, audioClips.Length);
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
