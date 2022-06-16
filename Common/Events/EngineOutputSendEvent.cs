using GeometriaObliczeniowa.Engines.Models;
using Prism.Events;

namespace GeometriaObliczeniowa.Common.Events
{
    public sealed class EngineOutputSendEvent : PubSubEvent<IntersectionEngineOutput>
    {
    }
}
