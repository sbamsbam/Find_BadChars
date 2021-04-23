using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_BadChars
{
    public class Storage : ObservableObject
    {
        private string _debugOutput;

        public string DebugOutput
        {
            get { return _debugOutput; }
            set { OnPropertyChanged(ref _debugOutput, value); }
        }

        private string _badChars;

        public string BadChars
        {
            get { return _badChars; }
            set { OnPropertyChanged(ref _badChars, value); }    
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { OnPropertyChanged(ref _error, value); }
        }

        private string _selectedFile;

        public string SelectedFile
        {
            get { return _selectedFile; }
            set { OnPropertyChanged(ref _selectedFile, value); }    
        }

    }
}
