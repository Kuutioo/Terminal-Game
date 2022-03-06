public class EchoCommand : Interpreter, ICommands
{
    public void Command()
    {
        string text = string.Empty;

        for (int i = 1; i < args.Length; i++)
        {
            text += args[i] + " ";
        }

        response.Add(text);
    }
}
