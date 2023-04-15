using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Obesenec
{
    
    public class Hra
    {


        //nahodne uzite pri losovani slova
        private Random random;

        //seznam slov načtený ze souboru + defaultní slovo když soubor neexistuje
        private List<string> seznamSlov;

        //slovo které se bude hledat při aktuální hře
        public string HledaneSlovo { get; private set; }

        //slovo které se bude hledat při aktuální hře rozdělené na jednotlivé znaky do pole (pismeno + mezera)
        private List<string> HledaneSlovoPole;

        //počítadlo chyb a stavu šibenice
        public int score { get; private set; }




        /// <summary>
        /// nasvaví hru na počáteční hodnoty a načte slova ke hledání ze souboru
        /// </summary>
        public Hra()
        {
            score = 0;
            seznamSlov = new List<string>() { "ukulele" };
            random = new Random();
            HledaneSlovoPole = new List<string>();

            if (File.Exists("slova.txt"))
            {

                using (StreamReader sr = new("slova.txt"))
                {
                    string nacteneSlovo = "";
                    while ((nacteneSlovo = sr.ReadLine()) != null)
                        seznamSlov.Add(nacteneSlovo.Trim().ToLower());
                }

                //když načte alespon jedno slovo ze souboru tak smaže defaultní slovo
                if (seznamSlov.Count > 1)
                    seznamSlov.RemoveAt(0);
            }
        }

        /// <summary>
        /// zahájí / restartuje hru
        /// </summary>
        /// <returns>vrátí počateční slovo převedené na znaky "_"</returns>
        public string NovaHra()
        {
            //vybere nahodne slovo ze seznamu
            HledaneSlovo = seznamSlov[random.Next(0, seznamSlov.Count)];

            //převede slovo do pole pismen + za každé písmeno dá mezeru
            for (int i = 0; i < HledaneSlovo.Length; i++)
            {
                HledaneSlovoPole.Add(HledaneSlovo[i].ToString());
                HledaneSlovoPole.Add(" ");
            }

            // vytvoří string stejně dlouhý jako slovo ale míto písmen nastaví "_"
            var pole = Enumerable.Repeat("_ ", HledaneSlovo.Length);
            string pocatecniSkryteSlovo = String.Join("", pole);

            return pocatecniSkryteSlovo; 
        }


        /// <summary>
        /// kontrola jestli se zadané pismeno vyskytuje v hledaném slově
        /// </summary>
        /// <param name="pismeno">vybrané stisknuté písmeno</param>
        /// <param name="aktualniText">asktuální stav hádaného slova zobrazený na obrazovce</param>
        /// <returns>vrátí string se zobrazenými uhádnutými písmeny </returns>
        public string Zkontroluj(string pismeno, string aktualniText)
        {
            List<int> indexy = new List<int>();
            string[] vracenePole = aktualniText.Select(a => a.ToString()).ToArray(); 
           

            //když hledané slovo obsahuje stisknuté písmeno
            if (HledaneSlovoPole.Contains(pismeno))         
            {
                //najde index/indexy písmene v hledaném slově
                for (int i = 0; i < HledaneSlovoPole.Count; i++)
                {
                    if (HledaneSlovoPole[i] == pismeno)
                        indexy.Add(i);
                }

                // zamění znaky na indexech za písmena
                for (int i = 0; i < indexy.Count; i++)
                {
                    vracenePole[indexy[i]] = pismeno;
                }

            }
            else
                score++;

            string vraceneSlovo = String.Join("", vracenePole);
            return vraceneSlovo;

        }

    }
}
