using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.Models
{
    public sealed class SegmentDTO : ObservableCollection<SegmentDTO>
    {
        #region Properties                                                      
        public Point StartingPoint { get; set; }

        public Point EndingPoint { get; set; }
        #endregion

        #region Constructors
        public SegmentDTO()
        {
        }
        #endregion

        #region Methods
        public void UpdateBaseModel(SegmentDTO value = null)
        {
            if (value == null)
            {
                return;
            }
            this.StartingPoint = value.StartingPoint;
            this.EndingPoint = value.EndingPoint;
        }

        public SegmentDTO GetBaseModel()
        {
            return this;
        }
        #endregion
    }
}
