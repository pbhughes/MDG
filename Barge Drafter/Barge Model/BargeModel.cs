using System.ComponentModel;
using System;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WSMBS;
using System.IO;
using MDG.DataLogger;
using System.Collections.Generic;
using MDG.Conversions;
using Parity = WSMBS.Parity;
using Timer = System.Timers.Timer;

namespace MDG.Model
{
    public class BargeModel : INotifyPropertyChanged, IDisposable
    {

        #region Properties and Fields

        private int logCount = 0;
        private BackgroundWorker _bw = new BackgroundWorker();
        private const string SETTINGS_FILE_NAME = "ScaleSettings.xml";
        public const string DIALOG_TITLE = "I - Draft";
        private const double RADIAN = Math.PI/180;
        private const double RADIANINV = 180/Math.PI;
        
        private bool _modBusIsConnected;
        public bool ModBusIsConnected
        {
            get { return _modBusIsConnected; }
            set
            {
                _modBusIsConnected = value;
                OnPropertyChanged("ModBusIsConnected");
            }
        }

        private bool _readInProgress = false;

        public bool ReadInProgress
        {
            get { return _readInProgress; }
            set
            {
                _readInProgress = value;
                OnPropertyChanged("ReadInProgress");
            }
        }

        private bool _referenceAnglesAreSet = false;

        public bool ReferenceAnglesAreSet
        {
            get { return _referenceAnglesAreSet; }
            set { _referenceAnglesAreSet = value; }
        }

        private Int32 _rawRoll;

        public Int32 RawRoll
        {
            get { return _rawRoll; }
            set
            {
                _rawRoll = value;
                OnPropertyChanged("RawRoll");
            }
        }

        private UInt16 _rawPitch;

        public ushort RawPitch
        {
            get { return _rawPitch; }
            set
            {
                _rawPitch = value;
                OnPropertyChanged("RawPitch");
            }
        }

        private UInt16 _rawDepth;

        public ushort RawDepth
        {
            get { return _rawDepth; }
            set
            {
                _rawDepth = value;
                OnPropertyChanged("RawDepth");
            }
        }

        private Timer _simulator;

        public bool IsBusy
        {
           get { return _bw.IsBusy; }
        }
        #region Log File Properties

        private const string LOGFILENAME = "DrafterLog";
        private int _runCount = 0;
        private TextWriter _tw;

        #endregion

        #region MODBUS Properties

        private bool _inStartupMode;

        public bool InStartupMode
        {
            get { return _inStartupMode; }
            set
            {
                _inStartupMode = value;
                OnPropertyChanged("InStartupMode");
            }
        }

        private string _comPortName;

        public string ComPortName
        {
            get { return _comPortName; }
            set
            {
                _comPortName = value;
                OnPropertyChanged("ComPortName");
            }
        }

        private WSMBSControl _modbus;

        public Result Node1ModResult;
        public Result Node2ModResult;

        private string _modbusError;

        public string ModbusError
        {
            get { return _modbusError; }
            set
            {
                _modbusError = value;
                OnPropertyChanged("ModbusError");
            }
        }

        private string _buttonText = "Go";

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        private int _readingCount;

        public int ReadingCount
        {
            get { return _readingCount; }
            set
            {
                _readingCount = value;
                OnPropertyChanged("ReadingCount");
            }
        }

        private ushort _registerSize = 0;
        private double _readInterval;
        private double _freeboardOffset;

        private ushort _node1StateReg;
        private ushort _node1DepthReg;
        private ushort _node1RollReg;
        private ushort _node2StateReg;
        private ushort _node2DepthReg;
        private ushort _node2RollReg;

        private int _loopCount;
        public int LoopCount
        {
            get { return _loopCount; }
            set
            {
                OnPropertyChanged("LoopCount");
                _loopCount = value;
            }
        }
        #endregion

        #region Angular Properties

        private double _srDraftFeet;
        public double SternReferenceDraftFeet
        {
            get { return _srDraftFeet; }
            set { _srDraftFeet = value;
            OnPropertyChanged("SternReferenceDraftFeet");}
        }

        private double _srDraftInches;
        public double SternReferenceDraftInches
        {
            get { return _srDraftInches; }
            set
            {
                _srDraftInches = value;
                OnPropertyChanged("SternReferenceDraftInches");
            }
        }

        private double _tareAngleStern;

        public double TareAngleStern
        {
            get { return _tareAngleStern; }
            set
            {
                _tareAngleStern = value;
                OnPropertyChanged("TareAngleStern");
            }
        }

        private double _brDraftFeet;
        public double BowReferenceDraftFeet
        {
            get { return _brDraftFeet; }
            set
            {
                _brDraftFeet = value;
                OnPropertyChanged("BowReferenceDraftFeet");
            }
        }

        private double _brDraftInches;
        public double BowReferenceDraftInches
        {
            get { return _brDraftInches; }
            set
            {
                _brDraftInches = value;
                OnPropertyChanged("BowReferenceDraftInches");
            }
        }

        private double _tareAngleBow;

        public double TareAngleBow
        {
            get { return _tareAngleBow; }
            set
            {
                _tareAngleBow = value;
                OnPropertyChanged("TareAngleBow");
            }
        }

        private double _rollAngleNode2;

        public double RollAngleNode2
        {
            get { return _rollAngleNode2; }
            set
            {
                _rollAngleNode2 = value;
                OnPropertyChanged("RollAngleNode2");
            }
        }

        private double _rollAngleNode1;

        public double RollAngleNode1
        {
            get { return _rollAngleNode1; }
            set
            {
                _rollAngleNode1 = value;
                OnPropertyChanged("RollAngleNode1");
            }
        }

        private double _pitchAngle;

        public double PitchAngle
        {
            get { return _pitchAngle; }
            set
            {
                _pitchAngle = value;
                OnPropertyChanged("PitchAngle");
            }
        }



        #endregion

        #region scale properties

        private double _node1DepthCountsLow;
        private double _node1DepthCountsHigh;
        private double _node1DepthActualLow;
        private double _node1DepthActualHigh;

        private double _node1RollCountsLow;
        private double _node1RollCountsHigh;
        private double _node1RollActualLow;
        private double _node1RollActualHigh;

        private double _node2DepthCountsLow;
        private double _node2DepthCountsHigh;
        private double _node2DepthActualLow;
        private double _node2DepthActualHigh;

        private double _node2RollCountsLow;
        private double _node2RollCountsHigh;
        private double _node2RollActualLow;
        private double _node2RollActualHigh;

        #endregion

        #region Draft Properties

        private double _lightDraftTonnage;

        public double LightDraftTonnage
        {
            get { return _lightDraftTonnage; }
            set
            {
                _lightDraftTonnage = value;
                OnPropertyChanged("LightDraftTonnage");
            }
        }

        private double _heavyDraftTonnage;

        public double HeavyDraftTonnage
        {
            get { return _heavyDraftTonnage; }
            set
            {
                _heavyDraftTonnage = value;
                OnPropertyChanged("HeavyDraftTonnage");
            }
        }

        private double _lightDraftReading;

        public double LightDraftReading
        {
            get { return _lightDraftReading; }
            set
            {
                _lightDraftReading = value;
                OnPropertyChanged("LightDraftReading");
            }
        }

        private double _payload;

        public double Payload
        {
            get { return _payload; }
            set
            {
                _payload = value;
                OnPropertyChanged("Payload");
            }
        }

        private double _tonsPerInch;

        public double TonsPerInch
        {
            get { return _tonsPerInch; }
            set
            {
                _tonsPerInch = value;
                OnPropertyChanged("TonsPerInch");
            }
        }

        private double _depthMeasurement;

        public double DepthMeasurement
        {
            get { return _depthMeasurement; }
            set
            {
                _depthMeasurement = value;
                OnPropertyChanged("DepthMeasurement");
            }
        }

        private double _dockSideFreeboardAft;

        public double DockSideFreeboardAft
        {
            get { return _dockSideFreeboardAft; }
            set
            {
                _dockSideFreeboardAft = value;
                OnPropertyChanged("DockSideFreeboardAft");
            }
        }

        private double _dockSideFreeboardForward;

        public double DockSideFreeboardForward
        {
            get { return _dockSideFreeboardForward; }
            set
            {
                _dockSideFreeboardForward = value;
                OnPropertyChanged("DockSideFreeboardForward");
            }
        }

        private double _averageDraft;

        public double AverageDraft
        {
            get { return _averageDraft; }
            set
            {
                _averageDraft = value;
                OnPropertyChanged("AverageDraft");
            }
        }

        private double _waterSideFreeboardAft;

        public double WaterSideFreeboardAft
        {
            get { return _waterSideFreeboardAft; }
            set
            {
                _waterSideFreeboardAft = value;
                OnPropertyChanged("WaterSideFreeboardAft");
            }
        }

        private double _waterSideFreeboardForward;

        public double WaterSideFreeboardForward
        {
            get { return _waterSideFreeboardForward; }
            set
            {
                _waterSideFreeboardForward = value;
                OnPropertyChanged("WaterSideFreeboardForward");
            }
        }

        private string _lightDraft;

        public string LightDraft
        {
            get { return _lightDraft; }
            set
            {
                _lightDraft = value;
                OnPropertyChanged("LightDraft");
            }
        }

        private string _heavyDraft;

        public string HeavyDraft
        {
            get { return _heavyDraft; }
            set
            {
                _heavyDraft = value;
                OnPropertyChanged("HeavyDraft");
            }
        }

        #endregion

        #region Structure Properties

        private List<double> _inches;

        public List<double> Inches
        {
            get { return _inches; }
            set { _inches = value; }
        }

        private List<double> _bargeDepthOptions;

        public List<double> BargeDepthOptions
        {
            get { return _bargeDepthOptions; }
        }

        private List<double> _bargeWidthOptions;

        public List<double> BargeWidthOptions
        {
            get { return _bargeWidthOptions; }
        }

        private List<double> _deckPlateOptions;

        public List<double> DeckPlateOptions
        {
            get { return _deckPlateOptions; }
        }

        private double _blackBoxDeckPlate;

        public double BlackBoxDeckPlate
        {
            get { return _blackBoxDeckPlate; }
            set
            {
                _blackBoxDeckPlate = value;
                OnPropertyChanged("BlackBoxDeckPlate");
            }
        }

        private double _yellowBoxDeckPlate;

        public double YellowBoxDeckPlate
        {
            get { return _yellowBoxDeckPlate; }
            set
            {
                _yellowBoxDeckPlate = value;
                OnPropertyChanged("YellowBoxDeckPlate");
            }
        }

        private double _bargeWidth = 35.0;

        public double BargeWidth
        {
            get { return _bargeWidth; }
            set
            {
                _bargeWidth = value;
                _bargeWidthInches = value*12;
                OnPropertyChanged("BargeWidth");
            }
        }

        private double _bargeLengthInches;
        private double _bargeWidthInches;


        private double _bargeLength = 200.0;

        public double BargeLength
        {
            get { return _bargeLength; }
            set
            {
                _bargeLength = value;
                _bargeLengthInches = value*12;
                OnPropertyChanged("BargeLength");
            }
        }

        private double _bargeDepth;

        public double BargeDepth
        {
            get { return _bargeDepth; }
            set
            {
                _bargeDepth = value;
                SetBargeDepthOptions(0, (int) (value + 1));
                OnPropertyChanged("BargeDepth");
            }
        }

        #endregion

        #region Visual Control Properties



        #endregion

        #region Logging

        private bool _logReadings = false;

        public bool LogReadings
        {
            get { return _logReadings; }
            set
            {
                _logReadings = value;
                OnPropertyChanged("LogReadings");
            }
        }

        #endregion

        #endregion

        #region Construction / Destruction

        public BargeModel()
        {
            try
            {
                ReadSettings ( );
                StartModbusCommunication ( );

                _bw.WorkerReportsProgress = true;
                _bw.WorkerSupportsCancellation = true;
                _bw.DoWork += new DoWorkEventHandler ( _bw_DoWork );
                _bw.ProgressChanged += new ProgressChangedEventHandler ( _bw_ProgressChanged );
                _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler ( _bw_RunWorkerCompleted );
            }
            catch (Exception ex)
            {
                ReportStatus(String.Format("Error reading configuration file.  Error Text: {0}",ex.Message), true);
            }
          
        }

        private void ReadSettings ( )
        {
            XDocument xdoc;
            if (File.Exists(SETTINGS_FILE_NAME))
            {
                //reading scale settings from the file
                xdoc = XDocument.Load(SETTINGS_FILE_NAME);
                var settingsBase = xdoc.Element("Scales");

                if (settingsBase != null)
                {
                    //load com port
                    //load register size
                    var xComPort = settingsBase.Element ( "comport" );
                    if (xComPort != null)
                    {
                        _comPortName = xComPort.Value.ToString ( );
                        
                    }
                    else
                    {
                        
                    }

                    //load register size
                    var xRegisterSize = settingsBase.Element("registerSize");
                    if (xRegisterSize != null)
                    {
                        string high = xRegisterSize.Value.ToString();
                        _registerSize = UInt16.Parse(high);
                    }
                    else
                    {
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Register Size"));
                    }

                    //load the read interval
                    var xReadInterval = settingsBase.Element("readInterval");
                    if (xReadInterval != null)
                    {
                        string high = xReadInterval.Value.ToString();
                        _readInterval = Double.Parse(high);
                    }
                    else
                    {
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Read Interval"));
                    }

                    //read freeboard offset
                    var xFreeboardOffset = settingsBase.Element("freeboardOffset");
                    if (xFreeboardOffset != null)
                    {
                        string high = xFreeboardOffset.Value.ToString();
                        _freeboardOffset = Double.Parse(high);
                    }
                    else
                    {
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Freeboard Offset"));
                    }

                    //read required number of reads offset
                    var xReadCount = settingsBase.Element("loopCount");
                    if (xReadCount != null)
                    {
                        string val = xReadCount.Value.ToString();
                        LoopCount = Int32.Parse(val);
                    }
                    else
                    {
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Loop Count"));
                    }

                    //process Node1
                    var node1 = settingsBase.Element("node1");
                    if (node1 == null)
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Node 1"));
                    ProcessNode1Settings(node1);

                    //process Node 2
                    var node2 = settingsBase.Element("node2");
                    if (node2 == null)
                        throw new Exception(String.Format("Missing {0} value in the settings file", "Node 2"));
                    ProcessNode2Settings(node2);
                }

                else
                {
                    throw new Exception("Settings file is corrupt.");
                }
            }
            else
            {
                throw new Exception("Missing the settings file");
            }


            //SternReferenceDraftInches = 0.0;
            //BowReferenceDraftInches = 0.0;
            //AverageDraft = 0.0;
            //LightDraft = AverageDraft.ToFeetAndInches ( );
            //HeavyDraft = AverageDraft.ToFeetAndInches ( );

        }

        private void ProcessNode1Settings(XElement node1)
        {
            //process register settings
            var xNodeStateReg = node1.Element("stateRegister");
            if (xNodeStateReg != null)
            {
                string val = xNodeStateReg.Value.ToString();
                _node1StateReg = UInt16.Parse(val);
            }
            else
            {
                throw new Exception(String.Format("Missing {0} value in the settings file", "State Register"));
            }

            //process depth register
            var xNode1DepthReg = node1.Element("depthRegister");
            if (xNode1DepthReg != null)
            {
                string val = xNode1DepthReg.Value.ToString();
                _node1DepthReg = UInt16.Parse(val);
            }
            else
            {
                throw new Exception(String.Format("Missing {0} value in the settings file", "Depth Register"));
            }

            //process roll register
            var xNode1RollReg = node1.Element("rollRegister");
            if (xNode1RollReg != null)
            {
                string val = xNode1RollReg.Value.ToString();
                _node1RollReg = UInt16.Parse(val);
            }
            else
            {
                throw new Exception(String.Format("Missing {0} value in the settings file", "Roll Register"));
            }

            //process  scaling
            //process depth
            var node1Depth = node1.Element("depth");


            //process depth raw low
            var node1DepthRaw = node1Depth.Element("raw");
            var node1DepthReal = node1Depth.Element("real");

            ProcessNode1DepthRaw(node1DepthRaw);
            ProcessNode1DepthReal(node1DepthReal);

            //process roll
            var node1Roll = node1.Element("roll");
            //process row raw real
            var node1RollRaw = node1Roll.Element("raw");
            var node1RollReal = node1Roll.Element("real");

            ProcessNode1RollRaw(node1RollRaw);
            ProcessNode1RollReal(node1RollReal);

        }

        private void ProcessNode2Settings(XElement node2)
        {

            //process register settings
            var xNodeStateReg = node2.Element("stateRegister");
            if (xNodeStateReg != null)
            {
                string val = xNodeStateReg.Value.ToString();
                _node2StateReg = UInt16.Parse(val);
            }

            //process depth register
            var xNodeDepthReg = node2.Element("depthRegister");
            if (xNodeDepthReg != null)
            {
                string val = xNodeDepthReg.Value.ToString();
                _node2DepthReg = UInt16.Parse(val);
            }

            //process roll register
            var xNodeRollReg = node2.Element("rollRegister");
            if (xNodeRollReg != null)
            {
                string val = xNodeRollReg.Value.ToString();
                _node2RollReg = UInt16.Parse(val);
            }


            //process scaling
            //process depth
            var node2Depth = node2.Element("depth");

            //process depth raw real
            var node2DepthRaw = node2Depth.Element("raw");
            var node2DepthReal = node2Depth.Element("real");

            ProcessNode2DepthRaw(node2DepthRaw);
            ProcessNode2DepthReal(node2DepthReal);

            //process roll
            var node2Roll = node2.Element("roll");
            //process row raw real
            var node2RollRaw = node2Roll.Element("raw");
            var node2RollReal = node2Roll.Element("real");

            ProcessNode2RollRaw(node2RollRaw);
            ProcessNode2RollReal(node2RollReal);

        }

        private void ProcessNode1DepthRaw(XElement node1DepthRaw)
        {

            //process high
            var xNode1DepthLow = node1DepthRaw.Element("low");
            if (xNode1DepthLow != null)
            {
                string val = xNode1DepthLow.Value.ToString();
                _node1DepthCountsLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Depth Raw Low " ) );
            }


            var xNode1DepthHigh = node1DepthRaw.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node1DepthCountsHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Depth Raw High" ) );
            }
        }

        private void ProcessNode1DepthReal(XElement node1DepthReal)
        {

            //process high
            var xNode1DepthLow = node1DepthReal.Element("low");
            if (xNode1DepthLow != null)
            {
                string val = xNode1DepthLow.Value.ToString();
                _node1DepthActualLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Depth Real Low" ) );
            }

            var xNode1DepthHigh = node1DepthReal.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node1DepthActualHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Depth Real High" ) );
            }
        }

        private void ProcessNode2DepthRaw(XElement node1DepthRaw)
        {

            //process high
            var xNode2DepthLow = node1DepthRaw.Element("low");
            if (xNode2DepthLow != null)
            {
                string val = xNode2DepthLow.Value.ToString();
                _node2DepthCountsLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 2 Depth Raw Low" ) );
            }

            var xNode2DepthHigh = node1DepthRaw.Element("high");
            if (xNode2DepthHigh != null)
            {
                string val = xNode2DepthHigh.Value.ToString();
                _node2DepthCountsHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 2 Depth Raw High" ) );
            }
        }

        private void ProcessNode2DepthReal(XElement node2DepthReal)
        {

            //process high
            var xNode2DepthLow = node2DepthReal.Element("low");
            if (xNode2DepthLow != null)
            {
                string val = xNode2DepthLow.Value.ToString();
                _node2DepthActualLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 2 Depth Real Low" ) );
            }

            var xNode2DepthHigh = node2DepthReal.Element("high");
            if (xNode2DepthHigh != null)
            {
                string val = xNode2DepthHigh.Value.ToString();
                _node2DepthActualHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 2 Depth Real High" ) );
            }
        }

        private void ProcessNode1RollRaw(XElement node1RollRaw)
        {

            //process high
            var rollLow = node1RollRaw.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node1RollCountsLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Raw Low" ) );
            }

            var rollHigh = node1RollRaw.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node1RollCountsHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Raw High" ) );
            }
        }

        private void ProcessNode1RollReal(XElement node1RollReal)
        {

            //process high
            var rollLow = node1RollReal.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node1RollActualLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Real Low" ) );
            }

            var rollHigh = node1RollReal.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node1RollActualHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Real High" ) );
            }
        }

        private void ProcessNode2RollRaw(XElement node2RollRaw)
        {

            //process high
            var rollLow = node2RollRaw.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node2RollCountsLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Raw Low" ) );
            }

            var rollHigh = node2RollRaw.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node2RollCountsHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Raw High" ) );
            }
        }

        private void ProcessNode2RollReal(XElement node2RollReal)
        {

            //process high
            var rollLow = node2RollReal.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node2RollActualLow = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Real Low" ) );
            }

            var rollHigh = node2RollReal.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node2RollActualHigh = Double.Parse(val);
            }
            else
            {
                throw new Exception ( String.Format ( "Missing {0} value in the settings file", "Node 1 Real High" ) );
            }
        }

        #endregion

        #region Functions and Methods

        #region Test Support

        private double _aftOppDelta;

        public double AftOppDelta
        {
            get { return _aftOppDelta; }
            set
            {
                _aftOppDelta = value;
                OnPropertyChanged("AftOppDelta");
            }
        }

        private double _fowardOppDelta;

        public double ForwardOppDelta
        {
            get { return _fowardOppDelta; }
            set
            {
                _fowardOppDelta = value;
                OnPropertyChanged("ForwardOppDelta");
            }
        }

        private double _fowardAdjDelta;

        public double ForwardAdjDelta
        {
            get { return _fowardAdjDelta; }
            set
            {
                _fowardAdjDelta = value;
                OnPropertyChanged("ForwardAdjDelta");
            }
        }


        #endregion

        public void ReportStatus(string error, bool showMessage = false)
        {
            ModbusError = error;
            if (showMessage)
            {
                string msg =
                    String.Format ( "{0} \n1) Try cycling the power on the instruments. \n2) Restart the application.", error );
                //Application.Current.Dispatcher.Invoke (
                //    new Action ( ( ) => MessageBox.Show ( Application.Current.MainWindow, msg,
                //                                     "I Draft Error", MessageBoxButton.OK,
                //                                     MessageBoxImage.None, MessageBoxResult.OK,
                //                                     MessageBoxOptions.None ) ) );
            }

        }

       

        public void AddDeckPlateOption(double value)
        {
            if (_deckPlateOptions == null)
                _deckPlateOptions = new List<double>();

            _deckPlateOptions.Add(value);
        }

        public void AddBargeWidthOption(double width)
        {
            if (_bargeWidthOptions == null)
                _bargeWidthOptions = new List<double>();

            _bargeWidthOptions.Add(width);
        }

        public void AddBargeDepthOptions(double depth)
        {
            if (_bargeDepthOptions == null)
                _bargeDepthOptions = new List<double>();

            _bargeDepthOptions.Add(depth);
        }

        public void SetBargeDepthOptions(int start, int stop)
        {
            if (_bargeDepthOptions == null)
                _bargeDepthOptions = new List<double>();

            _bargeDepthOptions.Clear();
            for (int i = start; i < stop; i++)
                _bargeDepthOptions.Add((double) i);
        }

        public void AddInchOptions()
        {
            if (_inches == null)
                _inches = new List<double>() {0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0};
        }

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var start = DateTime.Now;

            try
            {
                
                AveragingStack<ushort> node1DepthAvg = new AveragingStack<ushort>();
                AveragingStack<ushort> node2DepthAvg = new AveragingStack<ushort>();

                UInt16 node1Roll = 0;
                UInt16 node1State = 0;
                UInt16 node2Roll = 0;
                UInt16 node2state = 0;

                var register = new short[_registerSize];
                ReadingCount = 0;
                InStartupMode = true;
                ReadInProgress = true;

                for (int i = 1; i <= LoopCount; i++)
                {
                    if (_bw.IsBusy && _bw.CancellationPending)
                        break;

                     

                     _bw.ReportProgress(i);

                    Node1ModResult = _modbus.ReadHoldingRegisters(1, 0, _registerSize, register);

                    node1State = (UInt16) register[_node1StateReg];
                    if (node1State == 128)
                    {
                        node1DepthAvg.Add((UInt16) register[_node1DepthReg]);
                        node1Roll = (UInt16) register[_node1RollReg];
                    }
                    else
                    {
                       StopTakingData ( );
                       ReportStatus("Error reading Black Node", true);
                       break;
                    }

                    node2state = (UInt16) register[_node2StateReg];
                    if (node2state == 128)
                    {
                        node2DepthAvg.Add((UInt16) register[_node2DepthReg]);
                        node2Roll = (UInt16) register[_node2RollReg];
                    }
                    else
                    {
                        StopTakingData ( );
                        ReportStatus ( "Error reading Yellow Node" , true);
                        break;
                    }


                    Thread.Sleep( (int)(_readInterval*1000) );

                }
                Double[] readings = {
                                        node1DepthAvg.Average, node1Roll, node1State, node2DepthAvg.Average, node2Roll,
                                        node2state
                                    };

                ProcessReadings ( readings );

                ReadInProgress = false;
                TimeSpan dur = new TimeSpan(DateTime.Now.Ticks - start.Ticks);
                int intDur = (int)dur.TotalSeconds;
                ReportStatus(
                    String.Format("Reading Every: {0} {1}",
                    (intDur.ToString()),
                    (intDur == 1) ? "Second" : "Seconds"));
            }
            catch (Exception ex)
            {
                ReadInProgress = false;
                throw ex;
            }
            finally
            {
                
            }

        }

        private void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReadingCount = e.ProgressPercentage;
        }

        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (InStartupMode)
                _bw.RunWorkerAsync();

        }

        private double CalculateAverageFreeboard()
        {
            return (WaterSideFreeboardAft + WaterSideFreeboardForward + DockSideFreeboardAft + DockSideFreeboardForward)/
                   4;
        }

        private double CalculateStroke(double sensor, double angle, double adjacent)
        {

            double stroke;

            double rads = Math.Abs(angle)*RADIAN;
            stroke = adjacent*Math.Tan(rads);


            if (angle < 0)
                stroke = -stroke;

            return stroke;
        }


        #endregion

        #region MODBUS Functions

        public void StartTakingData()
        {
            try
            {
                if (_bw.IsBusy && _bw.CancellationPending)
                {
                    MessageBox.Show("Waiting on read thread to stop, try again", DIALOG_TITLE, MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    return;
                }
                _bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Unable to take data try restarting the application.", DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        public void StartModbusCommunication ( )
        {
            try
            {
                InStartupMode = true;
                if (_modbus == null)
                    _modbus = new WSMBSControl();

                if (ModBusIsConnected)
                {
                    StopTakingData();
                    return;
                }

                String[] comPortsToCheck;
                //if the comport is specified in the 
                //config file add just that port to the list
                //if it's not specified then try them all
                if (String.IsNullOrEmpty ( _comPortName ))
                {
                    comPortsToCheck = SerialPort.GetPortNames();
                }
                else
                {
                    comPortsToCheck = new string[] { _comPortName };
                }

                if (comPortsToCheck.Length == 0)
                    throw new Exception("No COM port detected unable to connect. Try plugging in the gateway");

                foreach (string portName in comPortsToCheck)
                {
                    _modbus.Mode = Mode.RTU;
                    _modbus.PortName = portName;
                    _modbus.BaudRate = 19200;
                    _modbus.StopBits = 1;
                    _modbus.Parity = Parity.None;
                    _modbus.ResponseTimeout = 950;
                    _modbus.LicenseKey("0AA1-91A6-7BE9-08B7-1A9A-B68B");

                    Node1ModResult = _modbus.Open();


                    if (Node1ModResult != Result.SUCCESS)
                    {
                        ModbusError = String.Format("Connecting to communicator port {0} - {1}", portName,
                                                    _modbus.GetLastErrorString());
                        ButtonText = "Error";
                        InStartupMode = false;
                        ModBusIsConnected = false;
                        ReportStatus ( ModbusError, true );
                        return;

                    }
                    else
                    {
                        //clear the modbus error incase there was one
                        ModbusError = "Communication port connected...";
                        ModBusIsConnected = true;
                        InStartupMode = true;
                        //_bw.RunWorkerAsync();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                ReportStatus(String.Format("Unable to connect to sensor network: {0}", ex.Message), true);
            }


        }

        public void StopTakingData()
        {
            try
            {
                ReadInProgress = false;
                InStartupMode = false;
                _bw.CancelAsync();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Error trying to stop data acquisition try starting and stopping.", DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Error );
            }


        }

        private double ScaleAngle(double value, double fromMin, double fromMax, double toMin, double toMax)
        {
            try
            {
                if (value == 0.0)
                    return value;

                double newValue = Scale(value, fromMin, fromMax, toMin, toMax);
                return newValue;

            }
            catch
            {
                return (float) 0.0;
            }
        }

        private double ScaleDepth(double value, double oldMin, double oldMax, double newMin, double newMax)
        {
            try
            {

               

                double newValue = Scale(value, oldMin, oldMax, newMin, newMax);
                return newValue;

            }
            catch
            {
                return (float) 0.0;
            }
        }

        private double Scale(double value, double oldMin, double oldMax, double newMin, double newMax)
        {
            //if (value < (oldMin - (oldMin * .05)) || value > (oldMax + (oldMax * .05)))
            //{
            //    ReportStatus ( "Reading out of range" );
            //    return 0D;

            //}

            if (value < oldMin)
                return newMin;

            if (value > oldMax)
                return newMax;

            return (((value - oldMin)*(newMax - newMin))/(oldMax - oldMin)) + newMin;
        }

        private void ProcessReadings(double[] registers)
        {

            WaterSideFreeboardForward = ScaleDepth(registers[3], _node2DepthCountsLow, _node2DepthCountsHigh,
                                                   _node2DepthActualLow, _node2DepthActualHigh) - _freeboardOffset ;
                //Yellow Box

            WaterSideFreeboardAft = ScaleDepth(registers[0], _node1DepthCountsLow, _node1DepthCountsHigh,
                                               _node1DepthActualLow, _node1DepthActualHigh) - _freeboardOffset ;
                //Black box



            RollAngleNode1 =
                (ScaleAngle ( registers[1], _node1RollCountsLow, _node1RollCountsHigh, _node1RollActualLow,
                            _node1RollActualHigh ) - (TareAngleStern));

            RollAngleNode1 = RollAngleNode1*-1;

            RollAngleNode2 =
                (ScaleAngle(registers[4], _node2RollCountsLow, _node2RollCountsHigh, _node2RollActualLow,
                            _node2RollActualHigh) - (TareAngleBow));



            RollAngleNode2 = RollAngleNode2*-1;

            DockSideFreeboardAft = WaterSideFreeboardAft +
                                   CalculateStroke(WaterSideFreeboardAft, RollAngleNode1, BargeWidth*12);

            DockSideFreeboardForward = WaterSideFreeboardForward +
                                       CalculateStroke(WaterSideFreeboardForward, RollAngleNode2, BargeWidth*12) ;

            double avgDraft = (BargeDepth*12) - CalculateAverageFreeboard();

            HeavyDraftTonnage = TonsPerInch*(avgDraft);
            Payload = Math.Round( (HeavyDraftTonnage - LightDraftTonnage),0);

            if (_logReadings)
            {
                DataLogEntities1 logger = null;
                try
                {
                    logger = new DataLogEntities1();

                    DataLog newLogEntry = new DataLog()
                                              {
                                                  ReadingDate = DateTime.Now,
                                                  SternAngle = RollAngleNode1,
                                                  SternWaterSideDraft = (BargeDepth*12) - WaterSideFreeboardAft,
                                                  SternDockSideDraft = (BargeDepth*12) - DockSideFreeboardAft,
                                                  BowAngle = RollAngleNode2,
                                                  BowDockSideDraft = (BargeDepth*12) - DockSideFreeboardForward,
                                                  BowWaterSideDraft = (BargeDepth*12) - WaterSideFreeboardForward,
                                                  LightDraft = LightDraft,
                                                  HeavyDraft = HeavyDraft,
                                                  DayNumber = logCount++,
                                                  Depth1 = registers[3],
                                                  Depth2 = registers[0]
                                              };
                    logger.AddToDataLogs(newLogEntry);
                    logger.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (logger != null)
                        logger.Dispose();
                }
            }



        }

        #endregion

        #region Commands

        private RelayCommand _newBargeCommand;

        public ICommand NewBargeCommand
        {
            get
            {
                return _newBargeCommand ??
                       (_newBargeCommand =
                        new RelayCommand ( param => ExecuteNewBargeCommand(), param => CanExecuteNewBargeCommand( ) ));
            }
        }

        private RelayCommand _calcReferenceAnglesCommand;

        public ICommand CalcReferenceAnglesCommand
        {
            get
            {
                return _calcReferenceAnglesCommand ??
                       (_calcReferenceAnglesCommand =
                        new RelayCommand(param => CalibrateBarge(), param => CanCalcReferenceAngles()));
            }
        }

        private RelayCommand _clearReferenceAngles;

        public ICommand ClearReferenceAngles
        {
            get
            {
                return _clearReferenceAngles ??
                       (_clearReferenceAngles =
                        new RelayCommand(param => ResetReferenceAnglesFunction(), param => CanClearReferenceAngles()));
            }
        }

        private bool CanCalcReferenceAngles()
        {
            return (BargeDepth > 0);
        }

        private bool CanClearReferenceAngles()
        {

            return true;
        }

        private void CalibrateBarge()
        {
            try
            {
                //StopTakingData();

                while(ReadInProgress)
                    Thread.Sleep(250);

                TareAngleStern = 0.0;
                TareAngleBow = 0.0;
                
                CalculateSternReferenceAngleFunction ( );
                CalculateBowReferenceAngleFunction ( );
            }
            catch (Exception ex)
            {
                ReportStatus ( String.Format ( "Unable to connect to sensor network: {0}", ex.Message ) );
            }
           
        }

        private void CalculateSternReferenceAngleFunction()
        {


            //double sternRefDraft = DockSideFreeboardAft -
            //           (BargeDepth * 12 - ((SternReferenceDraftFeet * 12) + SternReferenceDraftInches));


            //TareAngleStern = (Math.Atan ( sternRefDraft / (BargeWidth * 12) ) * RADIANINV) * -1;

            


        }

        private void CalculateBowReferenceAngleFunction()
        {



            //double bowRefDraft = DockSideFreeboardForward -
            //                        (BargeDepth * 12 - ((BowReferenceDraftFeet * 12) + BowReferenceDraftInches));


            //TareAngleBow = (Math.Atan ( bowRefDraft / (BargeWidth * 12) ) * RADIANINV) * -1;

        }

        private void ResetReferenceAnglesFunction()
        {
            SternReferenceDraftFeet = 0.0;
            SternReferenceDraftInches = 0.0;
            BowReferenceDraftFeet = 0.0;
            BowReferenceDraftInches = 0.0;

            TareAngleBow = 0.0;
            TareAngleStern = 0.0;


        }

        private RelayCommand _startModBusComs;

        public ICommand StartModbusCommunicationCommand
        {
            get
            {
                return _startModBusComs ??
                       (_startModBusComs =
                        new RelayCommand(param => StartTakingData(), param => CanStartTakeData()));
            }
        }

        private bool CanExecuteNewBargeCommand()
        {
            return true;
        }

        private void ExecuteNewBargeCommand()
        {
            if (MessageBox.Show ( "This will clear all current values, are you sure?", DIALOG_TITLE, MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
            {
                StopTakingData();

                LightDraftReading = 0d;

                AverageDraft = 0d;
                Payload = 0d;
                HeavyDraftTonnage = 0d;
                TonsPerInch = 0;
                SternReferenceDraftFeet = 0;
                SternReferenceDraftInches = 0;
                BowReferenceDraftFeet = 0;
                BowReferenceDraftInches = 0;
                BargeDepth = 0;
                TareAngleBow = 0d;
                TareAngleStern = 0d;
                LightDraftTonnage = 00000.000;
                HeavyDraftTonnage = 00000.000;
                LightDraft = AverageDraft.ToFeetAndInches();
                HeavyDraft = AverageDraft.ToFeetAndInches();
                SternReferenceDraftFeet = 0;
                SternReferenceDraftInches = 0;
                BowReferenceDraftFeet = 0;
                BowReferenceDraftInches = 0;

            }
        }

        private bool CanStartTakeData()
        {
            return (BargeDepth > 0) && (BargeWidth > 0) && (ModBusIsConnected) && !InStartupMode && !ReadInProgress;
        }

        private RelayCommand _stopTakingData;

        public ICommand StopTakingDataCommand
        {
            get
            {
                return _stopTakingData ??
                       (_stopTakingData =
                        new RelayCommand(param => StopTakingData(), param => CanStopModbus()));
            }
        }

        private bool CanStopModbus()
        {
            return InStartupMode;
        }

        private RelayCommand _takeLightDraftReading;

        public ICommand TakeLightDraftReadingCommand
        {
            get
            {
                return _takeLightDraftReading ??
                       (_takeLightDraftReading =
                        new RelayCommand(param => TakeLightDraftReading(), param => CanTakeLightDraftReading()));
            }
        }

        private bool CanTakeLightDraftReading()
        {
            return  (BargeDepth > 0);
        }

        private RelayCommand _takeHeavyDraftReading;

        public ICommand TakeHeavyDraftReadingCommand
        {
            get
            {
                return _takeHeavyDraftReading ??
                       (_takeHeavyDraftReading =
                        new RelayCommand(param => TakeHeavyDraftReading(), param => CanTakeHeavyDraftReading()));
            }
        }

        private bool CanTakeHeavyDraftReading()
        {
            return (LightDraftReading > 0) && (BargeDepth > 0);
        }

        public void TakeLightDraftReading()
        {
          
            AverageDraft = (BargeDepth*12) - CalculateAverageFreeboard();
            LightDraftReading = AverageDraft;
            LightDraft = AverageDraft.ToFeetAndInches();
            LightDraftTonnage = LightDraftReading*TonsPerInch;

        }

        public void TakeHeavyDraftReading()
        {

            AverageDraft = (BargeDepth*12) - CalculateAverageFreeboard();
            HeavyDraft = AverageDraft.ToFeetAndInches();
            HeavyDraftTonnage = AverageDraft*TonsPerInch;
            Payload = HeavyDraftTonnage - LightDraftTonnage;
            StopTakingData();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private double EPSILON = 0.033;

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Idispoable

        public void Dispose()
        {
            if (_modbus != null)
            {
                StopTakingData();
                _modbus.Close();
                _modbus = null;
            }

        }

        #endregion
    }

  

}
