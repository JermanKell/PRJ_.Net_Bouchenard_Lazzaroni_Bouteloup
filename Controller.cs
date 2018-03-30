using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Controller
    {
        public event EventHandler<MyEventArgs> sendMessageToView;

        public void parse()
        {
            MyEventArgs myArgs = new MyEventArgs();

            myArgs.message = "Benjamin ???";
            sendMessageToView(this, myArgs);

            myArgs.message = "Préjjeennttt !!";
            sendMessageToView(this, myArgs);

            myArgs.message = "Raphael ?? ";
            sendMessageToView(this, myArgs);

            myArgs.message = "Préjjeennttt !!";
            sendMessageToView(this, myArgs);

            myArgs.message = "Xavier !!";
            sendMessageToView(this, myArgs);

            myArgs.message = "Préjjeennttt !!";
            sendMessageToView(this, myArgs);
        }
    }

    public class MyEventArgs : EventArgs
    {
        // Ici je peux mettre tous les attributs que je veux (string, int, double, ....).
        // Un objet de cette classe est en paramètre de la méthode eventReceived de la vue.
        // La vue à donc accès à l'ensemble des attributs de cette classe à chaque fois qu'elle recoit un évenement de la part du controleur.

        public string message { get; set; }
        public double pourcentage { get; set; }
    }
}
