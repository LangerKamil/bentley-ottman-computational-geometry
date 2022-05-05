using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeometriaObliczeniowa.Models
{
    public sealed class SegmentsDTO : ObservableCollection<SegmentsDTO>
    {
        #region Properties                                                      //TODO CHANGE TO DTO AND MAP WITH MODELS
        public List<SegmentDTO> Segments { get; set; }
        #endregion

        #region Constructors
        public SegmentsDTO()
        {
            this.Segments = new List<SegmentDTO>();
        }
        #endregion

        #region Methods
        public void UpdateBaseModel(SegmentsDTO value = null)
        {
            if (value == null)
            {
                return;
            }
            this.Segments = value.Segments;
        }

        public SegmentsDTO GetBaseModel()
        {
            return this;
        }
        #endregion
    }
}
