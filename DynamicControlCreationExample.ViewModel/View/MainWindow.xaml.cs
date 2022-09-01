using System.Windows;
using DynamicControlCreationExample.WPFMVVM.ViewModel;

namespace DynamicControlCreationExample.WPFMVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
