using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using Sanator.ViewModel;

namespace Sanator.View
{
    /// <summary>
    /// Interaction logic for MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
       
        public MainWindow1()
        {
            InitializeComponent();
            
        }

        private void OpenClientView(object sender, RoutedEventArgs e)
        {
            ClientView m = new ClientView(new ClientViewModel(new DbOperations(), new DefaultDialogService()));
            m.Show();
        }

        private void OpenReserveView(object sender, RoutedEventArgs e)
        {
            ReserveView d = new ReserveView();
            d.Show();
        }

        private void OpenUchetView(object sender, RoutedEventArgs e)
        {
            UchetView d = new UchetView();
            d.Show();
        }

        private void OpenServiceView(object sender, RoutedEventArgs e)
        {
            ServiceView d = new ServiceView(new ServiceViewModel(new DbOperations(), new DefaultDialogService())) ;
            d.Show();
        }

        private void OpenLogView(object sender, RoutedEventArgs e)
        {
            LogView d = new LogView();
            d.Show();
        }

        private void OpenPasswordView(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenPayView(object sender, RoutedEventArgs e)
        {
            PayView d = new PayView();
            d.Show();
        }

        private void OpenNumbersView(object sender, RoutedEventArgs e)
        {
            Number d = new Number();
            d.Show();
        }
        private void Ficha(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/Rin424525") { UseShellExecute = true });
        }

        private void Ficha2(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://memesmix.net/media/created/fgdagz.jpg") { UseShellExecute = true });
        }

        private void MyFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void OpenWorkerView(object sender, RoutedEventArgs e)
        {
            WorkerView d = new WorkerView(new WorkerViewModel(new DbOperations(), new DefaultDialogService()));
            d.Show();
        }
    }
}
