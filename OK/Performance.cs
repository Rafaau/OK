using System.Drawing;

namespace OK.EX1;

internal static class Performance
{
    public static Result GetResult(List<Point> points, int k)
    {
        var pointDistances = points.Select((p, index) =>
                new
                {
                    Index = index,
                    Distance = points.Where((p2, index2) => index2 != index).Sum(p2 => Math.GetDistance(p, p2))
                })
            .OrderByDescending(pd => pd.Distance)
            .ToList();

        var bestIndexes = pointDistances.Take(k).Select(pd => pd.Index).ToList();
        float bestDistanceSum = CalculateDistanceSumForIndexes(bestIndexes, points);

        for (int i = k; i < pointDistances.Count; i++)
        {
            for (int j = 0; j < k; j++)
            {
                var newIndexes = new List<int>(bestIndexes);
                newIndexes[j] = pointDistances[i].Index;

                float newDistanceSum = CalculateDistanceSumForIndexes(newIndexes, points);
                if (newDistanceSum > bestDistanceSum)
                {
                    bestIndexes = newIndexes;
                    bestDistanceSum = newDistanceSum;
                }
            }
        }

        bestIndexes.Sort();

        return new Result
        {
            DistanceSum = bestDistanceSum,
            PointIndexes = bestIndexes.ToArray()
        };
    }

    private static float CalculateDistanceSumForIndexes(List<int> indexes, List<Point> points)
    {
        float sum = 0;

        for (int i = 0; i < indexes.Count; i++)
        {
            for (int j = i + 1; j < indexes.Count; j++)
            {
                sum += Math.GetDistance(points[indexes[i]], points[indexes[j]]);
            }
        }

        return sum;
    }
}

