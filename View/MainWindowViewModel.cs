using GeometriaObliczeniowa.Models;
using System.Collections.ObjectModel;

namespace GeometriaObliczeniowa.View
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<SegmentsViewModel> segments;
        #endregion

        #region Properties
        public ObservableCollection<SegmentsViewModel> Segments
        {
            get { return this.segments; }
            set { this.segments = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            InitializeProperties();
        }
        #endregion

        #region Methods
        public void InitializeProperties()
        {
            this.Segments = new ObservableCollection<SegmentsViewModel>()
            {
                new SegmentsViewModel()
                {
                    X1 = 20,
                    Y1 = -100,
                    X2 = 76,
                    Y2 = 13,
                },
                new SegmentsViewModel()
                {
                    X1 = 57,
                    Y1 = 78,
                    X2 = -13,
                    Y2 = -47,
                },
            };
        }
        #endregion
    }
}
