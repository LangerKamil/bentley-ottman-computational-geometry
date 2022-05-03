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
        #endregion

        #region Properties
        public ObservableCollection<Segment> Segments
        {
            get => this.segments;
            set => SetProperty(ref this.segments, value);
        }
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
                    StartingPoint = new Point(42,48),
                    EndingPoint = new Point(23,88)
                },
                new Segment()
                {
                    StartingPoint = new Point(84,12),
                    EndingPoint = new Point(72,11)
                },
            };
        }


        #endregion
    }
}
