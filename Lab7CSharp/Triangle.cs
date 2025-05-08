using System;
using System.Drawing;

public class Triangle : Figure
{
    public Triangle(int x, int y, int size, Color color)
        : base(x, y, size, color) { }

    public override void Draw(Graphics g)
    {
        using (Brush brush = new SolidBrush(color))
        {
            Point[] points = new Point[3];
            points[0] = new Point(x + size / 2, y); // Верхня вершина
            points[1] = new Point(x + size, y + size);
            points[2] = new Point(x, y + size);

            g.FillPolygon(brush, points);
        }
    }
}
