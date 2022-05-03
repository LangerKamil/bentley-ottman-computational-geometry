using GeometriaObliczeniowa.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.Models
{
    public sealed class Segment :  ObservableCollection<Segment>
    {
        #region Fields
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        #endregion

        #region Properties
        public Point StartingPoint { get; set; }
        #endregion

        #region Constructors
        public Segment()
        {
            
        }
        #endregion

        #region Methods
        public Segment GetBaseModel()
        {
            return this;
        }
        #endregion
    }
}
