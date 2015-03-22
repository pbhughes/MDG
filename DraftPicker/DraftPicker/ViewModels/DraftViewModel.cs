using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DraftPicker.ViewModels
{
    public class DraftViewModel:INotifyPropertyChanged
    {
        private int _feet = 0;
        private int Feet
        {
            get { return _feet; }
            set
            {
                _feet = value;
                OnPropertyChanged("Feet");
                
            }
        }

        private int _inches = 0;
        private int Inches
        {
            get { return _inches; }
            set
            {
                _inches = value;
                OnPropertyChanged("Inches");
                
            }
        }

        public int Draft
        {
            get { return (_feet*12) + _inches; }
            set {
                _feet = value / 12;
                _inches = value % 12;
            }
        }

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
    }
}
