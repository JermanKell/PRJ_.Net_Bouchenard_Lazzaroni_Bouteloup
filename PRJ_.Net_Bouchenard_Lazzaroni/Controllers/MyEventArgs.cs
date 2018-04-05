using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class MyEventArgs : EventArgs
    {
        public string message { get; set; } // The message log
        public SubjectMessage subject { get; set; } // The subject of the message (addArticle, updateArticle, ...)
        public TypeMessage type { get; set; } // The type of the message (success, warning, error, ...)
        public int maxRange { get; set; } // The max range of the progress bar (correspond to the number of node in the XML file)
    }
}
