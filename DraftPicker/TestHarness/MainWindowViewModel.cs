using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestHarness
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        private int _inchesOfDraft = 0;
        public int DraftInches {
            get { return _inchesOfDraft; }
            set { _inchesOfDraft = value; OnPropertyChanged("DraftInches"); }
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
