using ScottPlot;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using OpenTK.Platform.Windows;

namespace EPIHelper.Views
{
    /// <summary>
    /// PLTrend.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PLTrendView : UserControl
    {
        List<ScottPlot.Plottables.Scatter> MyScatters = new List<ScottPlot.Plottables.Scatter>();
        ScottPlot.Plottables.Crosshair MyCrosshair;
        ScottPlot.Plottables.Marker MyHighlightMarker;
        ScottPlot.Plottables.Text MyHighlightText;
        readonly ScottPlot.IYAxis YAxis1;
        readonly ScottPlot.IYAxis YAxis2;
        readonly ScottPlot.IYAxis YAxis3;
        public PLTrendView()
        {
            InitializeComponent();
            YAxis1 = WpfPlot1.Plot.Axes.Left;
            YAxis2 = WpfPlot1.Plot.Axes.AddLeftAxis();
            YAxis3 = WpfPlot1.Plot.Axes.AddLeftAxis();

            // plot random data to start
            PlotRandomData();


            // create 3 scatter plots with random points
            for (int i = 0; i < 3; i++)
            {
                double[] xs = Generate.RandomSample(30);
                double[] ys = Generate.RandomSample(30);
                ScottPlot.Plottables.Scatter scatter = WpfPlot1.Plot.Add.ScatterPoints(xs, ys);
                scatter.LegendText = $"Scatter {i}";
                scatter.MarkerStyle.Size = 10;
                MyScatters.Add(scatter);
            }
            WpfPlot1.Plot.ShowLegend();

            // Create a marker to highlight the point under the cursor
            MyCrosshair = WpfPlot1.Plot.Add.Crosshair(0, 0);
            MyHighlightMarker = WpfPlot1.Plot.Add.Marker(0, 0);
            MyHighlightMarker.Shape = MarkerShape.OpenCircle;
            MyHighlightMarker.Size = 17;
            MyHighlightMarker.LineWidth = 2;

            // Create a text label to place near the highlighted value
            MyHighlightText = WpfPlot1.Plot.Add.Text("", 0, 0);
            MyHighlightText.LabelAlignment = Alignment.LowerLeft;
            MyHighlightText.LabelBold = true;
            MyHighlightText.OffsetX = 7;
            MyHighlightText.OffsetY = -7;
            WpfPlot1.Refresh();


        }
        private void PlotRandomData()
        {
            WpfPlot1.Plot.Clear();

            double[] data1 = ScottPlot.RandomDataGenerator.Generate.RandomWalk(count: 51, mult: 1);
            var sig1 = WpfPlot1.Plot.Add.Signal(data1);
            sig1.Axes.YAxis = YAxis1;
            YAxis1.Label.Text = "YAxis1";
            YAxis1.Label.ForeColor = sig1.Color;

            double[] data2 = ScottPlot.RandomDataGenerator.Generate.RandomWalk(count: 51, mult: 1000);
            var sig2 = WpfPlot1.Plot.Add.Signal(data2);
            sig2.Axes.YAxis = YAxis2;
            YAxis2.Label.Text = "YAxis2";
            YAxis2.Label.ForeColor = sig2.Color;

            double[] data3 = ScottPlot.RandomDataGenerator.Generate.RandomWalk(count: 51, mult: 1000);
            var sig3 = WpfPlot1.Plot.Add.Signal(data3);
            sig3.Axes.YAxis = YAxis3;
            YAxis3.Label.Text = "YAxis3";
            YAxis3.Label.ForeColor = sig3.Color;

            WpfPlot1.Plot.Axes.AutoScale();
            WpfPlot1.Plot.Axes.Zoom(.8, .7); // zoom out slightly
            WpfPlot1.Refresh();
        }

        private void WpfPlot1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            // determine where the mouse is
            Pixel mousePixel = new Pixel(e.Location.X, e.Location.Y);
            Coordinates mouseLocation = WpfPlot1.Plot.GetCoordinates(mousePixel);

            // get the nearest point of each scatter
            Dictionary<int, DataPoint> nearestPoints = new Dictionary<int, DataPoint>();
            for (int i = 0; i < MyScatters.Count; i++)
            {
                DataPoint nearestPoint = MyScatters[i].Data.GetNearest(mouseLocation, WpfPlot1.Plot.LastRender);
                nearestPoints.Add(i, nearestPoint);
            }

            // determine which scatter's nearest point is nearest to the mouse
            bool pointSelected = false;
            int scatterIndex = -1;
            double smallestDistance = double.MaxValue;
            for (int i = 0; i < nearestPoints.Count; i++)
            {
                if (nearestPoints[i].IsReal)
                {
                    // calculate the distance of the point to the mouse
                    double distance = nearestPoints[i].Coordinates.Distance(mouseLocation);
                    if (distance < smallestDistance)
                    {
                        // store the index
                        scatterIndex = i;
                        pointSelected = true;
                        // update the smallest distance
                        smallestDistance = distance;
                    }
                }
            }

            // place the crosshair, marker and text over the selected point
            if (pointSelected)
            {
                ScottPlot.Plottables.Scatter scatter = MyScatters[scatterIndex];
                DataPoint point = nearestPoints[scatterIndex];

                MyCrosshair.IsVisible = true;
                MyCrosshair.Position = point.Coordinates;
                MyCrosshair.LineColor = scatter.MarkerStyle.FillColor;

                MyHighlightMarker.IsVisible = true;
                MyHighlightMarker.Location = point.Coordinates;
                MyHighlightMarker.MarkerStyle.LineColor = scatter.MarkerStyle.FillColor;

                MyHighlightText.IsVisible = true;
                MyHighlightText.Location = point.Coordinates;
                MyHighlightText.LabelText = $"{point.X:0.##}, {point.Y:0.##}";
                MyHighlightText.LabelFontColor = scatter.MarkerStyle.FillColor;

                WpfPlot1.Refresh();
                //base.Text = $"Selected Scatter={scatter.LegendText}, Index={point.Index}, X={point.X:0.##}, Y={point.Y:0.##}";
            }

            // hide the crosshair, marker and text when no point is selected
            if (!pointSelected && MyCrosshair.IsVisible)
            {
                MyCrosshair.IsVisible = false;
                MyHighlightMarker.IsVisible = false;
                MyHighlightText.IsVisible = false;
                WpfPlot1.Refresh();
                //base.Text = $"No point selected";
            }
        }
    }
}
