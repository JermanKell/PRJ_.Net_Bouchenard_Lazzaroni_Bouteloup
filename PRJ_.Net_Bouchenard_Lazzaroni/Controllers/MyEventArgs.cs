using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Use to store all args of event send by controller to parse the xml file
    /// </summary>
    class MyEventArgs : EventArgs
    {
        public string Message { get; set; } // The message log
        public SubjectMessage Subject { get; set; } // The subject of the message (addArticle, updateArticle, ...)
        public TypeMessage Type { get; set; } // The type of the message (success, warning, error, ...)
        public int MaxRange { get; set; } // The max range of the progress bar (correspond to the number of node in the XML file)
    }
}
