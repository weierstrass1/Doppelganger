public delegate void commandHandler();

public class command
{
    public bool Enable;

    public commandHandler Action;
    public charComponent charC;
    public BaseCharacter baseChar;

    public void Execute()
    {
        if (Enable && Action != null && charC != null && baseChar != null)
            Action();
    }
}
