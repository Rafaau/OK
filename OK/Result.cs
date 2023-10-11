using System.Drawing;
using CommonTools;

namespace OK.EX1;

internal class Result
{
    internal float DistanceSum { get; set; } = 0f;
	internal int[] PointIndexes { get; set; } = Array.Empty<int>();

    internal Result()
    {
    }

    internal Result(Mode mode, List<Point> points, int k)
    {
        switch (mode)
        {
            case Mode.Quality:
                var qualityResult = Quality.GetResult(points, k);
                DistanceSum = qualityResult.DistanceSum;
                PointIndexes = qualityResult.PointIndexes;
                break;
            case Mode.Performance:
                var performanceResult = Performance.GetResult(points, k);
                DistanceSum = performanceResult.DistanceSum;
                PointIndexes = performanceResult.PointIndexes;
                break;
            case Mode.Balanced:
                var balancedResult = Balanced.GetResult(points, k);
                DistanceSum = balancedResult.DistanceSum;
                PointIndexes = balancedResult.PointIndexes;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
}
