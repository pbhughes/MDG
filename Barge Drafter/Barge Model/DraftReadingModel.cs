using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace MDG.Model
{
    public class DraftReadingModel:INotifyPropertyChanged
    {
        private double _aboveWaterReading;
        public double AboveWaterReading
        {
            get { return _aboveWaterReading; }
            set
            {
                _aboveWaterReading = value;
                OnPropertyChanged("AboveWaterReading");
            }
        }

        private double _belowWaterReading;
        public double BelowWaterReading
        {
            get { return _belowWaterReading; }
            set
            {
                _belowWaterReading = value;
                OnPropertyChanged("BelowWaterReading");
            }
        }

        private Visibility _showAboveWaterReading;
        public Visibility ShowAboveWaterReading
        {
            get { return _showAboveWaterReading; }
            set { 
                _showAboveWaterReading = value;
                OnPropertyChanged("ShowAboveWaterReading");
            
            }
        }

        private Visibility _showBelowWaterReading;
        public Visibility ShowBelowWaterReading
        {
            get { return _showBelowWaterReading; }
            set
            {
                _showBelowWaterReading = value;
                OnPropertyChanged("ShowBelowWaterReading");
            }
        }

        public double BargeDepth
        {
            get { return _bargeDepth; }
            set
            {
                _bargeDepth = value;
                OnPropertyChanged("BargeDepth");
            }
        }

        private double _bargeDepth;


     
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

    }
}
