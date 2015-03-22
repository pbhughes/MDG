﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using MDG.Model;
using MDG.Conversions;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for DraftMeasurmentBox.xaml
    /// </summary>
    public partial class DraftMeasurmentBox : INotifyPropertyChanged
    {
        private static int _bargeDepth = 0;
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
            DependencyProperty.Register("RawReading", typeof(double), typeof(DraftMeasurmentBox), new PropertyMetadata(0D, OnReadingChanged));


    
        private static void OnReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var xd = (DraftMeasurmentBox) d;
           
            var dNewvalue = (double) e.NewValue;

            xd.txtFreeboard.Text = dNewvalue.ToFeetAndInches();

            xd.txtDraft.Text = ( _bargeDepth*12 - dNewvalue ).ToFeetAndInches();

        }

        
        #endregion

        #region Barge Depth Property

        public static readonly DependencyProperty BargeDepthProperty =
            DependencyProperty.Register("BargeDepth", typeof(int), typeof(DraftMeasurmentBox), new PropertyMetadata(0, OnDepthChanged));

        public int BargeDepth
        {
            get { return (int) GetValue(BargeDepthProperty); }
            set { SetValue(BargeDepthProperty, value); }
        }

        private static void OnDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var xd = (DraftMeasurmentBox)d;
            var dNewvalue = (int)e.NewValue;
            _bargeDepth = dNewvalue;
          
        }

 

        #endregion
        #region Helper Functions

        #endregion

        public DraftMeasurmentBox()
        {
            InitializeComponent();
         
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
