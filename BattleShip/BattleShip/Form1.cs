using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        PictureBox[,] player = new PictureBox[10, 10];
        PictureBox[,] ComputerBoard = new PictureBox[10, 10];
        HashSet<int> playerLocations = new HashSet<int>();
        HashSet<int> ComputerLocations = new HashSet<int>();
        List<int> ComputerListLocations = new List<int>();
        int humanShots = 0;
        int comShots = 0;
        int playerHitCount = 0;
        int comHitCount = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            printgame();
            RandomAssignment();
            dispShips();
            randTurn();

        }

        private void randTurn()
        {
            Random rnd = new Random();
            int check = rnd.Next(0, 2);
            if (check == 0)
            {
                MessageBox.Show("Player starts...");
            }
            else
            {
                MessageBox.Show("Computer Starts...");
                ComputerTurn();
            }
        }
        private void printgame(bool resetgame = false)
        {   //Player board

            int xpos = 45;
            int ypos = 45;
            for (int i = 0; i < player.GetLength(0); i++) // The squares of the field for Left Side
            {
                for (int j = 0; j < player.GetLength(1); j++)
                {
                    if (resetgame == true)
                    {
                        player[i, j].ImageLocation = @"..\net6.0-windows\water.jpg";
                        continue;
                    }
                    player[i, j] = new PictureBox
                    {
                        Name = "picturebox" + i.ToString(),
                        Location = new Point(xpos, ypos),
                        Size = new Size(43, 43),
                        Visible = true,
                        BackColor = Color.SkyBlue,
                        ImageLocation = @"..\net6.0-windows\water.jpg",
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    this.Controls.Add(player[i, j]);
                    xpos += 45;
                }
                xpos = 45;
                ypos += 45;

            }
            xpos = 545;
            ypos = 45;
            // Computer Opponant board
            for (int k = 0; k < ComputerBoard.GetLength(0); k++)    // The squares of the board for the Right side
            {

                for (int l = 0; l < ComputerBoard.GetLength(1); l++)
                {
                    if (resetgame == true)
                    {
                        ComputerBoard[k, l].ImageLocation = @"..\net6.0-windows\water.jpg";
                        continue;
                    }
                    ComputerBoard[k, l] = new PictureBox
                    {
                        Name = "picturebox" + k.ToString() + l.ToString(),
                        Location = new Point(xpos, ypos),
                        Size = new Size(43, 43),
                        Visible = true,
                        BackColor = Color.SkyBlue,
                        ImageLocation = @"..\net6.0-windows\water.jpg",
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    this.Controls.Add(ComputerBoard[k, l]);
                    ComputerBoard[k, l].Click += Player_Click;
                    xpos += 45;
                }

                xpos = 545;
                ypos += 45;

            }
        }


        private void dispShips() //Place ships randomly on both Player and Computer Sides
        {
            //computerhide();
            List<int> myplace = playerLocations.ToList();
            ComputerListLocations = ComputerLocations.ToList();
            //Player Ships
            player[myplace[0], myplace[1]].ImageLocation = @"..\net6.0-windows\boat.png";   //horizontally orientation
            player[myplace[2], myplace[3]].ImageLocation = @"..\net6.0-windows\boat2.png";  //vertical orientation
            player[myplace[4], myplace[5]].ImageLocation = @"..\net6.0-windows\boat.png";
            player[myplace[6], myplace[7]].ImageLocation = @"..\net6.0-windows\boat2.png";
            //Computer Ships
/*            for (int i = 0; i < 8; i++)
            {
                for (int r = 0; r < 8; r++)
                {

                    output.Text += $"{ComputerListLocations[i]},{ComputerListLocations[r]}\n";
                }
            }*/
            ComputerBoard[ComputerListLocations[0], ComputerListLocations[1]].ImageLocation = @"..\net6.0-windows\water.jpg";   //vertical orientation
            ComputerBoard[ComputerListLocations[2], ComputerListLocations[3]].ImageLocation = @"..\net6.0-windows\water.jpg";    //horizontally orientation
            ComputerBoard[ComputerListLocations[4], ComputerListLocations[5]].ImageLocation = @"..\net6.0-windows\water.jpg";
            ComputerBoard[ComputerListLocations[6], ComputerListLocations[7]].ImageLocation = @"..\net6.0-windows\water.jpg";
        }
        private void RandomAssignment() // Randomly assign locations of all ships
        {
            Random rnd = new Random();
            while (playerLocations.Count < 8)   //randomly assign locations for Player Ships
            {
                int shiplocation = rnd.Next(9);
                playerLocations.Add(shiplocation);
                playerLocations.Add(shiplocation + 1);
            }
            while (ComputerLocations.Count < 8)     //randomly assing locations for Computer Ships
            {
                int shiplocation = rnd.Next(9);
                ComputerLocations.Add(shiplocation);
                ComputerLocations.Add(shiplocation + 1);
            }
        }

        private void shipHit(int xAttack, int yAttack)
        {   // Handles marking any of computer ship kills
            ComputerBoard[xAttack, yAttack].ImageLocation = @"..\net6.0-windows\hit.png";
            ++playerHitCount;
            humanShots++;
            MessageBox.Show("You've sunk my Battleship!");
            if (WinCheck() == true) { return; }
        }

        private void Player_Click(object sender, EventArgs e)   // Player Click Event
        {

            PictureBox ClickedPictureBox = (PictureBox)sender;      // The Player Click Get
            int xAttack = Convert.ToInt32(ClickedPictureBox.Name.Substring(10, 1)); // x value
            int yAttack = Convert.ToInt32(ClickedPictureBox.Name.Substring(11));    // y value
            output.Text += $"Player shot({xAttack},{yAttack})\n";
            // Tests list locations for ships
            if (ComputerBoard[xAttack, yAttack] == ComputerBoard[ComputerListLocations[0], ComputerListLocations[1]])
            {
                shipHit(xAttack, yAttack);
            }
            else if (ComputerBoard[xAttack, yAttack] == ComputerBoard[ComputerListLocations[2], ComputerListLocations[3]])
            {
                shipHit(xAttack, yAttack);
            }
            else if (ComputerBoard[xAttack, yAttack] == ComputerBoard[ComputerListLocations[4], ComputerListLocations[5]]){
                shipHit(xAttack, yAttack);
            }
            else if (ComputerBoard[xAttack, yAttack] == ComputerBoard[ComputerListLocations[6], ComputerListLocations[7]])
            {
                shipHit(xAttack, yAttack);
            }
            else if
                (ComputerBoard[xAttack, yAttack].ImageLocation == @"..\net6.0-windows\water.jpg")
            {
                ComputerBoard[xAttack, yAttack].ImageLocation = @"..\net6.0-windows\miss.png";
            }
            else
            {   // Huh? What happened?
                MessageBox.Show("Whoopsie, try again...");
                return;
            }
            humanShots++;
            ComputerTurn(); //Computer's turn
        }

        private bool WinCheck()
        {

            if (playerHitCount == 4)
            {
                MessageBox.Show("Player Wins!");
                resetGame(); //not sure if this will work but hey trying better than not
                return (true);
            }
            if (comHitCount == 4)
            {
                MessageBox.Show("Computer Wins!");
                resetGame();
                return (true);
            }
            if (humanShots == 25 && playerHitCount > 0 && comHitCount == 0)
            {
                MessageBox.Show("Did we remember to plug in the computer?");
                resetGame();
            }
            return (false);
        }

        private void ComputerTurn()
        {
            Random rand = new Random();
            int xAttack = rand.Next(10);
            int yAttack = rand.Next(10);
            MessageBox.Show("Computer Attacks " + xAttack.ToString());
            if (player[xAttack, yAttack].ImageLocation == @"..\net6.0-windows\boat.png")
            {
                player[xAttack, yAttack].ImageLocation = @"..\net6.0-windows\hit.png";
                comHitCount++;
                MessageBox.Show("Computer sunk my Battleship!");
                WinCheck();
            }
            else if (player[xAttack, yAttack].ImageLocation == @"..\net6.0-windows\boat2.png")
            {
                player[xAttack, yAttack].ImageLocation = @"..\net6.0-windows\hit.png";
                comHitCount++;
                MessageBox.Show("Computer sunk my Battleship!");
                WinCheck();
            }
            else if
                (player[xAttack, yAttack].ImageLocation == @"..\net6.0-windows\water.jpg")
            {
                player[xAttack, yAttack].ImageLocation = @"..\net6.0-windows\miss.png";
            }
            else
            {
                //ComputerTurn();
            }
            comShots++;
        }

        private void resetGame()
        {

            playerLocations = new HashSet<int>();
            ComputerLocations = new HashSet<int>();
            RandomAssignment();
            printgame(true);
            dispShips();
            playerHitCount = 0;
            comHitCount = 0;
            humanShots = 0;
            comShots = 0;
            return;
        }

    }
}