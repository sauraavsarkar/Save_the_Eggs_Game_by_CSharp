using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_the_Eggs_Game
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight;

        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox splash = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Saved: " + score; // show the score on Eggs Caught label
            txtMiss.Text = "Missed: " + missed; // Show the misses on Eggs Missed label

            // if the go left boolean is true AND chickens left is greater than 0
            if (goLeft == true && player.Left > 0)
            {
                // then we move the chicken 12 pixels to the left
                player.Left -= 12;
                //checken image will be change to the one moving left
                player.Image = Properties.Resources.chicken_normal2;
            }
            //if the go right is true AND chickens width and left is less than form width
            // meaning the chicken is still within the frame of the game
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                // move the chicken 12 pixels to the right
                player.Left += 12;
                // change the chicken image to the one moving right
                player.Image = Properties.Resources.chicken_normal;

            }

            foreach (Control X in this.Controls)
            {
                // if X is a type of picture box AND it has the tag of Eggs
                if (X is PictureBox && (string)X.Tag == "eggs")

                {
                    // then move X towards the bottom
                    X.Top += speed;

                    // if the EGGS [X] reaches bottom of the screen
                    if (X.Top + X.Height > this.ClientSize.Height)
                    {
                        // if the egg hit the floor then we show the splash image
                        splash.Image = Properties.Resources.splash; // set the splash image
                        splash.Location = X.Location; // splash shows up on the same location as the egg
                        splash.Height = 60; // set the height
                        splash.Width = 60; // set the width
                        splash.BackColor = System.Drawing.Color.Transparent; // apply transparent background to the picture box

                        this.Controls.Add(splash); // add the splash picture to the form

                        X.Top = randY.Next(80, 300) * -1; // position the eggs to a random Y location
                        X.Left = randX.Next(5, this.ClientSize.Width - X.Width); // position the eggs to a random X location
                        missed++; // add 1 to the missed integer
                        player.Image = Properties.Resources.chicken_hurt; // change the chicken image to hurt image

                    }

                    if (player.Bounds.IntersectsWith(X.Bounds))
                    {
                        X.Top = randY.Next(80, 300) * -1; // position the eggs to a random Y location
                        X.Left = randX.Next(5, this.ClientSize.Width - X.Width); // position the eggs to a random X location
                        score++; // add 1 to the score
                    }


                }

            }

            if (score >= 20)
            {
                speed = 10; // increase the eggs speed to 20
            }

            // if the missed number is greater than 5
            // we need to stop the game
            if (missed > 5)
            {
                GameTimer.Stop(); // stop the game timer
                                  // show the message box to say game is over. 
                MessageBox.Show("Game Over!! We lost good Eggs" + "\r\n" + "Click OK to restart");
                // once the players clicks OK we restart the game again
                RestartGame();
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                // if the left key is pressed change the go left to true
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                // if the right key is pressed change the go right to true
                goRight = true;
            }
        }

        private void KeyIsup(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                // if the left key is up then change the go left to false
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                // if the right key is up then change the go right to false
                goRight = false;
            }
        }


        private void RestartGame()
        {
            foreach (Control X in this.Controls)
            {

                if (X is PictureBox && (string)X.Tag == "eggs")
                {
                    // we move it to top of the screen
                    X.Top = randY.Next(80, 300) * -1; // give it a random y location
                    X.Left = randX.Next(5, this.ClientSize.Width - X.Width); // give it a random x location
                }
            }


            player.Left = this.ClientSize.Width / 2; // reset the chicken to middle of the form
            player.Image = Properties.Resources.chicken_normal2; // change the chicken picture to face left

            score = 0; // set score to 0
            missed = 0; // set missed to 0
            speed = 8; // set speed to 8

            goLeft = false; // set go left to false
            goRight = false; // set go right to false


            GameTimer.Start(); // start the game timer


        }
    }

}
