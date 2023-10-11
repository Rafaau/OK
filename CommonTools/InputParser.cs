namespace CommonTools;

public class InputParser
{
    public string? InputFilePath { get; private set; }
    public int? K { get; private set; }
    public Mode Mode { get; private set; } = Mode.Quality;

    public InputParser(string[] args)
    {
        if (args.Length < 4)
        {
            InputFilePath = null;
            K = null;
        }
        else
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-f")
                {
                    InputFilePath = args[i + 1];
                }

                if (args[i] == "-k")
                {
                    K = int.Parse(args[i + 1]);
                }

                if (args[i] == "-p")
                {
                    Mode = Mode.Performance;
                }
            }
        }
    }
}
