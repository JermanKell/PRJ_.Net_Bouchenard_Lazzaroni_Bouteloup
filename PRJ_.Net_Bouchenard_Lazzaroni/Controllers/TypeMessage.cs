using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    enum TypeMessage
    {
        Success,
        Warning,
        Error,
        Critical
    }

    enum SubjectMessage
    {
        Xml_Structure,

        Spelling_Mistake,
        Wrong_Information,

        Add_Article,
        Update_Article,

        Add_Famille,
        Update_Famille,

        Add_SousFamille,
        Update_SousFamille,

        Add_Marque,
        Update_Marque,

        Finish
    }
}
