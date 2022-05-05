using System;
using System.Collections.Generic;

namespace GeometriaObliczeniowa.Common.Events
{
    public class ParentUpdateEventArgs : EventArgs
    {
        #region Properties
        public List<string> PropertyNames { get; private set; }
        #endregion

        #region Constructors
        public ParentUpdateEventArgs(List<string> propertyNames)
        {
            this.PropertyNames = propertyNames;
        }
        #endregion
    }
}
