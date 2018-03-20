using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class ControllerParserXMLUpdate : ControllerParserXML
    {
        public ControllerParserXMLUpdate(string filename) : base(filename)
        { }

        public override void parse()
        {
            verifyFile();
        }
    }
}
