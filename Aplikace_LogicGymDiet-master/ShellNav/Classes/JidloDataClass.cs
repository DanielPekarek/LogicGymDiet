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
    public class JidloDataClass
    {
        public class Jidlo : INotifyPropertyChanged
        {
            private string _name;
            private int _kalorie;
            private int _bilkoviny;
            private int _vlaknina;
            private int _tuky;
            private int _sacharidy;

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

            public int Kalorie
            {
                get { return _kalorie; }
                set
                {
                    if (_kalorie != value)
                    {
                        _kalorie = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Bilkoviny
            {
                get { return _bilkoviny; }
                set
                {
                    if (_bilkoviny != value)
                    {
                        _bilkoviny = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Vlaknina
            {
                get { return _vlaknina; }
                set
                {
                    if (_vlaknina != value)
                    {
                        _vlaknina = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Tuky
            {
                get { return _tuky; }
                set
                {
                    if (_tuky != value)
                    {
                        _tuky = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int Sacharidy
            {
                get { return _sacharidy; }
                set
                {
                    if (_sacharidy != value)
                    {
                        _sacharidy = value;
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

        public static ObservableCollection<Jidlo> FoodList = new ObservableCollection<Jidlo>();
    }
}
