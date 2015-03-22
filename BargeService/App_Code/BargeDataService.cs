using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for BargeDataService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BargeDataService : System.Web.Services.WebService {

    public BargeDataService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int DraftLevel(string bargeID, string reading)
    {
        using (var context = new BargeDataModelDataContext())
        {
            var q = from draftLevels in context.DraftLevels
                    where draftLevels.ID == bargeID && draftLevels.DraftLevel1 == reading
                    select draftLevels;

            var firstOrDefault = q.FirstOrDefault();
            if (firstOrDefault != null) 
                return firstOrDefault.Tonnage;
            else
            {
                return 0;
            }
        }
    }
    [WebMethod]
    public BargeStructure GetBargeData(string bargeID)
    {
        using (var context = new BargeDataModelDataContext())
        {
            var q = from equip in context.Equipments
                    where equip.ID == bargeID
                    select
                        new BargeStructure()
                            {
                                Length = equip.Length,
                                Width = equip.Width,
                                Height = equip.Height,
                                LightDraft = equip.LightDraft
                            };

            return q.FirstOrDefault();
        }
    }

   
   [WebMethod]
    public string RecordReading(string bargeID, int scaleReading, int instrumentReading,string draftReading)
    {
        using(var context = new BargeDataModelDataContext())
        {
            TestReading test = new TestReading
                                   {
                                       BargeID = bargeID,
                                       DateTaken = DateTime.Now,
                                       ScaleReading = scaleReading,
                                       InstrumentReading = instrumentReading,
                                       DraftReading = draftReading
                                   };
            context.TestReadings.InsertOnSubmit(test);
            context.SubmitChanges();
        }

       return "Reading Recorded";
    }

    public class BargeStructure
    {
        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string LightDraft { get; set; }
    }
    
}
