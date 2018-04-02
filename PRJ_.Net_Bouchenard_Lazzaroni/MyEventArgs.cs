using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class MyEventArgs : EventArgs
    {
        public string message { get; set; }
        public SubjectMessage subject { get; set; }
        public TypeMessage type { get; set; }
    }
}
