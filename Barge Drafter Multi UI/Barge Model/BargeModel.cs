using System.ComponentModel;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using WSMBS;
using System.IO;
using System.Configuration;
using MDG.DataLogger;
using System.Collections;
using System.Collections.Generic;
using MDG.Conversions;

namespace MDG.Model
{
    public class BargeModel : INotifyPropertyChanged, IDisposable
    {

        #region Properties and Fields

        private int logCount = 0;
        private BackgroundWorker _bw = new BackgroundWorker();
        private const string ScaleFileName = "ScaleSettings.xml";
        private const double RADIAN = Math.PI/180;
        private const double RADIANINV = 180/Math.PI;
        private bool _modBusIsOn;
        private const string DIALOG_TITLE = "I - Draft";

        public bool ModBusIsOn
        {
            get { return _modBusIsOn; }
            set
            {
                _modBusIsOn = value;
                OnPropertyChanged("ModBusIsOn");
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

        private string _comPortName = "COM8";

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
        private int _readInterval;
        private int _freeboardOffset;

        private ushort _node1StateReg;
        private ushort _node1DepthReg;
        private ushort _node1RollReg;
        private ushort _node2StateReg;
        private ushort _node2DepthReg;
        private ushort _node2RollReg;

        #endregion

        #region Angular Properties


        public double SternReferenceDraftFeet { get; set; }
        public double SternReferenceDraftInches { get; set; }

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

        public double BowReferenceDraftFeet { get; set; }
        public double BowReferenceDraftInches { get; set; }

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
            ReadSettings();
            StartModbusCommunication();
        }

        private void ReadSettings()
        {
            XDocument xdoc;
            try
            {
                if (File.Exists(ScaleFileName))
                {
                    //reading scale settings from the file
                    xdoc = XDocument.Load(ScaleFileName);
                    var settingsBase = xdoc.Element("Scales");

                    if (settingsBase != null)
                    {
                        //load register size
                        var xRegisterSize = settingsBase.Element("registerSize");
                        if (xRegisterSize != null)
                        {
                            string high = xRegisterSize.Value.ToString();
                            _registerSize = ushort.Parse(high);
                        }

                        //load the read interval
                        var xReadInterval = settingsBase.Element("readInterval");
                        if (xReadInterval != null)
                        {
                            string high = xReadInterval.Value.ToString();
                            _readInterval = int.Parse(high);
                        }

                        //read freeboard offset
                        var xFreeboardOffset = settingsBase.Element("freeboardOffset");
                        if (xFreeboardOffset != null)
                        {
                            string high = xFreeboardOffset.Value.ToString();
                            _freeboardOffset = int.Parse(high);
                        }

                        //process Node1
                        var node1 = settingsBase.Element("node1");
                        ProcessNode1Settings(node1);

                        //process Node 2
                        var node2 = settingsBase.Element("node2");
                        ProcessNode2Settings(node2);
                    }

                    else
                    {

                        _readInterval = 1;
                        _freeboardOffset = 12;
                        _registerSize = 48;
                    }
                }


                SternReferenceDraftInches = 0.0;
                BowReferenceDraftInches = 0.0;

                AverageDraft = 0.0;
                LightDraft = AverageDraft.ToFeetAndInches();
                HeavyDraft = AverageDraft.ToFeetAndInches();
                _bw.WorkerReportsProgress = true;
                _bw.WorkerSupportsCancellation = true;
                _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
                _bw.ProgressChanged += new ProgressChangedEventHandler(_bw_ProgressChanged);
                _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ProcessNode1Settings(XElement node1)
        {
            //process register settings
            var xNodeStateReg = node1.Element("stateRegister");
            if (xNodeStateReg != null)
            {
                string val = xNodeStateReg.Value.ToString();
                _node1StateReg = ushort.Parse(val);
            }

            //process depth register
            var xNode1DepthReg = node1.Element("depthRegister");
            if (xNode1DepthReg != null)
            {
                string val = xNode1DepthReg.Value.ToString();
                _node1DepthReg = ushort.Parse(val);
            }

            //process roll register
            var xNode1RollReg = node1.Element("rollRegister");
            if (xNode1RollReg != null)
            {
                string val = xNode1RollReg.Value.ToString();
                _node1RollReg = ushort.Parse(val);
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
                _node2StateReg = ushort.Parse(val);
            }

            //process depth register
            var xNodeDepthReg = node2.Element("depthRegister");
            if (xNodeDepthReg != null)
            {
                string val = xNodeDepthReg.Value.ToString();
                _node2DepthReg = ushort.Parse(val);
            }

            //process roll register
            var xNodeRollReg = node2.Element("rollRegister");
            if (xNodeRollReg != null)
            {
                string val = xNodeRollReg.Value.ToString();
                _node2RollReg = ushort.Parse(val);
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
                _node1DepthCountsLow = double.Parse(val);
            }

            var xNode1DepthHigh = node1DepthRaw.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node1DepthCountsHigh = double.Parse(val);
            }
        }

        private void ProcessNode1DepthReal(XElement node1DepthReal)
        {

            //process high
            var xNode1DepthLow = node1DepthReal.Element("low");
            if (xNode1DepthLow != null)
            {
                string val = xNode1DepthLow.Value.ToString();
                _node1DepthActualLow = double.Parse(val);
            }

            var xNode1DepthHigh = node1DepthReal.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node1DepthActualHigh = double.Parse(val);
            }
        }

        private void ProcessNode2DepthRaw(XElement node1DepthRaw)
        {

            //process high
            var xNode1DepthLow = node1DepthRaw.Element("low");
            if (xNode1DepthLow != null)
            {
                string val = xNode1DepthLow.Value.ToString();
                _node2DepthCountsLow = double.Parse(val);
            }

            var xNode1DepthHigh = node1DepthRaw.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node2DepthCountsHigh = double.Parse(val);
            }
        }

        private void ProcessNode2DepthReal(XElement node1DepthReal)
        {

            //process high
            var xNode1DepthLow = node1DepthReal.Element("low");
            if (xNode1DepthLow != null)
            {
                string val = xNode1DepthLow.Value.ToString();
                _node2DepthActualLow = double.Parse(val);
            }

            var xNode1DepthHigh = node1DepthReal.Element("high");
            if (xNode1DepthHigh != null)
            {
                string val = xNode1DepthHigh.Value.ToString();
                _node2DepthActualHigh = double.Parse(val);
            }
        }

        private void ProcessNode1RollRaw(XElement node1RollRaw)
        {

            //process high
            var rollLow = node1RollRaw.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node1RollCountsLow = double.Parse(val);
            }

            var rollHigh = node1RollRaw.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node1RollCountsHigh = double.Parse(val);
            }
        }

        private void ProcessNode1RollReal(XElement node1RollReal)
        {

            //process high
            var rollLow = node1RollReal.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node1RollActualLow = double.Parse(val);
            }

            var rollHigh = node1RollReal.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node1RollActualHigh = double.Parse(val);
            }
        }

        private void ProcessNode2RollRaw(XElement node2RollRaw)
        {

            //process high
            var rollLow = node2RollRaw.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node2RollCountsLow = double.Parse(val);
            }

            var rollHigh = node2RollRaw.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node2RollCountsHigh = double.Parse(val);
            }
        }

        private void ProcessNode2RollReal(XElement node2RollReal)
        {

            //process high
            var rollLow = node2RollReal.Element("low");
            if (rollLow != null)
            {
                string val = rollLow.Value.ToString();
                _node2RollActualLow = double.Parse(val);
            }

            var rollHigh = node2RollReal.Element("high");
            if (rollHigh != null)
            {
                string val = rollHigh.Value.ToString();
                _node2RollActualHigh = double.Parse(val);
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
            try
            {
                AveragingStack<UInt16> node1DepthAvg = new AveragingStack<ushort>();
                AveragingStack<UInt16> node2DepthAvg = new AveragingStack<ushort>();

                UInt16 node1Roll = 0;
                UInt16 node1State = 0;
                UInt16 node2Roll = 0;
                UInt16 node2state = 0;

                var register = new short[_registerSize];
                ReadingCount = 0;
                InStartupMode = true;

                ReadInProgress = true;
                for (int i = 0; i <= 5; i++)
                {
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
                        node1Roll = 10;
                    }

                    node2state = (UInt16) register[_node2StateReg];
                    if (node2state == 128)
                    {
                        node2DepthAvg.Add((UInt16) register[_node2DepthReg]);
                        node2Roll = (UInt16) register[_node2RollReg];
                    }
                    else
                    {
                        node2Roll = 10;
                    }


                    System.Threading.Thread.Sleep(_readInterval*1000);

                }
                Double[] readings = {
                                        node1DepthAvg.Average, node1Roll, node1State, node2DepthAvg.Average, node2Roll,
                                        node2state
                                    };

                ProcessReadings(readings);

                ReadInProgress = false;
            }
            catch (Exception ex)
            {
                ReadInProgress = false;
                throw ex;
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
                _bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Unable to take data try restarting the application.", DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        public void StartModbusCommunication()
        {
            InStartupMode = true;
            if (_modbus == null)
                _modbus = new WSMBSControl();

            if (ModBusIsOn)
            {
                StopTakingData();

                return;
            }

            String[] comPortsToCheck = {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10"};


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
                    ModbusError = string.Format("Connecting to radio network {0} - {1}", portName,
                                                _modbus.GetLastErrorString());
                    ButtonText = "Error";
                    InStartupMode = false;
                    ModBusIsOn = false;

                }
                else
                {
                    //clear the modbus error incase there was one
                    ModbusError = "Radio Network Connected...";
                    ModBusIsOn = true;
                    InStartupMode = true;
                    _bw.RunWorkerAsync();
                    return;
                }
            }
        }

        public void StopTakingData()
        {
            try
            {
                InStartupMode = false;
                _bw.CancelAsync();
                ModBusIsOn = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Error trying to stop data acquisition try starting and stopping.", DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Error );
            }


        }

        public static double ScaleAngle(double value, double fromMin, double fromMax, double toMin, double toMax)
        {
            try
            {

                double newValue = Scale(value, fromMin, fromMax, toMin, toMax);
                return newValue;

            }
            catch
            {
                return (float) 0.0;
            }
        }

        public static double ScaleDepth(double value, double oldMin, double oldMax, double newMin, double newMax)
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

        private static double Scale(double value, double oldMin, double oldMax, double newMin, double newMax)
        {
            return (((value - oldMin)*(newMax - newMin))/(oldMax - oldMin)) + newMin;
        }

        private void ProcessReadings(double[] registers)
        {
            WaterSideFreeboardForward = ScaleDepth(registers[3], _node2DepthCountsLow, _node2DepthCountsHigh,
                                                   _node2DepthActualLow, _node2DepthActualHigh) - _freeboardOffset;
                //Yellow Box

            WaterSideFreeboardAft = ScaleDepth(registers[0], _node1DepthCountsLow, _node1DepthCountsHigh,
                                               _node1DepthActualLow, _node1DepthActualHigh) - _freeboardOffset;
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
                MDG.DataLogger.DataLogEntities1 logger = null;
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
                        new RelayCommand(param => CalcReferenceAngles(), param => CanCalcReferenceAngles()));
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
            return true;
        }


        private bool CanClearReferenceAngles()
        {

            return true;
        }

        private void CalcReferenceAngles()
        {
            CalculateSternReferenceAngleFunction();
            CalculateBowReferenceAngleFunction();
        }

        private void CalculateSternReferenceAngleFunction()
        {
            if (Math.Abs(SternReferenceDraftFeet - 0.0) > EPSILON || Math.Abs(SternReferenceDraftInches - 0.0) > EPSILON)
            {
                double sternRefDraft = DockSideFreeboardAft -
                                       (BargeDepth * 12 - ((SternReferenceDraftFeet * 12) + SternReferenceDraftInches));

                
                TareAngleStern = ( Math.Atan(sternRefDraft/(BargeWidth*12))*RADIANINV ) * -1 ;
            }



        }

        private void CalculateBowReferenceAngleFunction()
        {


            if (Math.Abs(BowReferenceDraftFeet - 0.0) > EPSILON || Math.Abs(BowReferenceDraftInches - 0.0) > EPSILON)
            {
                double bowRefDraft = DockSideFreeboardForward -
                                     (BargeDepth * 12 - ((BowReferenceDraftFeet * 12) + BowReferenceDraftInches));


                TareAngleBow = (Math.Atan(bowRefDraft/(BargeWidth*12))*RADIANINV) * -1;
            }

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
                        new RelayCommand(param => StartTakingData(), param => CanTakeData()));
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

            }
        }

        private bool CanTakeData()
        {
            return  (Node1ModResult == Result.SUCCESS) && (! (ModBusIsOn || InStartupMode)); ;
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
            return ModBusIsOn || InStartupMode;
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
            return (TonsPerInch > 0) && (BargeDepth > 0);
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
            return (TonsPerInch > 0) && (BargeDepth > 0);
        }

        public void TakeLightDraftReading()
        {
            if (TonsPerInch <= 0)
            {
                MessageBox.Show ( "Please enter Tons Per Inch", DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Information );
                return;
            }
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

        #region Comports


        #endregion
    }

  

}
