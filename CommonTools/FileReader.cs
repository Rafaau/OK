using System.Drawing;

namespace CommonTools;

public static class FileReader
{
    public static List<Point> GetPoints(string filePath)
    {
        var points = new List<Point>();

        using (var reader = new StreamReader(filePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var split = line.Split(',');
                points.Add(new Point(int.Parse(split[0]), int.Parse(split[1])));
            }
        }

        return points;
    }
}

