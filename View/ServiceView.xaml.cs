using Sanator.ViewModel;
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

namespace Sanator.View
{
    /// <summary>
    /// Логика взаимодействия для ServiceView.xaml
    /// </summary>
    public partial class ServiceView : Window
    {
        private ServiceViewModel vm;
        public ServiceView(ServiceViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            this.vm = vm;
        }
        private void SaveService(object sender, RoutedEventArgs e)
        {
            vm.SaveService.Execute(null);
        }
      
    }
}
