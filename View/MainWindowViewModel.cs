using GeometriaObliczeniowa.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.View
{
    public sealed class MainWindowViewModel : BindableBase
    {
        #region Fields

        private ObservableCollection<Segment> segments;
        private string buttonText;
        private bool isRunning;

        #endregion

        #region Properties

        public ObservableCollection<Segment> Segments
        {
            get => this.segments;
            set => SetProperty(ref this.segments, value);
        }

        public string ButtonText { get; set; }
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            InitializeProperties();
        }
        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.Segments = new ObservableCollection<Segment>()
            {
                new Segment()
                {
                    StartingPoint = new Point(0,0),
                    EndingPoint = new Point(0,0)
                },
                new Segment()
                {
                    StartingPoint = new Point(0,0),
                    EndingPoint = new Point(0,0)
                },
            };
            this.isRunning = false;
            this.ButtonText = "Run";
        }
        #endregion
    }
}
