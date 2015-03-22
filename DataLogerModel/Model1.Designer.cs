﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace MDG.DataLogger
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class DataLogEntities1 : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new DataLogEntities1 object using the connection string found in the 'DataLogEntities1' section of the application configuration file.
        /// </summary>
        public DataLogEntities1() : base("name=DataLogEntities1", "DataLogEntities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataLogEntities1 object.
        /// </summary>
        public DataLogEntities1(string connectionString) : base(connectionString, "DataLogEntities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataLogEntities1 object.
        /// </summary>
        public DataLogEntities1(EntityConnection connection) : base(connection, "DataLogEntities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<DataLog> DataLogs
        {
            get
            {
                if ((_DataLogs == null))
                {
                    _DataLogs = base.CreateObjectSet<DataLog>("DataLogs");
                }
                return _DataLogs;
            }
        }
        private ObjectSet<DataLog> _DataLogs;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the DataLogs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToDataLogs(DataLog dataLog)
        {
            base.AddObject("DataLogs", dataLog);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DataLogModel1", Name="DataLog")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DataLog : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DataLog object.
        /// </summary>
        /// <param name="readingDate">Initial value of the ReadingDate property.</param>
        /// <param name="sternAngle">Initial value of the SternAngle property.</param>
        /// <param name="sternWaterSideDraft">Initial value of the SternWaterSideDraft property.</param>
        /// <param name="sternDockSideDraft">Initial value of the SternDockSideDraft property.</param>
        /// <param name="bowAngle">Initial value of the BowAngle property.</param>
        /// <param name="bowWaterSideDraft">Initial value of the BowWaterSideDraft property.</param>
        /// <param name="bowDockSideDraft">Initial value of the BowDockSideDraft property.</param>
        /// <param name="dayNumber">Initial value of the DayNumber property.</param>
        public static DataLog CreateDataLog(global::System.DateTime readingDate, global::System.Double sternAngle, global::System.Double sternWaterSideDraft, global::System.Double sternDockSideDraft, global::System.Double bowAngle, global::System.Double bowWaterSideDraft, global::System.Double bowDockSideDraft, global::System.Int32 dayNumber)
        {
            DataLog dataLog = new DataLog();
            dataLog.ReadingDate = readingDate;
            dataLog.SternAngle = sternAngle;
            dataLog.SternWaterSideDraft = sternWaterSideDraft;
            dataLog.SternDockSideDraft = sternDockSideDraft;
            dataLog.BowAngle = bowAngle;
            dataLog.BowWaterSideDraft = bowWaterSideDraft;
            dataLog.BowDockSideDraft = bowDockSideDraft;
            dataLog.DayNumber = dayNumber;
            return dataLog;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime ReadingDate
        {
            get
            {
                return _ReadingDate;
            }
            set
            {
                if (_ReadingDate != value)
                {
                    OnReadingDateChanging(value);
                    ReportPropertyChanging("ReadingDate");
                    _ReadingDate = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ReadingDate");
                    OnReadingDateChanged();
                }
            }
        }
        private global::System.DateTime _ReadingDate;
        partial void OnReadingDateChanging(global::System.DateTime value);
        partial void OnReadingDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double SternAngle
        {
            get
            {
                return _SternAngle;
            }
            set
            {
                OnSternAngleChanging(value);
                ReportPropertyChanging("SternAngle");
                _SternAngle = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SternAngle");
                OnSternAngleChanged();
            }
        }
        private global::System.Double _SternAngle;
        partial void OnSternAngleChanging(global::System.Double value);
        partial void OnSternAngleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double SternWaterSideDraft
        {
            get
            {
                return _SternWaterSideDraft;
            }
            set
            {
                OnSternWaterSideDraftChanging(value);
                ReportPropertyChanging("SternWaterSideDraft");
                _SternWaterSideDraft = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SternWaterSideDraft");
                OnSternWaterSideDraftChanged();
            }
        }
        private global::System.Double _SternWaterSideDraft;
        partial void OnSternWaterSideDraftChanging(global::System.Double value);
        partial void OnSternWaterSideDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double SternDockSideDraft
        {
            get
            {
                return _SternDockSideDraft;
            }
            set
            {
                OnSternDockSideDraftChanging(value);
                ReportPropertyChanging("SternDockSideDraft");
                _SternDockSideDraft = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SternDockSideDraft");
                OnSternDockSideDraftChanged();
            }
        }
        private global::System.Double _SternDockSideDraft;
        partial void OnSternDockSideDraftChanging(global::System.Double value);
        partial void OnSternDockSideDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double BowAngle
        {
            get
            {
                return _BowAngle;
            }
            set
            {
                OnBowAngleChanging(value);
                ReportPropertyChanging("BowAngle");
                _BowAngle = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BowAngle");
                OnBowAngleChanged();
            }
        }
        private global::System.Double _BowAngle;
        partial void OnBowAngleChanging(global::System.Double value);
        partial void OnBowAngleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double BowWaterSideDraft
        {
            get
            {
                return _BowWaterSideDraft;
            }
            set
            {
                OnBowWaterSideDraftChanging(value);
                ReportPropertyChanging("BowWaterSideDraft");
                _BowWaterSideDraft = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BowWaterSideDraft");
                OnBowWaterSideDraftChanged();
            }
        }
        private global::System.Double _BowWaterSideDraft;
        partial void OnBowWaterSideDraftChanging(global::System.Double value);
        partial void OnBowWaterSideDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double BowDockSideDraft
        {
            get
            {
                return _BowDockSideDraft;
            }
            set
            {
                OnBowDockSideDraftChanging(value);
                ReportPropertyChanging("BowDockSideDraft");
                _BowDockSideDraft = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BowDockSideDraft");
                OnBowDockSideDraftChanged();
            }
        }
        private global::System.Double _BowDockSideDraft;
        partial void OnBowDockSideDraftChanging(global::System.Double value);
        partial void OnBowDockSideDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String LightDraft
        {
            get
            {
                return _LightDraft;
            }
            set
            {
                OnLightDraftChanging(value);
                ReportPropertyChanging("LightDraft");
                _LightDraft = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("LightDraft");
                OnLightDraftChanged();
            }
        }
        private global::System.String _LightDraft;
        partial void OnLightDraftChanging(global::System.String value);
        partial void OnLightDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String HeavyDraft
        {
            get
            {
                return _HeavyDraft;
            }
            set
            {
                OnHeavyDraftChanging(value);
                ReportPropertyChanging("HeavyDraft");
                _HeavyDraft = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("HeavyDraft");
                OnHeavyDraftChanged();
            }
        }
        private global::System.String _HeavyDraft;
        partial void OnHeavyDraftChanging(global::System.String value);
        partial void OnHeavyDraftChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DayNumber
        {
            get
            {
                return _DayNumber;
            }
            set
            {
                if (_DayNumber != value)
                {
                    OnDayNumberChanging(value);
                    ReportPropertyChanging("DayNumber");
                    _DayNumber = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("DayNumber");
                    OnDayNumberChanged();
                }
            }
        }
        private global::System.Int32 _DayNumber;
        partial void OnDayNumberChanging(global::System.Int32 value);
        partial void OnDayNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Double> Depth1
        {
            get
            {
                return _Depth1;
            }
            set
            {
                OnDepth1Changing(value);
                ReportPropertyChanging("Depth1");
                _Depth1 = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Depth1");
                OnDepth1Changed();
            }
        }
        private Nullable<global::System.Double> _Depth1;
        partial void OnDepth1Changing(Nullable<global::System.Double> value);
        partial void OnDepth1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Double> Depth2
        {
            get
            {
                return _Depth2;
            }
            set
            {
                OnDepth2Changing(value);
                ReportPropertyChanging("Depth2");
                _Depth2 = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Depth2");
                OnDepth2Changed();
            }
        }
        private Nullable<global::System.Double> _Depth2;
        partial void OnDepth2Changing(Nullable<global::System.Double> value);
        partial void OnDepth2Changed();

        #endregion

    
    }

    #endregion

    
}