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
            get => (MainWindowViewModel)GetValue(MainWindowViewModelProperty);
            set => SetValue(MainWindowViewModelProperty, value);
        }

        public static readonly DependencyProperty MainWindowViewModelProperty =
            DependencyProperty.Register(nameof(MainWindowViewModel), typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(default));

        private bool IsRunning { get; set; }
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            this.MainWindowViewModel = new MainWindowViewModel();
            this.DataContext = this.MainWindowViewModel;
        }
        #endregion

        #region Methods
        private void ProceedCoordinateSystemRendering()
        {
            List<Segment> segments = this.MainWindowViewModel.Segments.ToList();
            this.coordinateSystem.RenderCoordinateSystem(segments);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) // TODO: Change it to react on Segment properties change only
        {
            if (MainWindowViewModel != null)
            {
                this.ProceedCoordinateSystemRendering();
            }
            base.OnPropertyChanged(e);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!this.IsRunning)
            {
                this.IsRunning = this.coordinateSystem.RunSweep(true);
            }
            else
            {
                this.IsRunning = this.coordinateSystem.RunSweep(false);
            }
        }
        #endregion


    }
}
