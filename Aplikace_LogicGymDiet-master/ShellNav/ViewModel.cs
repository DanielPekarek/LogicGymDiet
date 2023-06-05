using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellNav
{
    public class ViewModel
    {
        public List<Person> Data { get; set; }

        public ViewModel()
        {
            Data = new List<Person>()
            {
                new Person { Makroziviny = "Bílkoviny", Height = 170 },
                new Person { Makroziviny = "Vláknina", Height = 96 },
                new Person { Makroziviny = "Tuky", Height = 65 },
                new Person { Makroziviny = "Sacharidy", Height = 182 }
            }; 
        }
    }
}
