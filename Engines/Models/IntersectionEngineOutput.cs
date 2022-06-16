using System.Windows;
using GeometriaObliczeniowa.Common.BaseClasses;

namespace GeometriaObliczeniowa.Engines.Models
{
    public sealed class IntersectionEngineOutput
    {
        #region Fields
        private readonly Point coordinates;

        private readonly string output;

        private readonly Line? commonPart;
        #endregion

        #region Constructors
        public IntersectionEngineOutput(Point coordinates, string output, Line commonPart = null)
        {
            this.coordinates = coordinates;
            this.output = output;
            this.commonPart = commonPart;
        }
        #endregion

        #region Methods
        public string GetOutput()
        {
            return this.output;
        }
        public Point GetCoorinates()
        {
            return this.coordinates;
        }

        public Line GetCommonPart()
        {
            return this.commonPart;
        }
        #endregion
    }
}
