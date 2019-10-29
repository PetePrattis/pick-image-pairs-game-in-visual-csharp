using System;
using System.Drawing;
using System.Windows.Forms;




namespace WindowsFormsApplication2ProjectF
{
    public partial class Form1 : Form
    {
        #region General
        PictureBox[] pb = new PictureBox[24];//the pictureBox array for the game
        PictureBox[] pb1 = new PictureBox[12];//the pictureBox array for the small pictureBoxes at the game menu panel
        string[] paths = new string[12];//the saved image paths
        string[] link = new string[24];//this will help me determine which image belongs to which pictureBox and which picturBoxes are a pair
        int[] timespicked = new int[12];//this will help me find out how many times each image was asigned to a picturebox

        //in order to check if 2 of the same cards are facing up, i will create a 1 dimensional array with 2 indexes
        //the 1st index will have the path of the 1st pictureBox's image and the 2nd index will have the 2nd's 
        string[] pair = new string[2];
        string[] pairn = new string[2];

        //i will also create a PictureBox array with 2 indexes to help me know which pictureBoxes are each time flipped
        PictureBox[] selectedp = new PictureBox[2];

        //in the game there will also be the custom images setting
        //i will create a string array that will have the url of the images
        public string[] urlimg = new string[12];

        //a public variable that will let me know if i picked custom images or from pc
        bool img = false;

        //i will use this array to save the top scores, names + time
        public string[][] hscores = new string[10][]
        {
            new string[] {"beat me","500"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
            new string[] {"","-1"},
        };

        
        #endregion

        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;

            //initializing the PictureBox pb array with the game's pictureBoxs
            pb[0] = pictureBox1;
            pb[1] = pictureBox2;
            pb[2] = pictureBox3;
            pb[3] = pictureBox4;
            pb[4] = pictureBox5;
            pb[5] = pictureBox6;
            pb[6] = pictureBox7;
            pb[7] = pictureBox8;
            pb[8] = pictureBox9;
            pb[9] = pictureBox10;
            pb[10] = pictureBox11;
            pb[11] = pictureBox12;
            pb[12] = pictureBox13;
            pb[13] = pictureBox14;
            pb[14] = pictureBox15;
            pb[15] = pictureBox16;
            pb[16] = pictureBox17;
            pb[17] = pictureBox18;
            pb[18] = pictureBox19;
            pb[19] = pictureBox20;
            pb[20] = pictureBox21;
            pb[21] = pictureBox22;
            pb[22] = pictureBox23;
            pb[23] = pictureBox24;

            pb1[0] = pictureBox25;
            pb1[1] = pictureBox26;
            pb1[2] = pictureBox27;
            pb1[3] = pictureBox28;
            pb1[4] = pictureBox32;
            pb1[5] = pictureBox31;
            pb1[6] = pictureBox30;
            pb1[7] = pictureBox29;
            pb1[8] = pictureBox36;
            pb1[9] = pictureBox35;
            pb1[10] = pictureBox34;
            pb1[11] = pictureBox33;

            initializer ini = new initializer();//constructor that creates an object reference to the class initializer
            for(int i=0;i<=11;i++)
            {
                urlimg[i] = ini.p[i];//i have initializd the p array with the urls and i copy them to the urlimg array
            }
            


            for (int i = 0; i<=23; i++)//at the start all pictureBoxes are turned backwards
            {
                pb[i].BackColor = Color.Black;//they are black
                pb[i].Enabled = false;//they can't be clicked
                open[i] = false;//they are backwards
            }
            for(int i =0; i<=11; i++)
            {
                timespicked[i] = 0;//and i haven't initialized them with any images yet 
            }

            

        }



        private void button1_Click(object sender, EventArgs e)//the pick images button
        {//it will also arrange the images into the pictureBoxes
            clear();
           
            img = true;
            for (int i = 0; i <= 11; i++)
            {
                bool picked = true;
                
                while (picked)
                {
                    OpenFileDialog of = new OpenFileDialog();//we create a openfiledialog object
                    //For any other formats
                    of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        for (int j = 0; j <= 11; j++)
                        {
                            if(paths[j] == of.FileName.ToString())
                            {
                                MessageBox.Show("Don't Pick the same Image twice");
                                picked = true;
                            }
                            else
                                picked = false;                          
                        }
                        if (!picked)
                        {
                            paths[i] = of.FileName;   
                        }
                    };
                }
              
            }

            SetPB();
        }

        public void SetPB()//It sets the pictureBoxes' images, each image goes to 2 pictureBoxes
        {
            Random r = new Random();
            int x;
            for(int i = 0; i <=23; i++)//for each pictureBox
            {
                x = r.Next(0, 12);
                if (timespicked[x] < 2)//a random picture will be asigned
                {//making sure that the random image isn't used more than 2 times
                    link[i] = paths[x];
                    timespicked[x]++;
                }
                else if (timespicked[x] >= 2)//if the random image is already used 2 times
                {//we repeat the procedure for that particular pictureBox
                    i--;
                }
            }

            for(int i = 0; i<=11; i++)//the small pictureBoxs at the side will show which are the images of the game
            {
                pb1[i].ImageLocation = paths[i];
            }
            
        }

        public void turnCard(int x)//it will turn the card the other side
        {//the x variable that i pass is the number of the picturebox at the pb array
            while (pb[x].Width >= 1)//at the start it shrinks the card
            {//creating the illusion that i flip the card
                pb[x].Width--;
            }

            pb[x].ImageLocation = link[x];//i just load the image
            pb[x].BackColor = Color.Transparent;//i just remove the color
            
            open[x] = !open[x];
            while (pb[x].Width < 150)//it unfolds the card
            {//creating the illusion that i flip the card over
                pb[x].Width++;
            }
        }

        int wincounter, allcounter, loscounter;       
        int tm3result;//this will help me execute the timer properly
        public void pairFinder(string[] pair)
        {           
            //our 2 situations for when i have both cards of an effort flipped, they will be either the same or not
            if (pair[1] != "")//firstly i will check if i have 2 cards flipped
            {
                
               //while the timer3 runs i wont be able to click any image because i already have 2 images flipped over
                if(pair[0] == pair[1])//if the images of the 2 pictureBoxes are the same then we got a pair
                {
                    tm3result = 1;//it is 1 when its a pair
                    timer3.Start();                  
                }
                else if(pair[0] != pair[1] && pair[1] != null)//if the 2 images are not the same
                {
                    tm3result = 0;//it is 0 when its not a pair
                     timer3.Start();                      
                }
                

            }
        }

        private void timer3_Tick(object sender, EventArgs e)//the time it needs to pass till the 2 cards turn over
        {
            timer3.Stop();
            if (tm3result == 1)
            {
               
                selectedp[0].Enabled = false;
                
                selectedp[1].Enabled = false;
                wincounter++;
                allcounter++;
                
                pair[0] = null;//the effort starts over
                pair[1] = null;
                //there's the situation when after finding a new pair there are no more cards to pair so tha player wins
                if (wincounter == 12)//if the player finds all pairs
                {
                    tm3result = -1;
                    timer3.Start();
                }
            }
            else if (tm3result == 0)
            {
                while (selectedp[0].Width >= 1)//it shrinks both cards
                {//creating the illusion that i flip the card

                    selectedp[0].Width--;
                    selectedp[1].Width--;

                }

                selectedp[0].Image.Dispose();//i delete the loaded image
                selectedp[0].Image = null;//the value of the picturebox's image becomes null
                selectedp[0].BackColor = Color.Black;//i color the picturebox with black
                selectedp[1].Image.Dispose();//i delete the loaded image
                selectedp[1].Image = null;//the value of the picturebox's image becomes null
                selectedp[1].BackColor = Color.Black;//i color the picturebox with black
                loscounter++;
                allcounter++;
                while (selectedp[0].Width <= 150)//it unfolds both cards
                {//creating the illusion that i flip the card

                    selectedp[0].Width++;
                    selectedp[1].Width++;


                }
                selectedp[0].Enabled = true;//you can try again
                selectedp[1].Enabled = true;
                pair[0] = null;
                pair[1] = null;
            }
            else if (tm3result == -1)
            {

                timer1.Stop();//i stop the time               
                
                
                MessageBox.Show("Congratulations! Player '" + textBox1.Text + "' beat the game." + "\n" +
                    "You found all pairs within " + time + " seconds," + "\n" + "having only "
                    + loscounter + " failed attempts out of " + allcounter + " total attempts!");

                
                Scores();
                

            }
            
        }

        public void Scores()//a method that will arrange the highscores
        {
            for (int i = 0; i < 10; i++)//for all of the 10 highscore positions
            {
                if (time <= long.Parse(hscores[i][1]) && long.Parse(hscores[i][1]) != -1)//if we have a better time
                {
                    for (int j = 9; j > i; j--)//we move all scores one place below deleting the 10th highscore
                    {
                        hscores[j][1] = hscores[j - 1][1];
                        hscores[j][0] = hscores[j - 1][0];
                    }
                    hscores[i][1] = time.ToString();
                    hscores[i][0] = textBox1.Text;
                    break;//once we find where this score goes we stop the loop
                }
                else if (hscores[i][1] == "-1")//or if we find a position where there is no highscore yet
                {              //we put that score at that position    
                    hscores[i][0] = textBox1.Text;
                    hscores[i][1] = time.ToString();
                    break;
                }
            }
            clear();//we call the clear method to restart the game
        }
        

        bool[] open = new bool[24];//it will help determine if the cards are open to see
        //at the start of the game all cards are closed as an empty boolean array it's initialized with false value

        #region PictureBoxesClick

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)//if we don't already have 2 pairs flipped over
            {
                turnCard(0);//i pass the value of the pb array place that each of the pictureboxes are 
                            //every time i click on a pictureBox it will save it's image's path to the pair[] array
                            //and i will call the pairFinder method to check if the 2 pictureBoxes are the same

                pb[0].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[0];
                    selectedp[0] = pb[0];
                    pairFinder(pair);
                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[0];
                    selectedp[1] = pb[0];
                    pairFinder(pair);
                }
            }
        }  

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(1);


                pb[1].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[1];

                    selectedp[0] = pb[1];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[1];

                    selectedp[1] = pb[1];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(2);

                pb[2].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[2];

                    selectedp[0] = pb[2];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[2];

                    selectedp[1] = pb[2];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(3);

                pb[3].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[3];

                    selectedp[0] = pb[3];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[3];

                    selectedp[1] = pb[3];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(4);

                pb[4].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[4];

                    selectedp[0] = pb[4];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[4];

                    selectedp[1] = pb[4];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(5);

                pb[5].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[5];

                    selectedp[0] = pb[5];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[5];
                    selectedp[1] = pb[5];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(6);

                pb[6].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[6];
                    selectedp[0] = pb[6];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[6];
                    selectedp[1] = pb[6];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(7);

                pb[7].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[7];
                    selectedp[0] = pb[7];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[7];
                    selectedp[1] = pb[7];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(8);

                pb[8].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[8];
                    selectedp[0] = pb[8];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[8];
                    selectedp[1] = pb[8];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(9);

                pb[9].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[9];
                    selectedp[0] = pb[9];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[9];
                    selectedp[1] = pb[9];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(10);

                pb[10].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[10];
                    selectedp[0] = pb[10];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[10];
                    selectedp[1] = pb[10];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(11);

                pb[11].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[11];
                    selectedp[0] = pb[11];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[11];
                    selectedp[1] = pb[11];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(12);

                pb[12].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[12];
                    selectedp[0] = pb[12];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[12];
                    selectedp[1] = pb[12];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(13);

                pb[13].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[13];
                    selectedp[0] = pb[13];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[13];
                    selectedp[1] = pb[13];

                    pairFinder(pair);

                }
            }
        }
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(14);

                pb[14].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[14];
                    selectedp[0] = pb[14];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[14];
                    selectedp[1] = pb[14];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(15);

                pb[15].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[15];
                    selectedp[0] = pb[15];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[15];
                    selectedp[1] = pb[15];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(16);

                pb[16].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[16];
                    selectedp[0] = pb[16];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[16];
                    selectedp[1] = pb[16];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(17);

                pb[17].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[17];
                    selectedp[0] = pb[17];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[17];
                    selectedp[1] = pb[17];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(18);

                pb[18].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[18];
                    selectedp[0] = pb[18];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[18];
                    selectedp[1] = pb[18];

                    pairFinder(pair);

                }
            }
        }
       
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(19);

                pb[19].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[19];
                    selectedp[0] = pb[19];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[19];
                    selectedp[1] = pb[19];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(20);

                pb[20].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[20];
                    selectedp[0] = pb[20];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[20];
                    selectedp[1] = pb[20];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(21);

                pb[21].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[21];
                    selectedp[0] = pb[21];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[21];
                    selectedp[1] = pb[21];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(22);

                pb[22].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[22];
                    selectedp[0] = pb[0];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[22];
                    selectedp[1] = pb[22];

                    pairFinder(pair);

                }
            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                turnCard(23);

                pb[23].Enabled = false;
                if (pair[0] == null)//if this is the 1st card i flip at this effort and i flipped it with the image up front
                {
                    pair[0] = link[23];
                    selectedp[0] = pb[23];

                    pairFinder(pair);

                }
                else if (pair[0] != null && pair[1] == null)//or if this is the 2nd
                {
                    pair[1] = link[23];
                    selectedp[1] = pb[23];

                    pairFinder(pair);

                }
            }
        }

        #endregion


        private void button4_Click(object sender, EventArgs e)//the start game button
        {
            
            int ccount = 0;
            if (img)//if picked from pc
            {
                for (int i = 0; i < 12; i++)
                {
                    if (paths[i] != null)
                        ccount++;
                }
            }
            else//if custom
            {
                for (int i = 0; i < 24; i++)
                {
                    if (link[i] != null)
                        ccount++;
                }
                ccount = ccount / 2;//it will either be 0/2 or 24/2
            }

            if (ccount==12 && textBox1.Text != "")//you must have picked images first
            {
                for (int i = 0; i <= 23; i++)//it enables the pictureBox click
                {
                    pb[i].Enabled = true;
                }
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                textBox1.Enabled = false;//you can't change your name while playing
                wincounter = 0;
                allcounter = 0;
                loscounter = 0;
                timecounter = 0;
                time = 0;
                mins = 0;
                timer1.Start();
            }
            else if(textBox1.Text == "")
                MessageBox.Show("You need to input a player name before playing the game!");
            else if (ccount != 12)
                MessageBox.Show("You need to pick your own images from pc or pick the custom images to play this game!");
        }

        int timecounter,time,mins;
        private void timer1_Tick(object sender, EventArgs e)//timer for the gameplay time
        {
            
            timecounter++;
            time++;//total seconds
            if(timecounter <= 59)
                label4.Text = mins.ToString() + "." +timecounter.ToString();
            else if (timecounter == 60)
            {
                mins++;
                timecounter = 0;
                label4.Text = mins.ToString() + "." + timecounter.ToString();
            }

            label5.Text = allcounter.ToString();
            label6.Text = loscounter.ToString();
            label9.Text = wincounter.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(hscores);
            f2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void button7_Click(object sender, EventArgs e)//info button
        {
            MessageBox.Show("This is a memory game, you have to find out where the pairs are," + "\n" +
                "remembering each time which cards you have already flipped over" + "\n" +
                "and at the beginning you don't get a chance to see where are the images for some seconds!!!");          
            
        }



        private void button3_Click(object sender, EventArgs e)//the custom images button
        {
            clear();
                      
            img = false; //it is false when i pick custom images
            Random r = new Random();
            int x;
            for (int i = 0; i <= 23; i++)//for each pictureBox
            {
                x = r.Next(0, 12);
                if (timespicked[x] < 2)//a random picture will be asigned
                {//making sure that the random image isn't used more than 2 times
                    link[i] = urlimg[x];                                      
                    timespicked[x]++;
                }
                else if (timespicked[x] >= 2)//if the random image is already used 2 times
                {//we repeat the procedure for that particular pictureBox
                    i--;
                }
            }

            for (int i = 0; i <= 11; i++)//the small pictureBoxs at the side will show which are the images of the game
            {
                pb1[i].Load(urlimg[i]);
            }

        }

        public void clear()//a method that will clear the images in order to restart the game with other images
        {
            for (int i = 0; i <= 23; i++)
            {
                try
                {
                    pb[i].Image.Dispose();
                    pb[i].Image = null;
                    pb[i].InitialImage = null;
                    pb[i].BackColor = Color.Black;
                    pb[i].Enabled = false;
                    open[i] = false;
                }
                catch { }
            }
            for (int i = 0; i <= 11; i++)
            {
                try
                {
                    timespicked[i] = 0;
                    paths[i] = null;
                }
                catch { }
            }
            for(int i =0; i<=11; i++)
            {
                try
                {
                    pb1[i].Image.Dispose();
                    pb1[i].Image = null;
                    pb1[i].InitialImage = null;
                }
                catch { }
            }
            Enable();
        }

        public void Enable()//it enables the buttons and the textbox in order to play again
        {
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            textBox1.Clear();
            textBox1.Enabled = true;
            label4.Text = "0";
            label5.Text = "0";
            label6.Text = "0";
            label9.Text = "0";
        }
              
    }

    public class initializer//a class that i will use to initialize the url of the custom images
    {
        public string[] p = new string[12];
        public initializer()
        {
            //initializing the custom images array with the url
            p[0] = "http://i.imgur.com/mCZPx22b.jpg";
            p[1] = "http://i.imgur.com/Rb4F9IAb.jpg";
            p[2] = "http://i.imgur.com/SxN9uneb.jpg";
            p[3] = "http://i.imgur.com/K7xdiGnb.jpg";
            p[4] = "http://i.imgur.com/2QoDjwub.jpg";
            p[5] = "http://i.imgur.com/oRTMFS2b.jpg";
            p[6] = "http://i.imgur.com/6IRArOXb.jpg";
            p[7] = "http://i.imgur.com/vN2QNlnb.jpg";
            p[8] = "http://i.imgur.com/aSe06jhb.jpg";
            p[9] = "http://i.imgur.com/QpAVRzUb.jpg";
            p[10] = "http://i.imgur.com/B062ueyb.jpg";
            p[11] = "http://i.imgur.com/CiapGHEb.jpg";
        }
    }

    
}
