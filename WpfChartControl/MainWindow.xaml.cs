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

using System.Windows.Threading;

namespace WpfChartControl
{
    public class MyObservable
    {
        public System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string, int>> Points1 { get; set; }
        public System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string, int>> Points2 { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<KeyValuePair<string, int>> _values1 = new List<KeyValuePair<string, int>>();
        private List<KeyValuePair<string, int>> _values2 = new List<KeyValuePair<string, int>>();
        private MyObservable _observable = new MyObservable();
        private int _counter = 0;

        public MainWindow()
        {
            _values1.Add(new KeyValuePair<string, int>("Administration", 20));
            _values1.Add(new KeyValuePair<string, int>("Management", 36));
            _values1.Add(new KeyValuePair<string, int>("Development", 89));
            _values1.Add(new KeyValuePair<string, int>("Support", 270));
            _values1.Add(new KeyValuePair<string, int>("Sales", 140));

            _values2.Add(new KeyValuePair<string, int>("Administration", 20));
            _values2.Add(new KeyValuePair<string, int>("Management", 36));
            _values2.Add(new KeyValuePair<string, int>("Development", 89));
            _values2.Add(new KeyValuePair<string, int>("Support", 230));
            _values2.Add(new KeyValuePair<string, int>("Sales", 140));

            _observable = new MyObservable()
            {
                Points1 = new System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string, int>>(_values1),
                Points2 = new System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string, int>>(_values2)
            };

            InitializeComponent();
            showChart();
        }

        private void showChart()
        {
            ColumnChart1.DataContext = _observable;
            AreaChart1.DataContext = _observable;
            LineChart1.DataContext = _observable;
            PieChart1.DataContext = _observable;
            BarChart1.DataContext = _observable;
            BubbleSeries1.DataContext = _observable;
            ScatterSeries1.DataContext = _observable;

            Action addPointsAction = null;
            addPointsAction = () =>
            {
                System.Threading.Thread.Sleep(1000);
                Dispatcher.Invoke(() => addPoints());
                Task.Run(addPointsAction);
            };
            Task.Run(addPointsAction);
        }

        private void addPoints()
        {
            var random = new Random();
            _observable.Points1.Add(new KeyValuePair<string, int>(_counter.ToString(), random.Next(0, 200)));
            _observable.Points1.RemoveAt(0);
            _observable.Points2.Add(new KeyValuePair<string, int>(_counter.ToString(), random.Next(0, 200)));
            _observable.Points2.RemoveAt(0);
            _counter++;
        }
    }
}
