using System;
using System.Collections.Generic; 

public static class PositionUtils
{
    /// <summary>
    /// Находится ли точка в сфере
    /// </summary>
    /// <param name="tower"></param>
    /// <param name="radius"></param>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public static bool IsPointInsideSphere((float x, float y, float z) source, float radius, (float x, float y, float z) point)
    {
        var distanceSquared = Math.Pow(point.x - source.x, 2) + Math.Pow(point.y - source.y, 2) + Math.Pow(point.z - source.z, 2);
        return distanceSquared <= Math.Pow(radius, 2);
    }

    /// <summary>
    /// Получение ближайшей точки 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="points"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static (float x, float y, float z) GetClosestPoint((float x, float y, float z) target, List<(float x, float y, float z)> points)
    {
        if (points == null || points.Count == 0)
        {
            throw new ArgumentException("The list of points is either null or empty.");
        }

        var closestPoint = points[0];
        var minDistance = DistanceSquared(points[0], target);

        foreach (var point in points)
        {
            var distance = DistanceSquared(point, target);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    private static double DistanceSquared((float x, float y, float z) point1, (float x, float y, float z) point2)
    {
        double dx = point1.x - point2.x;
        double dy = point1.y - point2.y;
        double dz = point1.z - point2.z;

        return dx * dx + dy * dy + dz * dz;
    }
}