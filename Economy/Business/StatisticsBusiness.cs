using Economy.Models.JsonModels;
using Economy.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Economy.Business
{
    public class StatisticsBusiness
    {
        
        public static string GetChart(FilterJson filterJson)
        {            
            var chartData = StatisticsBusiness.GetChartData(filterJson);
            var items = chartData.GroupBy(d => d.Year).ToList();

            //var chart = new Chart(width: 800, height: 250, theme: themeChart);
            var chart = new Chart(width: 1200, height: 375, theme: GetThemeChart());

            foreach (var item in chartData)
            {
                chart = chart.AddSeries(
                    chartType: "column",
                    xValue: item.Months.Select(m => m.Name).ToList(), //new[] { String.Join(",", item.Months)},
                    yValues: item.Months.Select(m => m.Sum).ToList(), //new[] { 18000, 14000, 22500 },
                    name: item.Year.ToString());
            }

            chart = chart.AddLegend();

            var bytes = chart.GetBytes("png");
            //return File(bytes, "image/png");

            string imageBase64 = Convert.ToBase64String(bytes);
            string imageSrc = string.Format("data:image/png;base64,{0}", imageBase64);
            return imageSrc;
            //var model = StatisticsBusiness.GetChartData(filterJson);
            //return Json(model);
        }
        public static IEnumerable<int> GetYears()
        {
            var years = new List<int>();
            int year = 2006;
            while (year <= DateTime.Now.Year)
            {
                years.Add(year);
                year++;
            }
            return years;
        }
        private static IEnumerable<StatisticItem> GetChartData(FilterJson filter)
        {
            var Bill = EconomyBusiness.GetBills(filter);
            var result = Bill.GroupBy(x => new { x.DueDate.Year, x.DueDate.Month })
                              .Select(g => new {
                                  Year = g.Key.Year,
                                  Month = g.Key.Month,
                                  Sum = g.Sum(x => Math.Round(x.Amount, 0)),
                              })
                              .ToList();
            var years = result.GroupBy(x => x.Year).OrderBy(x => x.Key).ToList();
            var statistics = new List<StatisticItem>();
            foreach (var y in years)
            {
                var months = new List<Month>();
                // Months
                for (int monthNo = 1; monthNo < 13; monthNo++)
                {
                    var sum = (decimal)0;
                    var monthExist = result.Where(d => d.Year.Equals(y.Key) && d.Month.Equals(monthNo));
                    if (monthExist != null && monthExist.Any())
                        sum = monthExist.FirstOrDefault().Sum;
                    var month = new Month
                    {
                        Number = monthNo,
                        Name = GetMonthName(monthNo),
                        Sum = sum
                    };
                    months.Add(month);
                }
                //var months = result.Where(d => d.Year.Equals(y.Key))
                //                    .Select(x => new Month{  Number = x.Month, Name = GetMonthName(x.Month), Sum = x.Sum })
                //                    .OrderBy(x => x.Number)
                //                    .ToList();

                statistics.Add(new StatisticItem { Year = y.Key, Months = months });
            }
            return statistics;
        }

        private static string GetThemeChart()
        {
            string themeChart = @"<Chart>
                      <ChartAreas>
                        <ChartArea Name=""Default"" _Template_=""All"">
                          <AxisY Interval=""2000"">
                            <LabelStyle Font=""Verdana, 12px"" />                                      
                          </AxisY>
                          <AxisX LineColor=""64, 64, 64, 64"" Interval=""1"">
                            <LabelStyle Font=""Verdana, 12px"" />
                          </AxisX>
                        </ChartArea>
                      </ChartAreas>
                    </Chart>";
            return themeChart;
        }

        private static string GetMonthName(int month)
        {
            switch(month)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "Maj";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Okt";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return "";
                
            }
        }
    }
}