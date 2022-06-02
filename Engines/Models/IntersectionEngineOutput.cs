using System.Windows;

namespace GeometriaObliczeniowa.Engines.Models
{
    public sealed class IntersectionEngineOutput
    {
        #region Fields
        private readonly Point coordinates;

        private readonly string output;
        #endregion

        #region Constructors
        public IntersectionEngineOutput(Point coordinates, string output)
        {
            this.coordinates = coordinates;
            this.output = output;
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
        #endregion
    }
}
