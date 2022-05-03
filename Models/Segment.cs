using System.Collections.ObjectModel;
using System.Windows;

namespace GeometriaObliczeniowa.Models
{
    public sealed class Segment : ObservableCollection<Segment>
    {
        #region Properties
        public Point StartingPoint { get; set; }

        public Point EndingPoint { get; set; }
        #endregion

        #region Constructors
        public Segment()
        {

        }
        #endregion
    }
}
