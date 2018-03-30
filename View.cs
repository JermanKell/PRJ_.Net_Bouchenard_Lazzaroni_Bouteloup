using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class View
    {
        public void clickOnButtonIntegration()
        {
            Controller controller = new Controller();
            controller.sendMessageToView += eventReceived;
            controller.parse();
        }

        static void eventReceived(object sender, MyEventArgs e)
        {
            // Ici, on vient ajouter le message à la vue, mettre à jour le pourcentage, etc ....
            MessageBox.Show(e.message);
        }
    }
}