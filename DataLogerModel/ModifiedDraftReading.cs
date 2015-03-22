using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDG.DataLogger
{
    public class ModifiedDraftReading
    {
        public string ReadingDate { get; set; }

        public int DayNumber { get; set; }

        public string SternAngle { get; set; }

        public string SternWaterSideDaft { get; set; }

        public string SternDockSideDraft { get; set; }

        public string BowAngle { get; set; }

        public string BowWaterSideDraft { get; set; }

        public string BowDockSideDraft { get; set; }

        public string AverageDraft { get; set; }

        public string HeavyDraft { get; set; }

        public string LightDraft { get; set; }

        public float Depth1 { get; set; }

        public float Depth2 { get; set; }

        public override string ToString ( )
        {
            return string.Format("{10},{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", ReadingDate, SternAngle, SternWaterSideDaft,
                                 SternDockSideDraft, BowAngle, BowWaterSideDraft, BowDockSideDraft, AverageDraft,
                                 LightDraft, HeavyDraft,DayNumber);
        }
    }
}


