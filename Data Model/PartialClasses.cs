using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.DynamicData;


namespace Data_Model
{
    /// <summary>
    /// Summary description for PartialClasses
    /// </summary>
    /// 
    /// 
    [MetadataType(typeof(EquipmentMetaData))]
    [ScaffoldTable(true)]
    public partial class Equipment
    {

    }

    [DisplayName("Barges")]
    [TableName("Barges")]
    public class EquipmentMetaData
    {
        [Display(Name = "Factory Draft")]
        public object LightDraft { get; set; }

        [Display(Name = "Barge ID")]
        public object ID { get; set; }
    }

    [MetadataType(typeof(DraftLevelMetaData))]
    [ScaffoldTable(true)]
    public partial class DraftLevel
    {

    }

    [DisplayName("Draft Tables")]
    [TableName("Draft Tables")]
    public class DraftLevelMetaData
    {
        [Display(Name = "Draft Level")]
        public object DraftLevel { get; set; }

        [Display(Name = "Barge ID")]
        public object ID { get; set; }
    }
}
