using System.Diagnostics;
using System.Drawing;
using CommonTools;

namespace OK.EX1;
public static class Program
{
	public static void Main(string[] args)
	{
        try
        {
            var inputParser = new InputParser(args);
            int k = 0;
			List<Point> points = new();
            Stopwatch timer = new();

            if (inputParser.InputFilePath == null || inputParser.K == null)
            {
                Console.WriteLine("No arguments delivered. Using default values.");

                k = 4;

                points = new()
                {
                    new Point(2, 0),
                    new Point(0, 5),
                    new Point(1, 5),
                    new Point(3, 5),
                    new Point(4, 5)
                };
            }
            else
            {
                k = inputParser.K.Value;
				points = FileReader.GetPoints(inputParser.InputFilePath);
            }

            if (inputParser.TrackTime)
                timer.Start();
            
            var result = new Result(inputParser.Mode, points, k);

            if (inputParser.TrackTime)
            {
                timer.Stop();
                Console.WriteLine($"Time elapsed: {timer.ElapsedMilliseconds} ms");
            }

            Console.WriteLine($"{result.DistanceSum:F2}");
            Console.WriteLine(string.Join(", ", result.PointIndexes));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
	}
}
