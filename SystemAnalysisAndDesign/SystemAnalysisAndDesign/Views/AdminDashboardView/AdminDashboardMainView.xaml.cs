﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemAnalysisAndDesign.ViewModels.SignInandRegisterViewModel;
using SystemAnalysisAndDesign.Views.AdminCarRentView;
using SystemAnalysisAndDesign.Views.AdminCarRentApprovalView;
using SystemAnalysisAndDesign.Views.PaymentView;
using LiveCharts;
using LiveCharts.Wpf;

namespace SystemAnalysisAndDesign.Views.AdminDashboardView
{
    /// <summary>
    /// Interaction logic for AdminDashboardMainView.xaml
    /// </summary>
    public partial class AdminDashboardMainView : Window
    {
        public AdminDashboardMainView()
        {
            InitializeComponent();
            DataContext = this;
            
            // PointLabel = chartPoint =>
            //     string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            PointLabel = chartPoint => chartPoint.Y.ToString();
        }
        private void btn_CarRentList(object sender, RoutedEventArgs e)
        {
            AdminCarRentMainView adminCarRentMainView = new AdminCarRentMainView();
            adminCarRentMainView.Show();
            this.Close();
        }
        private void btn_CarRental(object sender, RoutedEventArgs e)
        {
            AdminCarRentApprovalMainView adminCarRentApprovalMainView = new AdminCarRentApprovalMainView();
            adminCarRentApprovalMainView.Show();
            this.Close();
        }
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
