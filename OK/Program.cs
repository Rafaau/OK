namespace OK.EX1;
public class Program
{
	public static void Main(string[] args)
	{
		int k = 4;

		List<Point> points = new()
		{
			new Point(2, 0),
			new Point(0, 5),
			new Point(1, 5),
			new Point(3, 5),
			new Point(4, 5)
		};

		var result = CalculateMaxDistance(points, k);

		Console.WriteLine($"{result.DistanceSum:F2}");
		Console.WriteLine(string.Join(", ", result.PointIndexes));
	}

	private static Result CalculateMaxDistance(List<Point> points, int k)
	{
		var result = new Result { DistanceSum = 0 };

		var combinations = GetCombinations(points.Count, k);
		foreach (var combo in combinations)
		{
			float distanceSum = 0;

			for (int i = 0; i < combo.Count; i++)
			{
				for (int j = i + 1; j < combo.Count; j++)
				{
					distanceSum += GetDistance(points[combo[i]], points[combo[j]]);
				}
			}

			if (distanceSum > result.DistanceSum)
			{
				result.DistanceSum = distanceSum;
				result.PointIndexes = combo.ToArray();
			}
		}

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

	private static float GetDistance(Point firstPoint, Point secondPoint)
	{
		return MathF.Sqrt(MathF.Pow(firstPoint.X - secondPoint.X, 2) + MathF.Pow(firstPoint.Y - secondPoint.Y, 2));
	}
}

public class Point
{
	public int X { get; set; }
	public int Y { get; set; }

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

	public override string ToString() => $"({X}, {Y})";
}
