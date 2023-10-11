using System.Drawing;
using System.Reflection;

namespace OK.EX1;

internal static class Quality
{
    public static Result GetResult(List<Point> points, int k)
    {
        var result = new Result();

        var combinations = GetCombinations(points.Count, k);
        object lockObject = new object();

        Parallel.ForEach(combinations, combo =>
        {
            var distanceSum = CalculateDistanceSumForIndexes(points, combo);

            lock (lockObject)
            {
                if (distanceSum > result.DistanceSum)
                {
                    result.DistanceSum = distanceSum;
                    result.PointIndexes = combo.ToArray();
                }
            }
        });

        return result;
    }

    private static List<List<int>> GetCombinations(int n, int k)
    {
        var result = new List<List<int>>();
        GetCombinationsRecursive(new List<int>(), 0, n, k, result);
        return result;
    }

    private static void GetCombinationsRecursive(List<int> current, int start, int n, int k, List<List<int>> result)
    {
        if (current.Count == k)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = start; i < n; i++)
        {
            current.Add(i);
            GetCombinationsRecursive(current, i + 1, n, k, result);
            current.RemoveAt(current.Count - 1);
        }
    }

    private static float CalculateDistanceSumForIndexes(List<Point> points, List<int> indexes)
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

