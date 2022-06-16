using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Resources;
using GeometriaObliczeniowa.Engines.Models;
using System;
using System.Windows;

namespace GeometriaObliczeniowa.Common.Extensions
{
    public static class EngineOutputStringExtensions
    {
        public static string GetIntersectionOutputString(this IntersectionEngineOutput engineOutput)
        {
            return engineOutput.GetOutput();
        }

        public static string GetCoordinatesOutputString(this IntersectionEngineOutput engineOutput)
        {
            Line commonSegment = engineOutput.GetCommonPart();

            if (commonSegment != null)
            {
                return $"[ X: {Math.Round(commonSegment.Left.X)},Y: {Math.Round(commonSegment.Left.Y)} | X: {Math.Round(commonSegment.Right.X)}, Y: {Math.Round(commonSegment.Right.Y)} ]";
            }

            Point coordinates = engineOutput.GetCoorinates();

            return engineOutput.GetOutput() == Strings.Yes ? $"X: {Math.Round(coordinates.X)} Y: {Math.Round(coordinates.Y)}" : "-";
        }
    }
}
