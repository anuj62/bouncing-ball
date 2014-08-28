using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Bouncing_Ball
{
    public partial class Form1 : Form
    {
        int speed=5,bmargin=88,tmargin=25,level=1;        
        public Form1()
        {
            InitializeComponent();
        }
        Point p = new Point(); //for ball
        Point q = new Point(); //for ball following slider
        Point r = new Point(); //for mouse following slider
        int cnt=0;        
        private void Form1_Load(object sender, EventArgs e)
        {                      
            r.X = 5;
            r.Y = MousePosition.Y - this.Location.Y - 70;
            pictureBox3.Location = r;
            q.X = this.Size.Width - 35;            
            p.Y = pictureBox1.Location.Y;
            q.Y = p.Y;
            pictureBox2.Location = q;
            p.X = pictureBox1.Location.X;                       
        }      
        
        private void bottomright_Tick(object sender, EventArgs e)
        {            
            if (p.X + bmargin >= this.Size.Width)
            {
                bottomright.Enabled = false;
                bottomleft.Enabled = true;
            }
            else if (p.Y + bmargin >= this.Size.Height)
            {
                bottomright.Enabled = false;
                topright.Enabled = true;
            }
            else
            {
                r.Y = MousePosition.Y - this.Location.Y - 70;
                pictureBox3.Location = r;             
                p.Y += speed;                
                p.X += speed;                
                pictureBox1.Location = p;
                q.Y = p.Y;
                pictureBox2.Location = q;
            }
        }

        private void topright_Tick(object sender, EventArgs e)
        {
            if (p.X + bmargin >= this.Size.Width)
            {
                topright.Enabled = false;
                topleft.Enabled = true;
            }
            else if (p.Y <= tmargin)
            {

                topright.Enabled = false;
                bottomright.Enabled = true;
            }                
            else
            {                
                r.Y = MousePosition.Y - this.Location.Y - 70;
                pictureBox3.Location = r;             
                p.Y -= speed;
                p.X += speed;
                pictureBox1.Location = p;
                q.Y = p.Y;
                pictureBox2.Location = q;
            }
        }
        
        private void topleft_Tick(object sender, EventArgs e)
        {
            if (p.X <= tmargin)
            {
                if (r.Y <= p.Y + 25 && r.Y >= p.Y - 25)
                {
                    topleft.Enabled = false;
                    topright.Enabled = true;
                }
                else
                {
                    cnt++;
                }
            }
            else if (p.Y <= tmargin)
            {
                topleft.Enabled = false;
                bottomleft.Enabled = true;
            }
            if (cnt <= 10)
            {
                r.Y = MousePosition.Y - this.Location.Y - 70;
                pictureBox3.Location = r;           
                p.Y -= speed;
                p.X -= speed;
                pictureBox1.Location = p;
                q.Y = p.Y;
                pictureBox2.Location = q;
            }
            else
            {                                                    
                topleft.Enabled = false;
                levelcounter.Enabled = false;
                FileStream fs = new FileStream("highscores.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Level: " + level);
                sw.Close();
                fs.Close();
                MessageBox.Show("Game Over");
                fileToolStripMenuItem.Enabled = true;
            }
        }

        private void bottomleft_Tick(object sender, EventArgs e)
        {
            if (p.X <= tmargin)
            {
                if (r.Y <= p.Y + 25 && r.Y >= p.Y - 25)
                {
                    bottomleft.Enabled = false;
                    bottomright.Enabled = true;
                }
                else
                {
                    cnt++;
                }                                
            }
            else if (p.Y + bmargin >= this.Size.Height)
            {
                bottomleft.Enabled = false;
                topleft.Enabled = true;
            }
            if (cnt <= 10)
            {
                r.Y = MousePosition.Y - this.Location.Y - 70;
                pictureBox3.Location = r;             
                p.X -= speed;
                p.Y += speed;
                pictureBox1.Location = p;
                q.Y = p.Y;
                pictureBox2.Location = q;
            }
            else
            {                
                bottomleft.Enabled = false;
                levelcounter.Enabled = false;
                FileStream fs = new FileStream("highscores.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);                
                sw.WriteLine("Level: "+level);
                sw.Close();
                fs.Close();
                MessageBox.Show("Game Over");
                fileToolStripMenuItem.Enabled = true;
            }
        }
        
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileToolStripMenuItem.Enabled = false;
            cnt = 0;
            speed = 5;
            level = 1;            
            bottomright.Enabled = true;
            levelcounter.Enabled = true;
        }
        
        private void levelcounter_Tick(object sender, EventArgs e)
        {            
            bottomleft.Enabled = false;
            topleft.Enabled = false;
            topright.Enabled = false;
            bottomright.Enabled = false;
            levelcounter.Enabled = false;
            MessageBox.Show("You completed level " + level);            
            cnt = 0;
            speed += 2;
            level++;
            bottomright.Enabled = true;
            levelcounter.Enabled = true;
        }

        private void highscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("highscores.txt", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();
            if (str != "")
                MessageBox.Show("Scores:\n" + str);
            else
                MessageBox.Show("No Highscores Yet");
            sr.Close();
            fs.Close();
        }              
    }
}
