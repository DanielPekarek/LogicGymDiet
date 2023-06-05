using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShellNav
{
    public class SportyDataClass
    {
        public class Sport : INotifyPropertyChanged
        {
            private string _name;
            private int _cas;
            private int _spaleni;

            public string Name
            {
                get { return _name; }
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Cas
            {
                get { return _cas; }
                set
                {
                    if (_cas != value)
                    {
                        _cas = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Spaleni
            {
                get { return _spaleni; }
                set
                {
                    if (_spaleni != value)
                    {
                        _spaleni = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static ObservableCollection<Sport> SportList = new ObservableCollection<Sport>();
    }
}
