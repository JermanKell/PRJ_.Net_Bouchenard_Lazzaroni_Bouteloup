using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Four king of messages can be used for logs printing
    /// </summary>
    enum TypeMessage
    {
        Succès,
        Avertissement,
        Erreur,
        Critique
    }

    /// <summary>
    /// Type of messages used for logs printing
    /// </summary>
    enum SubjectMessage
    {
        Structure_XML,

        Erreur_orthographe,
        Mauvaise_information,

        Ajouter_article,
        Modifier_article,

        Ajouter_famille,
        Modifier_famille,

        Ajouter_sous_famille,
        Modifier_sous_famille,

        Ajouter_marque,
        Modifier_marque,

        Terminé
    }
}
