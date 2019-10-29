using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2ProjectF
{
    public partial class Form2 : Form
    {
        Label[] tops = new Label[10];//an array of the labels which will show the highscores

      
        public string[][] scores;
        public Form2(string[][] hscores)
        {
            InitializeComponent();

            scores = hscores;
            tops[0] = label2;
            tops[1] = label3;
            tops[2] = label4;
            tops[3] = label5;
            tops[4] = label6;
            tops[5] = label7;
            tops[6] = label8;
            tops[7] = label9;
            tops[8] = label10;
            tops[9] = label11;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            for (int i = 0; i < 10; i++)
            {
                if (scores[i][1] == "-1")//no highscore yet 
                {
                    tops[i].Text = "No high score at this position";
                }
                else if (scores[i][1] != "-1")
                {
                    tops[i].Text = "Position " + (i + 1).ToString() + " High score set by player '" + scores[i][0] + "' with time: " + scores[i][1] + " seconds!!!";
                }
            }
            
        }

        
    }
}
