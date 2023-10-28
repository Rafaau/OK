namespace CommonTools;

public class InputParser
{
    public string? InputFilePath { get; private set; } = null;
    public int? K { get; private set; } = null;
    public Mode Mode { get; private set; } = Mode.Balanced;
    public bool TrackTime { get; private set; } = false;

    public InputParser(string[] args)
    {
        if (args.Length >= 4)
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

                if (args[i] == "-b")
                {
                    Mode = Mode.Balanced;
                }

                if (args[i] == "-t")
                {
                    TrackTime = true;
                }
            }
        }
    }
}
