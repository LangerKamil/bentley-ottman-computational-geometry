using GeometriaObliczeniowa.Engines.Models;

namespace GeometriaObliczeniowa.Engines.Interface
{
    public interface IIntersectionEngine
    {
        /// <summary>
        ///  Returns IntersectionEngineOutput instance with the intersection <c>Point</c> coordinates and a calculation result message.
        /// </summary>
        /// <param name="input"> IntersectionEngineInput instance.</param>
        /// <returns>The <c>IntersectionEngineOutput</c> instance.</returns>
        public IntersectionEngineOutput FindIntersection(IntersectionEngineInput input);

    }


}
