﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MDG.Model;
using MDG.Conversions;

namespace MDG.Visuals
{
    public delegate void DeckPlateChangedDelegate(double newValue);
    /// <summary>
    /// Interaction logic for DraftMeasurmentBox.xaml
    /// </summary>
    /// 
    /// 
    public partial class PrimaryDraftMeasurementBox :  UserControl
    {
        private static int _bargeDepth = 0;
        private static double _deckPlate = 0;
        private static string _nodeName = string.Empty;

        #region Reading Property

        public double RawReading
        {
            get { return (double)GetValue(VeryRawReadingProperty); }
            set
            {
                SetValue(VeryRawReadingProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for VeryRawReading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VeryRawReadingProperty =
            DependencyProperty.Register("RawReading", typeof(double), typeof(PrimaryDraftMeasurementBox), new PropertyMetadata(0D, OnReadingChanged));


    
        private static void OnReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var xd = (PrimaryDraftMeasurementBox) d;
           
            var dNewvalue = (double) e.NewValue;

            xd.txtFreeboard.Text = dNewvalue.ToFeetAndInches();

            xd.txtDraft.Text = ( _bargeDepth*12 - dNewvalue ).ToFeetAndInches();

        }

        
        #endregion

        #region Barge Depth Property

   
        public static readonly DependencyProperty BargeDepthProperty =
            DependencyProperty.Register("BargeDepth", typeof(int), typeof(PrimaryDraftMeasurementBox), new PropertyMetadata(0, OnDepthChanged));

        public int BargeDepth
        {
            get { return (int) GetValue(BargeDepthProperty); }
            set { SetValue(BargeDepthProperty, value); }
        }

        private static void OnDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dNewvalue = (int)e.NewValue;
            _bargeDepth = dNewvalue;
          
        }

        #endregion

        #region Events

        public event DeckPlateChangedDelegate DeckPlateChanged;

        #endregion

        #region DeckPlate Property

        public static readonly DependencyProperty DeckPlateProperty =
            DependencyProperty.Register ( "DeckPlate", typeof ( double ), typeof ( PrimaryDraftMeasurementBox ), new PropertyMetadata ( 0.0, OnDeckPlateChanged ) );

        public double DeckPlate
        {
            get { return (double)GetValue ( DeckPlateProperty ); }
            set { SetValue ( DeckPlateProperty, value ); }
        }

        private static void OnDeckPlateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var newValue = (double)e.NewValue;
            _deckPlate = newValue;
        }

       
        #endregion

        #region NodeName Property

        private string _nodeNameLocal = "Node Name";
        public string NodeNameLocal
        {
            get { return _nodeName; }
            set { _nodeName = value;
                OnPropertyChanged ("NodeNameLocal");
            }
        }

        public static readonly DependencyProperty NodeNameProperty =
            DependencyProperty.Register("NodeName", typeof (string), typeof (PrimaryDraftMeasurementBox), new PropertyMetadata("Node Name", OnNodeNameChanged));

        public string NodeName
        {
            get { return (string) GetValue(NodeNameProperty); }
            set { SetValue(NodeNameProperty, value); }
        }

        private static void OnNodeNameChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var newValue = (string)e.NewValue;
            var xd = (PrimaryDraftMeasurementBox)d;
            xd.txtNodeName.Text = newValue;
        }


        #endregion

        #region Helper Functions

        #endregion

        #region Constructions / Destruction

        public PrimaryDraftMeasurementBox ( )
        {
            InitializeComponent ( );
           
        }

        #endregion
     
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged ( string name )
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler ( this, new PropertyChangedEventArgs ( name ) );
            }
        }

        #endregion

        #region Event Handlers

        private void cboDeckPlate_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var value = (double)cboDeckPlate.SelectedValue;
            if (DeckPlateChanged != null)
                DeckPlateChanged ( value );
        }

        #endregion
       

    }
}
