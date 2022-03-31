using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DriversChangedEventArgs: EventArgs
    {
        #region props
        public Track Track { get; }
        #endregion
        #region Constructor
        public DriversChangedEventArgs(Track t)
        {
            Track = t;
        }
        #endregion

    }
}
