﻿using Lab.Classes; 
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;

namespace Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       private void DefaultValues(object sender, RoutedEventArgs e)
        {
            SplitNumber.Text = 1000.ToString();
            TopLimit.Text = 10000.ToString();
            BottomLimit.Text = 1.ToString(); 
        } 
        private void BuildGraphic(object sender, RoutedEventArgs e)
        {
            var pm = new PlotModel()
            {
                Title = " 2x - ln(11x) - 1 "
            };
            var lineSeries = new LineSeries();

            int upLim = Convert.ToInt32(TopLimit.Text);
            int lowLim = Convert.ToInt32(BottomLimit.Text);
            ICalculator calcultGraph = new SimsonMethod();

            for (int i = 0; i < Convert.ToInt32(SplitNumber.Text); i++)
            {
                double time = 0, result;
                if (switchParallel.IsChecked == true)
                {
                    result = calcultGraph.CalculateParallel(i, upLim, lowLim, x => (2 * x) - Math.Log(11 * x) - 1, out time);
                }
                else
                {
                    result = calcultGraph.Calculate(i, upLim, lowLim, x => (2 * x) - Math.Log(11 * x) - 1, out time);
                }
                lineSeries.Points.Add(new DataPoint(i, time));
            }

            pm.Series.Add(lineSeries);
            Graph.Model = pm;
        }
        private ICalculator GetMethod()
        {
            return methods.SelectedIndex switch
            {
                0 => new SimsonMethod(),
                1 => new TrapezoidalMethod(),
                _ => throw new NotImplementedException(),
            };
        }
        private void StartCalculate(object sender, RoutedEventArgs e)
        {
            stCalculate();
        }
        private void stCalculate()
        {
            int splits = Convert.ToInt32(SplitNumber.Text); 
            int upLim = Convert.ToInt32(TopLimit.Text);
            int lowLim = Convert.ToInt32(BottomLimit.Text);
            double time = 0, result;
            ICalculator calcult = GetMethod();
            if (switchParallel.IsChecked == true)
            {
                 result = calcult.CalculateParallel(splits, upLim, lowLim, x => (2 * x) - Math.Log(11 * x) - 1, out time);
            }
            else
            {
                result = calcult.Calculate(splits, upLim, lowLim, x => (2 * x) - Math.Log(11 * x) - 1, out time);
            }
            timePar.Text = Convert.ToString(time);
            Result.Text = result.ToString(); 
        }
    } 
}
