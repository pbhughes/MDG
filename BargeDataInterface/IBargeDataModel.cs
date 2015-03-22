using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDG.DataLogger
{
    interface IBargeDataModel
    {
        List<MDG.DataLogger.DataLog> GetEntries();

        int ClearEntries();

        List<DataLog> DataLog { get; }

        List<ModifiedDraftReading> DataLogFormated { get; } 

        
    }
}
