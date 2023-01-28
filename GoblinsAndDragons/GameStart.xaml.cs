using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Policy;

namespace GoblinsAndDragons
{
    /// <summary>
    /// Interaction logic for GameStart.xaml
    /// </summary>
    public partial class GameStart : Window
    {
        Random rnd = new Random(); // Pozivamo Random klasu da bi mogli da generisemo nasumicne brojeve
        SoundPlayer player = new SoundPlayer(Properties.Resources.CavesOfDawn); // Omogucavamo da mozemo da pustamo ambientan zvuk

        public int hpH { get; set; } = 25;
        public int apH { get; set; } = 25;
        public int dpH { get; set; } = 25;
        public int dmH { get; set; } = 25; // Javne zadate vrednosti heroja (i njihove pocetne vrednosti od 25)
                                           // koje mozemo da menjamo u Creator prozoru
        public int atvH; // Attack value heroja

        public int hpHs, apHs, dpHs, dmHs; // Vrednosti heroja koje su iste kao i pocetne ali ove sluze da pratimo unapredivanja (s pored hpHs oznacava sesiju)
        public int hpM, apM, dpM, dmM, atvM; // Health points, action points, defense points, damage points, attack value od cudovista
        public int hpB, apB, dpB, dmB, atvB; // Health points, action points, defense points, damage points, attack value od bossa
        public int dice, xp, side, encounter, boss, upstat, gold; // Dice, xp, strana, na sta smo naleteli, da li nalecemo na bossa, status za upgrejdovanje, koliko para ima heroj

        public GameStart()
        {
            InitializeComponent();
            moveForward.IsEnabled = false;
            moveLeft.IsEnabled = false;
            moveRight.IsEnabled = false;
            gameRestart.IsEnabled = false;
        }

        private void exitWindow_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            this.Close(); // Zatvorimo ovaj prozor
        }

        private void gameRestart_Click(object sender, RoutedEventArgs e)
        {
            player.Stop(); // Zaustavimo muziku

            imageEnemy.Source = null; // Uklonimo ikone oklopa, oruzja i neprijatelja
            imageChest.Source = null;
            imageLegs.Source = null;
            imageSword.Source = null;
            imageShield.Source = null;
            imageEquipmentRight.Source = new BitmapImage(new Uri(@"/Images/UI/HandRight.png", UriKind.Relative));
            imageEquipmentLeft.Source = new BitmapImage(new Uri(@"/Images/UI/HandLeft.png", UriKind.Relative));

            chestButton.IsEnabled = true; // Ponovo pustamo dugmice zakupovinu
            legsButton.IsEnabled = true;
            swordButton.IsEnabled = true;
            shieldButton.IsEnabled = true;

            moveForward.IsEnabled = false; // Gasimo dugmice za skretanje
            moveLeft.IsEnabled = false;
            moveRight.IsEnabled = false;
            gameRestart.IsEnabled = false;
            gameStart.IsEnabled = true;

            imageExplore.Source = new BitmapImage(new Uri(@"/Images/Path/Path.png", UriKind.Relative)); // Stavljamo pocetnu sliku

            displayText.Text = "Press Start Game to begin your jurney anew!"; // Praznimo TextBox i stavljamo nov tekst

            xp = 0; // Sve valute vracamo na nula
            side = 0;
            boss = 0;
            upstat = 0;
            gold = 0;

            currentHPValue.Text = Convert.ToString(hpH);
            currentAPValue.Text = Convert.ToString(apH);
            currentDPValue.Text = Convert.ToString(dpH);
            currentDMValue.Text = Convert.ToString(dmH);
            currentCoinsValue.Text = Convert.ToString(gold); // Vracamo sve vrednosti na njihove pocetne valute
        }

        private void gameStart_Click(object sender, RoutedEventArgs e)
        {
            moveForward.IsEnabled = true;
            moveLeft.IsEnabled = true;
            moveRight.IsEnabled = true;
            gameRestart.IsEnabled = true;
            gameStart.IsEnabled = false;

            xp = 0;
            side = 0;
            boss = 0;
            upstat = 0;
            gold = 0;

            currentHPValue.Text = Convert.ToString(hpH);
            currentAPValue.Text = Convert.ToString(apH);
            currentDPValue.Text = Convert.ToString(dpH);
            currentDMValue.Text = Convert.ToString(dmH);
            currentCoinsValue.Text = Convert.ToString(gold);

            // Deklarisemo da je s iteracija ista kao i pocetna
            hpHs = hpH;
            apHs = apH;
            dpHs = dpH;
            dmHs = dmH;

            // Deklarisemo random karakteristike cudovista
            hpM = rnd.Next(1, 26);
            apM = rnd.Next(1, 26);
            dpM = rnd.Next(1, 21);
            dmM = rnd.Next(1, 26);

            // Deklarisemo valute bossa
            hpB = 50;
            apB = 20;
            dpB = 20;
            dmB = 5;

            displayText.Text = "The hero has entered a cave and is faced with three paths. One going left the other right and the final one straight ahead.\n";

            imageEnemy.Source = null; // Uklonimo ikone oklopa, oruzja i neprijatelja ako slucajno nisu
            imageChest.Source = null;
            imageLegs.Source = null;
            imageSword.Source = null;
            imageShield.Source = null;
            
            player.Load();
            player.Play(); // Ucitavamo i pustamo zvuk
        }

        private void moveForward_Click(object sender, RoutedEventArgs e)
        {
            int hpHd = hpHs; // Zadajemo vrednosti heroja, cudovista i bosa u ovoj instanci
            int hpMd = hpM;
            int hpBd = hpB;

            Forward.startMoving(displayText, encounter, rnd, hpMd, hpHd, imageEnemy, dice, atvH, apHs, dpM, dmHs, atvM, apM, dpHs, dmM, gold, upstat, 
                    currentCoinsValue, lvlBox, shopBox, xp, side, imageExplore, moveForward, moveLeft, moveRight, gameRestart, gameStart, player);
        }

        private void moveLeft_Click(object sender, RoutedEventArgs e)
        {
            int hpHd = hpHs; // Zadajemo vrednosti heroja, cudovista i bosa u ovoj instanci
            int hpMd = hpM;
            int hpBd = hpB;

            displayText.AppendText("You turn left! \n");

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

            displayText.ScrollToEnd();

        }

        private void moveRight_Click(object sender, RoutedEventArgs e)
        {
            int hpHd = hpHs; // Zadajemo vrednosti heroja, cudovista i bosa u ovoj instanci
            int hpMd = hpM;
            int hpBd = hpB;

            boss++; // Dodajemo sansu da se pojavi boss
            

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

            displayText.ScrollToEnd();

        }

        private void lvlHpButton_Click(object sender, RoutedEventArgs e)
        {
            hpHs = hpHs + 5;
            currentHPValue.Text = Convert.ToString(hpHs);

            lvlBox.Visibility = Visibility.Collapsed;
        }

        private void lvlApButton_Click(object sender, RoutedEventArgs e)
        {
            apHs = apHs + 5;
            currentAPValue.Text = Convert.ToString(apHs);

            lvlBox.Visibility = Visibility.Collapsed;
        }

        private void lvlDpButton_Click(object sender, RoutedEventArgs e)
        {
            dpHs = dpHs + 5;
            currentDPValue.Text = Convert.ToString(dpHs);

            lvlBox.Visibility = Visibility.Collapsed;
        }

        private void lvlDmButton_Click(object sender, RoutedEventArgs e)
        {
            dmHs = dmHs + 5;
            currentDMValue.Text = Convert.ToString(dmHs);

            lvlBox.Visibility = Visibility.Collapsed;
        }

        private void chestButton_Click(object sender, RoutedEventArgs e)
        {
            if (gold > 100)
            {
                imageChest.Source = new BitmapImage(new Uri(@"/Images/Equipment/Chest.png", UriKind.Relative));
                gold = gold - 100;
                currentCoinsValue.Text = Convert.ToString(gold);
                hpHs = hpHs + 13;
                currentHPValue.Text = Convert.ToString(hpHs);

                chestButton.IsEnabled = false;
                shopBox.Visibility = Visibility.Collapsed;
            }
            else 
            {
                MessageBox.Show("You do not have enough gold to buy that item", "Insufficient funds!", MessageBoxButton.OK);
            }
        }

        private void legsButton_Click(object sender, RoutedEventArgs e)
        {
            if (gold > 100)
            {
                imageLegs.Source = new BitmapImage(new Uri(@"/Images/Equipment/Legs.png", UriKind.Relative));
                gold = gold - 100;
                currentCoinsValue.Text = Convert.ToString(gold);
                apHs = apHs + 9;
                currentAPValue.Text = Convert.ToString(apHs);

                legsButton.IsEnabled = false;
                shopBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You do not have enough gold to buy that item", "Insufficient funds!", MessageBoxButton.OK);
            }
        }

        private void swordButton_Click(object sender, RoutedEventArgs e)
        {
            if (gold > 50)
            {
                imageSword.Source = new BitmapImage(new Uri(@"/Images/Equipment/Sword.png", UriKind.Relative));
                imageEquipmentRight.Source = new BitmapImage(new Uri(@"/Images/UI/HandRightSword.png", UriKind.Relative));
                gold = gold - 50;
                currentCoinsValue.Text = Convert.ToString(gold);
                dmHs = dmHs + 8;
                currentDMValue.Text = Convert.ToString(dmHs);

                swordButton.IsEnabled = false;
                shopBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You do not have enough gold to buy that item", "Insufficient funds!", MessageBoxButton.OK);
            }
        }

        private void shieldButton_Click(object sender, RoutedEventArgs e)
        {
            if (gold > 50)
            {
                imageShield.Source = new BitmapImage(new Uri(@"/Images/Equipment/Shield.png", UriKind.Relative));
                imageEquipmentLeft.Source = new BitmapImage(new Uri(@"/Images/UI/HandLeftShield.png", UriKind.Relative));
                gold = gold - 50;
                currentCoinsValue.Text = Convert.ToString(gold);
                dpHs = dpHs + 11;
                currentDPValue.Text = Convert.ToString(dpHs);

                shieldButton.IsEnabled = false;
                shopBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("You do not have enough gold to buy that item", "Insufficient funds!", MessageBoxButton.OK);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            shopBox.Visibility = Visibility.Collapsed;
        }
    }
}
