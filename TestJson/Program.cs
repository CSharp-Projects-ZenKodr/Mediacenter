using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesMediaCenter;

namespace TestJson
{
    class Program
    {
        static void Main(string[] args)
        {
            IMDBRequestProxy im = new IMDBRequestProxy();

            int choix = -1;

            while (choix != 350)
            {
                string json = im.GetInformation("Transformers").Result.ToString();
                //Création d'un média support.
                Media m = new Media("Transformers", false, true, 12);

                //Récupération des informations concernant ce média grâce à son titre.
                RootObject[] ro = JSonToObject.JSonToRootObject(json);



                //Affichage des résultats.
                int i = 0;
                foreach (RootObject r in ro)
                {
                    Console.WriteLine(i + ". " + r.title);
                    i++;
                }

                //Choix des résultats
                while (choix < 0 || choix > ro.Length - 1)
                {
                    Console.WriteLine("Choisissez les informations qui correspondent à votre médias. (entre 0 et " + (ro.Length - 1) + ") :");
                    string choixstr = Console.ReadLine();
                    if (choixstr != "")
                        choix = int.Parse(choixstr);
                }

                //Remplissage du média avec les informations choisies 
                JSonToObject.RootObjectToMedia(ro[choix], m);

                //Affichage du média
                Console.WriteLine(m.ToString());
                choix = Console.Read();
            }
           
        }
    }
}
