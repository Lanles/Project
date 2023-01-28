﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Media;
using System.Windows.Controls;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace GoblinsAndDragons
{
    internal class MoveRight
    {
        public static void StartRight(TextBox displayText, int encounter, Random rnd, int hpMd, int hpHd, Image imageEnemy,
                int dice, int atvH, int apHs, int dpM, int dmHs, int atvM, int apM, int dpHs, int dmM, int gold, int upstat, TextBlock currentCoinsValue,
                Grid lvlBox, Grid shopBox, int xp, int side, Image imageExplore, Button moveForward, Button moveLeft, Button moveRight, Button gameRestart, Button gameStart,
                SoundPlayer player, int boss, int hpBd, int dpB, int atvB, int apB, int dmB)
        {
            GameStart game = new GameStart();

            displayText.AppendText("You turn right! \n");

            if (boss == 6)
            {
                displayText.AppendText("You have found the boss of this dungeon! \n");

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

                if (hpHd > 0)
                {
                    player.Stop();
                    displayText.AppendText("The hero won and defeated the boss! You can now look at your playthrough. \n");

                    moveForward.IsEnabled = false; // Gasimo dugmice za skretanje
                    moveLeft.IsEnabled = false;
                    moveRight.IsEnabled = false;
                    gameRestart.IsEnabled = true;
                    gameStart.IsEnabled = false;
                }
                else
                {
                    player.Stop();
                    displayText.AppendText("The boss has defeated the hero! Better luck next time. \n");

                    moveForward.IsEnabled = false; // Gasimo dugmice za skretanje
                    moveLeft.IsEnabled = false;
                    moveRight.IsEnabled = false;
                    gameRestart.IsEnabled = true;
                    gameStart.IsEnabled = false;
                }

            }
            else
            {
                encounter = rnd.Next(3); // Nasumicno biramo sta ce heroj da sretne (pod nulom je bitka, pod jedan je kovceg a pod tri je trgovac)

                switch (encounter)
                {
                    case 0:
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
                                displayText.AppendText("The enemy was ready for your attack it manages to attack first. \n");
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
                        break;

                    case 1:
                        imageEnemy.Source = null;
                        displayText.AppendText("You have found a chest with enough XP to level up, and some coins in it! \n");
                        gold = gold + rnd.Next(50, 151);
                        currentCoinsValue.Text = Convert.ToString(gold);
                        upstat = 1;
                        displayText.AppendText("Choose what you want to level up by five points. \n");
                        lvlBox.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        imageEnemy.Source = new BitmapImage(new Uri(@"/Images/Enemy/Merchant.png", UriKind.Relative));
                        displayText.AppendText("You seem to have encountered a merchant, and he seems to be offering you some items to buy. \n");
                        upstat = 1;
                        displayText.AppendText("Merchant: Hello there i have some items you might want to buy. \n");
                        shopBox.Visibility = Visibility.Visible;
                        break;
                }



                // Proglasavamo ko je pobedio
                if (hpHd > 0 && upstat == 0)
                {
                    displayText.AppendText("The hero won! \n"); // Ako je heroj pobedio dajemo mu nasumicnu valutu novcica i jedan xp poen
                    displayText.AppendText("You found some coins! \n");
                    gold = gold + rnd.Next(50, 151);
                    currentCoinsValue.Text = Convert.ToString(gold);
                    xp++;

                    if (xp == 2) // Ako heroj ima vise od 2 xp poena moze da unapredi neke njegove sposobnosti
                    {
                        displayText.AppendText("You have enough XP to level up! \n");
                        xp = 0;
                        displayText.AppendText("Choose what you want to level up by five points. \n");
                        lvlBox.Visibility = Visibility.Visible;
                    }

                    side = rnd.Next(4); // Nasumicno prikazujemo sledece opcije kuda heroj moze da ide

                    switch (side)
                    {
                        case 0:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = true;
                            break;

                        case 1:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path1.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = false;
                            moveRight.IsEnabled = true;
                            break;

                        case 2:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path2.png", UriKind.Relative));
                            moveForward.IsEnabled = false;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = true;
                            break;

                        case 3:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path3.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = false;
                            break;
                    }
                }
                else if (hpHd > 0 && upstat == 1)
                {
                    displayText.AppendText("You continue forward. \n"); // Ako heroj nije naleteo na cudoviste pisemo da je samo nastavio dalje
                    upstat = 0;

                    side = rnd.Next(4); // Nasumicno prikazujemo sledece opcije kuda heroj moze da ide

                    switch (side)
                    {
                        case 0:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = true;
                            break;

                        case 1:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path1.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = false;
                            moveRight.IsEnabled = true;
                            break;

                        case 2:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path2.png", UriKind.Relative));
                            moveForward.IsEnabled = false;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = true;
                            break;

                        case 3:
                            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path3.png", UriKind.Relative));
                            moveForward.IsEnabled = true;
                            moveLeft.IsEnabled = true;
                            moveRight.IsEnabled = false;
                            break;
                    }
                }
                else
                {
                    player.Stop();
                    displayText.AppendText("The monster won! Better luck next time. \n"); // Ako je heroj izgubio gasimo sve dugmice sem restarta
                    moveForward.IsEnabled = false;
                    moveLeft.IsEnabled = false;
                    moveRight.IsEnabled = false;
                    gameRestart.IsEnabled = true;
                    gameStart.IsEnabled = false;
                }
            }

            game.gold = gold;

            displayText.ScrollToEnd();
        }
    }
}
