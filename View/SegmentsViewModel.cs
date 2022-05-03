using GeometriaObliczeniowa.Models;

namespace GeometriaObliczeniowa.View
{
    public sealed class SegmentsViewModel : ViewModelBase
    {
        #region Fields
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        #endregion

        #region Properties
        public double X1
        {
            get => x1;
            set { x1 = value; OnPropertyChanged(); }
        }

        public double Y1
        {
            get => y1;
            set { y1 = value; OnPropertyChanged(); }
        }

        public double X2
        {
            get => x2;
            set { x2 = value; OnPropertyChanged(); }
        }

        public double Y2
        {
            get => y2;
            set { y2 = value; OnPropertyChanged(); }
        }
        #endregion

        public SegmentsViewModel()
        {
           
        }
    }
}
