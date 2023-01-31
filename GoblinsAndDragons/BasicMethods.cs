using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace GoblinsAndDragons
{
    internal class BasicMethods
    {
        public static int Battle(TextBox displayText, Image imageEnemy,Random rnd, int hpHd, int atvH, int apHs, int dmHs, int dpHs, 
                int hpMd, int atvM, int apM, int dmM, int dpM, int dice)
        {
            while (hpMd > 0 && hpHd > 0)
            {
                displayText.AppendText("You ran into an enemy! \n");
                imageEnemy.Source = new BitmapImage(new Uri(@"/Images/Enemy/Mage.png", UriKind.Relative));
                bool attacker = rnd.Next(2) == 1; // Odredjujemo ko prvi igra

                if (attacker)
                {
                    displayText.AppendText("You gain the upper hand and attack the enemy first. \n");
                    dice = rnd.Next(1, 7) + rnd.Next(1, 7); // Bacamo kocku
                    atvH = apHs + dice; // Odredjujemo attack value od napadaca
                    displayText.AppendText("You rolled a " + dice + "\n");
                    displayText.AppendText("Your attack value is " + atvH + "\n");

                    if (atvH > dpM)
                    {
                        displayText.AppendText("You hit the monster! \n");
                        hpMd = hpMd - dmHs; // Ako je atv veci od dp onda se oduzima hp od osobe koja je napadnuta
                        displayText.AppendText("The monsters remaining HP is " + hpMd + "\n");
                    }
                    else
                    {
                        displayText.AppendText("You missed. \n");
                    }

                }

                else
                {
                    displayText.AppendText("The monster was ready for your attack it manages to attack first. \n");
                    dice = rnd.Next(1, 7) + rnd.Next(1, 7); // Sve isto kao if samo je vazi za cudoviste
                    atvM = apM + dice;
                    displayText.AppendText("The monster rolled a " + dice + "\n");
                    displayText.AppendText("The monsters attack value is " + atvM + "\n");

                    if (atvM > dpHs)
                    {
                        displayText.AppendText("The monster hit the hero! \n");
                        hpHd = hpHd - dmM;
                        displayText.AppendText("The hero's remaining HP is " + hpHd + "\n");
                    }
                    else
                    {
                        displayText.AppendText("The monster missed. \n");
                    }

                }

            }
            return hpHd;
        }

        public static int BossBattle(TextBox displayText, Image imageEnemy, Random rnd, int hpHd, int atvH, int apHs, int dmHs, int dpHs,
                int hpBd, int atvB, int apB, int dmB, int dpB, int dice)
        {
            while (hpBd > 0 && hpHd > 0)
            {
                imageEnemy.Source = new BitmapImage(new Uri(@"/Images/Enemy/Boss.png", UriKind.Relative));
                bool attacker = rnd.Next(2) == 1; // Odredjujemo ko prvi igra

                if (attacker)
                {
                    displayText.AppendText("You gain the upper hand and attack the boss first. \n");
                    dice = rnd.Next(1, 7) + rnd.Next(1, 7); // Bacamo kocku
                    atvH = apHs + dice; // Odredjujemo attack value od napadaca
                    displayText.AppendText("You rolled a " + dice + "\n");
                    displayText.AppendText("Your attack value is " + atvH + "\n");

                    if (atvH > dpB)
                    {
                        displayText.AppendText("You hit the boss! \n");
                        hpBd = hpBd - dmHs; // Ako je atv veci od dp onda se oduzima hp od osobe koja je napadnuta
                        displayText.AppendText("The bosses remaining HP is " + hpBd + "\n");
                    }
                    else
                    {
                        displayText.AppendText("You missed. \n");
                    }

                }
                else
                {
                    displayText.AppendText("The boss was ready for your attack it manages to attack first. \n");
                    dice = rnd.Next(1, 7) + rnd.Next(1, 7); // Sve isto kao if samo vazi za bossa
                    atvB = apB + dice;
                    displayText.AppendText("The boss rolled a " + dice + "\n");
                    displayText.AppendText("The bosses attack value is " + atvB + "\n");

                    if (atvB > dpHs)
                    {
                        displayText.AppendText("The boss hit the hero! \n");
                        hpHd = hpHd - dmB;
                        displayText.AppendText("The hero's remaining HP is " + hpHd + "\n");
                    }
                    else
                    {
                        displayText.AppendText("The boss missed. \n");
                    }

                }

            }
            return hpHd;
        }
    }
}
