using System.Collections.ObjectModel;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.ViewModels;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels
{
    public class CoordinateSystemElementsViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<SegmentViewModel> segments;
        private ObservableCollection<CoordinateSystemAxesViewModel> axes;

        #endregion

        #region Properties
        public ObservableCollection<SegmentViewModel> Segments
        {
            get { return this.segments; }
            set { SetProperty(ref this.segments, value); }
        }

        public ObservableCollection<CoordinateSystemAxesViewModel> Axes
        {
            get { return this.axes; }
            set { SetProperty(ref this.axes, value); }
        }
        #endregion

        #region Constructors

        public CoordinateSystemElementsViewModel(ObservableCollection<SegmentViewModel> segments)
        {
            this.Segments = segments;
            this.Axes = new ObservableCollection<CoordinateSystemAxesViewModel>();
        }
        #endregion

        public override void InitializeProperties()
        {
        }

        public override void InitializeEvents()
        {
        }

        public override void Dispose()
        {
        }
    }
}
