using System.Drawing;

namespace OK.EX1;

internal static class Math
{
    internal static float GetDistance(Point firstPoint, Point secondPoint)
    {
        return MathF.Sqrt(MathF.Pow(firstPoint.X - secondPoint.X, 2) + MathF.Pow(firstPoint.Y - secondPoint.Y, 2));
    }
}

