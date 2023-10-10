using System.Drawing;

namespace OK.EX1;

internal class Result
{
    internal float DistanceSum { get; set; } = 0f;
	internal int[] PointIndexes { get; set; } = Array.Empty<int>();

    internal Result(List<Point> points, int k)
    {
        var pointDistances = points.Select((p, index) =>
            new
            {
                Index = index,
                Distance = points.Where((p2, index2) => index2 != index).Sum(p2 => GetDistance(p, p2))
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

                Console.WriteLine($"\nIndex {j}:");
                foreach (var index in newIndexes)
                {
                    Console.WriteLine(index);
                }

                float newDistanceSum = CalculateDistanceSumForIndexes(newIndexes, points);
                if (newDistanceSum > bestDistanceSum)
                {
                    bestIndexes = newIndexes;
                    bestDistanceSum = newDistanceSum;
                }
            }
        }

        bestIndexes.Sort();


        DistanceSum = bestDistanceSum;
        PointIndexes = bestIndexes.ToArray();
    }

    private float CalculateDistanceSumForIndexes(List<int> indexes, List<Point> points)
    {
        float sum = 0;

        for (int i = 0; i < indexes.Count; i++)
        {
            for (int j = i + 1; j < indexes.Count; j++)
            {
                sum += GetDistance(points[indexes[i]], points[indexes[j]]);
            }
        }

        return sum;
    }

    private float GetDistance(Point firstPoint, Point secondPoint)
    {
        return MathF.Sqrt(MathF.Pow(firstPoint.X - secondPoint.X, 2) + MathF.Pow(firstPoint.Y - secondPoint.Y, 2));
    }
}
