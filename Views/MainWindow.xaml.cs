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
            this.PreviewKeyDown += DisableFloatingPointNumbers;
        }

        private void DisableTabNavigation(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }

        private void DisableFloatingPointNumbers(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal || e.Key == Key.OemComma || e.Key == Key.OemPeriod)
            {
                e.Handled = true;
            }
        }
    }
}
