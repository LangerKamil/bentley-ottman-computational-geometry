using System.Windows;
using System.Windows.Input;

namespace GeometriaObliczeniowa.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += DisableTabNavigation;
        }

        private void DisableTabNavigation(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }
    }
}
