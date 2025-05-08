using System;
using System.Drawing;

public class Star : Figure
{
    public Star(int x, int y, int size, Color color)
        : base(x, y, size, color) { }

    public override void Draw(Graphics g)
    {
        using (Brush brush = new SolidBrush(color))
        {
            PointF[] points = CalculateStarPoints(5, new PointF(x + size / 2, y + size / 2), size / 2, size / 4);
            g.FillPolygon(brush, points);
        }
    }

    private PointF[] CalculateStarPoints(int num_points, PointF center, float outer_radius, float inner_radius)
    {
        PointF[] points = new PointF[num_points * 2];
        double angle = -Math.PI / 2; // початковий кут вгору

        double step = Math.PI / num_points; // крок між вершинами

        for (int i = 0; i < num_points * 2; i++)
        {
            float radius = (i % 2 == 0) ? outer_radius : inner_radius;
            points[i] = new PointF(
                center.X + (float)(Math.Cos(angle) * radius),
                center.Y + (float)(Math.Sin(angle) * radius));
            angle += step;
        }
        return points;
    }
}
