using LiveCharts;

namespace SystemAnalysisAndDesign.ViewModels.AdminDashboardViewModel;

public class AdminDashboardViewModel
{
    public Func<ChartPoint, string> PointLabel { get; set; }
    public AdminDashboardViewModel()
    {
        PointLabel = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
    }
}