using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Side_scroll
{
    public partial class Form1 : Form
    {
        bool left = false;
        bool right = false;
        bool jump = false;
        bool hasKey = false;

        int playerforce = 8;
        int playerjump = 10;
        int playerscore = 0;
        int playerspeed = 5;
        int bgspeed = 8;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void MainTimerEvent(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + playerscore;
            lblScore.Left = 536;
            player.Top += playerjump;

            if (left == true && player.Left > 60)
            {
                player.Left -= playerspeed;
            }
            if (right == true && player.Left + (player.Width + 60) < this.ClientSize.Width)
            {
                player.Left += playerspeed;
            }

            if (left == true && background.Left > 60)
            {
                background.Left += bgspeed;
                MoveGameElements("forward");
            }
            if (right == true && background.Left < -1732)
            {
                background.Left -= bgspeed;
                MoveGameElements("back");
            }
            if (jump == true)
            {
                playerjump = -12;
                playerforce -= 1;
            }
            else
            {
                playerjump = 12;
            }
            if (jump == true && playerforce > 0)
            {
                jump = false;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && jump == false)
                    {
                        playerforce = 8;
                        player.Top = x.Top - player.Height;
                        playerjump = 0;
                    }
                    x.BringToFront();
                    {
                        if (x is PictureBox && (string)x.Tag == "coin")
                        {
                            if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                            {
                                x.Visible = false;
                                playerscore += 1;
                            }

                        }
                    }
                }
            }

            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                hasKey = true;
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true)
            {
                door.Image = Properties.Resources.door_open;
                MainTimer.Stop();
                MessageBox.Show("You completed this stage" + Environment.NewLine + "Click to play again");
                Restart();
            }
        }
        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "door")
                {
                    if (direction == "back")
                    {
                        x.Left -= bgspeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += bgspeed;
                    }
                }
            }
        }
        private void Restart()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }
        private void CloseForm(object sender, FormClosedEventArgs eventArgs)
        {
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) ;
            {
                left = true;
            }
            if (e.KeyCode == Keys.D) ;
            {
                right = true;
            }
            if (e.KeyCode == Keys.W && jump == false) ;
            {
                jump = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) ;
            {
                left = false;
            }
            if (e.KeyCode == Keys.D) ;
            {
                right = false;
            }
            if (jump == true)
            {
                jump = false;
            }
        }
    }
}
