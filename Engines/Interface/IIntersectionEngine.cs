using System.Windows;

namespace GeometriaObliczeniowa.Engines.Interface
{
    public interface IIntersectionEngine
    {
        public Point Intersection(IntersectionEngineInput input);
    }
}
