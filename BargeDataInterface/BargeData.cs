using System;
using MDG.DataLogger;
using System.Linq;
using MDG.Conversions;

namespace MDG.DataLogger
{
    public class BargeData: IBargeDataModel
    {
        public System.Collections.Generic.List<DataLogger.DataLog> GetEntries ( )
        {
            MDG.DataLogger.DataLogEntities1 _Entity = new DataLogEntities1();
            try
            {
                return _Entity.DataLogs.Where ( x => x.ReadingDate.Day == DateTime.Now.Day ).OrderByDescending( log => log.ReadingDate).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _Entity.Dispose();
            }
        }

        public int ClearEntries ( )
        {
            MDG.DataLogger.DataLogEntities1 _Entity = new DataLogEntities1 ( );
            try
            {
                int count = 0;
                foreach (var dataLog in _Entity.DataLogs)
                {
                    _Entity.DeleteObject(dataLog);
                    count++;
                }
                _Entity.SaveChanges();

                return count;
            }
            catch (Exception ex)
            {

                throw ex;
                
            }
            finally
            {
                _Entity.Dispose ( );
            }
        }

        public System.Collections.Generic.List<DataLog> DataLog
        {
            get { return GetEntries(); }
        }


        public System.Collections.Generic.List<ModifiedDraftReading> DataLogFormated
        {
            get { return GetFormatedEntries(); }
        }

        private System.Collections.Generic.List<DataLogger.ModifiedDraftReading> GetFormatedEntries ( )
        {
            MDG.DataLogger.DataLogEntities1 _Entity = new DataLogEntities1 ( );
            try
            {
                var query =
                    _Entity.DataLogs.Where ( x => x.ReadingDate.Day == DateTime.Now.Day ).OrderByDescending (
                        log => log.ReadingDate ).OrderByDescending ( logEntry => logEntry.ReadingDate );

                var formattedQuery =
                    from rawLogEntry in query.AsEnumerable()
                    select new ModifiedDraftReading()
                               {
                                   ReadingDate =
                                       string.Format(
                                           "{0} : {1} : {2}",
                                           rawLogEntry.
                                               ReadingDate
                                               .Hour,
                                           rawLogEntry.
                                               ReadingDate
                                               .Minute,
                                           rawLogEntry.
                                               ReadingDate
                                               .Second),
                                   DayNumber =
                                       rawLogEntry.
                                       DayNumber,
                                   SternAngle =
                                       rawLogEntry.
                                       SternAngle.ToString
                                       ("N2"),
                                   SternWaterSideDaft = rawLogEntry.
                                               SternWaterSideDraft.ToFeetAndInches(),
                                   SternDockSideDraft = rawLogEntry.SternDockSideDraft.ToFeetAndInches(),
                                   BowAngle =
                                       rawLogEntry.
                                       BowAngle.ToString(
                                           "N2"),
                                   BowWaterSideDraft = rawLogEntry.BowWaterSideDraft.ToFeetAndInches(),
                                   BowDockSideDraft = rawLogEntry.BowDockSideDraft.ToFeetAndInches(),
                                    AverageDraft = ((rawLogEntry.SternWaterSideDraft + 
                                                        rawLogEntry.SternDockSideDraft + 
                                                        rawLogEntry.BowWaterSideDraft + 
                                                        rawLogEntry.BowDockSideDraft) /  4).ToFeetAndInches(),
                                   LightDraft =
                                       rawLogEntry.
                                       LightDraft,
                                   HeavyDraft =
                                       rawLogEntry.
                                       HeavyDraft,
                                       Depth1 = (float) rawLogEntry.Depth1,
                                       Depth2 = (float) rawLogEntry.Depth2
                               };
                return formattedQuery.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _Entity.Dispose ( );
            }
        }
    }
}
