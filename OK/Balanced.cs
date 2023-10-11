using System.Drawing;

namespace OK.EX1;

internal static class Balanced
{
    public static Result GetResult(List<Point> allPoints, int k)
    {
        List<int> bestCombinationIndexes = null;
        float bestDistanceSum = -1;

        for (int startIdx = 0; startIdx < allPoints.Count; startIdx++)
        {
            var currentCombinationIndexes = new List<int> { startIdx };

            while (currentCombinationIndexes.Count < k)
            {
                float maxDistance = -1;
                int farthestPointIndex = -1;

                for (int candidateIdx = 0; candidateIdx < allPoints.Count; candidateIdx++)
                {
                    if (!currentCombinationIndexes.Contains(candidateIdx))
                    {
                        float currentDistanceSum = currentCombinationIndexes.Sum(pointIdx => Math.GetDistance(allPoints[pointIdx], allPoints[candidateIdx]));
                        if (currentDistanceSum > maxDistance)
                        {
                            maxDistance = currentDistanceSum;
                            farthestPointIndex = candidateIdx;
                        }
                    }
                }
                currentCombinationIndexes.Add(farthestPointIndex);
            }

            float totalDistance = CalculateTotalDistance(currentCombinationIndexes, allPoints);
            if (totalDistance > bestDistanceSum)
            {
                bestDistanceSum = totalDistance;
                bestCombinationIndexes = currentCombinationIndexes;
            }
        }

        bestCombinationIndexes.Sort();

        return new Result
        {
            DistanceSum = bestDistanceSum,
            PointIndexes = bestCombinationIndexes.ToArray()
        };
    }

    private static float CalculateTotalDistance(List<int> indexes, List<Point> points)
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

