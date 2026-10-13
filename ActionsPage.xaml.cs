using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using MoneyManager.DAL;
using Model;

namespace MoneyManager
{
    public partial class ActionsPage : Page
    {
        // Modern pastel color palette matching your dark theme
        private readonly Color[] PastelColors = new Color[]
        {
            Color.FromRgb(255, 179, 179), // Soft Rose
            Color.FromRgb(140, 226, 140), // Soft Green
            Color.FromRgb(124, 169, 255), // Soft Blue
            Color.FromRgb(255, 218, 121)  // Soft Yellow
        };

        public ActionsPage()
        {
            InitializeComponent();
            LoadTransactionsAndPopulateChart();
        }

        private void LoadTransactionsAndPopulateChart()
        {
            // TODO: replace userId=1 with currently logged-in user id when available
            var transactions = new MMDal().GetTransactions(1);
            var categoryTotals = transactions
                .GroupBy(t => string.IsNullOrEmpty(t.Category) ? "Uncategorized" : t.Category)
                .ToDictionary(group => group.Key, group => group.Sum(t => Math.Abs(t.Amount)));
            DrawPieChart(categoryTotals);
        }

        private void DrawPieChart(Dictionary<string, double> categoryTotals)
        {
            PieChartCanvas.Children.Clear();
            ChartLegendPanel.Children.Clear();

            double total = categoryTotals.Values.Sum();
            if (total <= 0) return;

            const double centerX = 60;
            const double centerY = 60;
            const double radius = 55;
            const double innerRadius = 35;
            double cumulativeAngle = 0;

            int index = 0;
            foreach (var item in categoryTotals)
            {
                double sliceAngle = (item.Value / total) * 360;
                Color color = PastelColors[index++ % PastelColors.Length];
                PieChartCanvas.Children.Add(new Path
                {
                    Data = CreateDonutSlice(centerX, centerY, radius, innerRadius, cumulativeAngle, cumulativeAngle + sliceAngle),
                    Fill = new SolidColorBrush(color),
                    Stroke = new SolidColorBrush(Color.FromRgb(30, 30, 47)),
                    StrokeThickness = 1.5
                });
                AddLegendItem(item.Key, color);
                cumulativeAngle += sliceAngle;
            }
        }

        private Geometry CreateDonutSlice(double cx, double cy, double outerRadius, double innerRadius, double startAngle, double endAngle)
        {
            Point outerStart = PointOnCircle(cx, cy, outerRadius, startAngle);
            Point outerEnd = PointOnCircle(cx, cy, outerRadius, endAngle);
            Point innerStart = PointOnCircle(cx, cy, innerRadius, endAngle);
            Point innerEnd = PointOnCircle(cx, cy, innerRadius, startAngle);
            bool isLargeArc = endAngle - startAngle > 180;

            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext context = geometry.Open())
            {
                context.BeginFigure(outerStart, true, true);
                context.ArcTo(outerEnd, new Size(outerRadius, outerRadius), 0, isLargeArc, SweepDirection.Clockwise, true, false);
                context.LineTo(innerStart, true, false);
                context.ArcTo(innerEnd, new Size(innerRadius, innerRadius), 0, isLargeArc, SweepDirection.Counterclockwise, true, false);
            }
            return geometry;
        }

        private static Point PointOnCircle(double cx, double cy, double radius, double angle)
        {
            double radians = (angle - 90) * Math.PI / 180;
            return new Point(cx + radius * Math.Cos(radians), cy + radius * Math.Sin(radians));
        }

        private void AddLegendItem(string title, Color color)
        {
            StackPanel row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 3, 0, 3) };
            Ellipse dot = new Ellipse
            {
                Width = 8,
                Height = 8,
                Fill = new SolidColorBrush(color),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 6, 0)
            };
            TextBlock text = new TextBlock
            {
                Text = title,
                Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 215)),
                FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center
            };

            row.Children.Add(dot);
            row.Children.Add(text);
            ChartLegendPanel.Children.Add(row);
        }
    }
}