using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject directoryLine;
    [SerializeField] private GameObject responseLine;

    [Space(20)]
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    [Header("Other")]
    [SerializeField] private InputField terminalInput;
    [SerializeField] private GameObject userInputLine;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject msgList;

    private Interpreter interpreter;

    public int CommandIndex { get; set; }

    private void Awake()
    {
        interpreter = GetComponent<Interpreter>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            audioSource.clip = audioClips[RandomAudioClip()];
            audioSource.Play();
        }

        OldInputs();
    }

    private void OnGUI()
    {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            TerminalHandler();
        }
    }

    private void TerminalHandler()
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

    private void OldInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            OldInputsScrollUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            OldInputsScrollDown();
    }

    private void OldInputsScrollDown()
    {
        try
        {
            if (!interpreter.GetOldInputs().Any())
            {
                return;
            }
            CommandIndex += 1;

            string s = interpreter.GetOldInputs()[CommandIndex];
            terminalInput.text = s;
        }
        catch
        {
            terminalInput.text = interpreter.GetOldInputs().Last();
            CommandIndex = interpreter.GetOldInputs().Count - 1;
        }

        terminalInput.caretPosition = terminalInput.text.Length;
    }

    private void OldInputsScrollUp()
    {
        try
        {
            if (!interpreter.GetOldInputs().Any())
            {
                return;
            }
            CommandIndex -= 1;

            string s = interpreter.GetOldInputs()[CommandIndex];
            terminalInput.text = s;
        }
        catch
        {
            terminalInput.text = interpreter.GetOldInputs().First();
            CommandIndex = interpreter.GetOldInputs().First().Length;
        }

        terminalInput.caretPosition = terminalInput.text.Length;
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
