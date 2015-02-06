using System;

namespace BearBones
{
	public class InfoPageViewModel
	{
		public int score;
		public string scout;
		public string drive;

		public InfoPageViewModel ()
		{

			
		}
	}
}
/*
 * 
 * Grab nuget Xlabs.Forms 2.0
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;

...code snippet

						Series firstBarSeries = new Series
						{
							Color = Color.Red,
							Type = ChartType.Bar
						};
						firstBarSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 25});
						firstBarSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 40});
						firstBarSeries.Points.Add(new DataPoint() { Label = "March", Value = 45});

						Series secondBarSeries = new Series
						{
							Color = Color.Blue,
							Type = ChartType.Bar
						};
						secondBarSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 30 });
						secondBarSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 35 });
						secondBarSeries.Points.Add(new DataPoint() { Label = "March", Value = 40 });

						Series lineSeries = new Series
						{
							Color = Color.Green,
							Type = ChartType.Line
						};
						lineSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 27.5 });
						lineSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 37.5 });
						lineSeries.Points.Add(new DataPoint() { Label = "March", Value = 42.5 });
						

						Chart chart = new Chart()
						{
							Color = Color.White,
							WidthRequest = MainPage.ScreenWidth-10,
							HeightRequest = 100,
							Spacing = 10
						};
						chart.Series.Add(firstBarSeries);
						chart.Series.Add(secondBarSeries);
						chart.Series.Add(lineSeries);
						StackLayout stack = new StackLayout ();
						
						Label lbl = new Label {
							Text = temp.PageName
						};

						stack.Children.Add(lbl);
						stack.Children.Add(chart);
						this.View = stack;
 * */
