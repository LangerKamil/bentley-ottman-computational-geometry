using System.Windows.Controls;
using GeometriaObliczeniowa.ViewModels;

namespace GeometriaObliczeniowa.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            var viewModel = (MainViewModel)this.DataContext;
            viewModel.DataGrid = this.dataGrid;
        }
    }
}
