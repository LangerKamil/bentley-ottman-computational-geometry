using GeometriaObliczeniowa.Models;
using GeometriaObliczeniowa.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GeometriaObliczeniowa
{
    public partial class MainWindow : Window
    {
        #region Properties
        public MainWindowViewModel MainWindowViewModel
        {
            get { return (MainWindowViewModel)GetValue(MainWindowViewModelProperty); }
            set { SetValue(MainWindowViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainWindowViewModelProperty =
            DependencyProperty.Register(nameof(MainWindowViewModel), typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(default));
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            this.MainWindowViewModel = new MainWindowViewModel();
            this.DataContext = this.MainWindowViewModel;
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(MainWindowViewModel))
            {
                this.Draw();
            }

            base.OnPropertyChanged(e);
        }

        #endregion

        #region Events
        #endregion

        #region Methods
        private void Draw()
        {
            List<SegmentsViewModel> segments = this.MainWindowViewModel.Segments.ToList();
            segments.ForEach(c => this.coordinateSystem.Draw(c));
        }
        #endregion
    }
}
