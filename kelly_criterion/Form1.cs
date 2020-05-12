using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;




namespace kelly_criterion
{
    public partial class Form1 : Form
    {
        int players_public=1;
        int max_toss_public=1;
        Decimal[,] A_public = new Decimal[99999 + 2, 3];
        Decimal[,] B_public = new Decimal[99999, 6];
        int gamenumber=0;
                    
        public Form1()
        {
            InitializeComponent();            
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
       
        }

   

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Decimal p = Convert.ToDecimal(textBox1.Text)/100;           //the chance
                Decimal b = Convert.ToDecimal(textBox2.Text);               // the reward of bet
                Decimal q = (1 - p);                                        //1-chance
                Decimal f = (((p * (b + 1)) - 1) / b)*100;                  //the kelly
                textBox3.Text = f.ToString();
                textBox9.Text = textBox1.Text;
                textBox10.Text = textBox2.Text;
                textBox4.Text = f.ToString();
                textBox5.Text = 300.ToString();
                textBox6.Text = 25.ToString();
                textBox7.Text = 300.ToString();
                textBox8.Text = 250.ToString();
                textBox11.Text = 1.ToString();

                //label5.Text = RandomNumber.Between(1, 100).ToString(); for test random
            }    
            catch (FormatException)
                {
                    MessageBox.Show("Put the number correctly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal p = Convert.ToDecimal(textBox9.Text);           //the chance
                Decimal b = Convert.ToDecimal(textBox10.Text);          //the reward of bet
                Decimal f = Convert.ToDecimal(textBox4.Text) / 100;     //the kelly
                int players = Convert.ToInt32(textBox5.Text);           //total players
                players_public = players;                               //put in the public for other button
                int max_toss = Convert.ToInt32(textBox7.Text);          //maximum iteration or toss
                max_toss_public = max_toss;                             //put in the public for other button
                Decimal max_cap = Convert.ToInt32(textBox8.Text);       //maximum cap earned
                Decimal[,] A = new Decimal[players + 2, 3];             //column 0=number of toss, 1 = cash after bet, 2=player #
                Decimal[,] B = new Decimal[players * max_toss + 2, 6];  //column 0= player #, 1=amount of bet, 2=the number generator, 3= win/lose status, 4=cash after bet, 5= # toss
                Decimal min_bet = Convert.ToDecimal(textBox11.Text);    //not developed yet... maybe i i should work on it next time
                Decimal total_payout = 0;                               // used for calculating average payout, 0 is default
                Decimal cash=0, total_bet = 0;
                int total_winner=0;                                     //counting the number that reached the maximum cap
                int i,j,t=1;                                            //standard looping, t for storing the data of each game on each player
                int hasil_tos;                                          //output of the generator number
                
                for (i=1; i<=players; i++) // GAME ON!!
                {
                    A[i, 2] = i;
                    A_public[i, 2] = A[i, 2];                           //put in on public for other button
                    cash = Convert.ToDecimal(textBox6.Text);            // every player will have same initial cash first
                    int winnertrue = 0;                                 //not used yet....
                    for (j=1; j<=max_toss; j++)                         //the iteration of the game for every player
                    {
                        B[t, 0] = i;                                    //B.player# | data store
                        B_public[t, 0] = B[t, 0];                       //put in on public for other button
                        B[t, 5] = j;                                    //B.toss# | data store
                        B_public[t, 5] = B[t, 5];                       //put in on public for other button
                        if (cash > max_cap)                             // checked if the cash is already reached the limit of maximum cap, if yes=stop
                        {
                            A[i, 0] = j;                                //| data store
                            A_public[i, 0] = A[i, 0];                   //put in on public for other button
                            A[i, 1] = cash;                             //| data store
                            A_public[i, 1] = A[i, 1];                   //put in on public for other button
                            total_payout = total_payout + A[i, 1];      //adding the total payout in the end of the game for average payout
                            total_winner++;                             //counting the winner = people who reach the maximum cap
                            break;
                        }
                        total_bet = cash * f;                           //the amount of bet that users put in the the game
                        B[t, 1] = total_bet;                            //B.total_bet# | data store
                        B_public[t, 1] = B[t, 1];                       //put in on public for other button
                        


                        /*if (total_bet<min_bet) //SAVED FOR NEXT DEV. for adding the bust feature or the minimum bet
                        {
                            A[i, 0] = j;
                            A[i,1]=cash;
                            total_payout = total_payout + A[i, 1];
                            break;}*/

                        hasil_tos = RandomNumber.Between(1, 100);       //generating the random number, credit for this one..
                        B[t, 2] = hasil_tos;                            //B.output bet |
                        B_public[t, 2] = B[t, 2];                       //put in on public for other button
                        if (hasil_tos <p) //YEAY WIN!
                        {
                            cash = cash + (total_bet * b);
                            B[t, 3] = 1;                                //if win then 1
                            B[t, 4] = cash;                             //B.cash left
                            B_public[t, 3] = B[t, 3];                   //put in on public for other button
                            B_public[t, 4] = B[t, 4];                   //put in on public for other button
                        }
                        else //TOTALLY FCKD
                        {
                            cash = cash - (total_bet);
                            B[t, 3] = 0;                                //if lose then 0
                            B[t, 4] = cash;                             //B.cash left
                            B_public[t, 3] = B[t, 3];                   //put in on public for other button
                            B_public[t, 4] = B[t, 4];                   //put in on public for other button
                        }

                        if(j==max_toss) //times up...
                        {
                            A[i, 0] = j+1;
                            A_public[i, 0] = A[i, 0];                   //put in on public for other button
                            A[i, 1] = cash;
                            A_public[i, 1] = A[i, 1];                   //put in on public for other button
                            total_payout = total_payout + A[i, 1];
                        }
                        t++;
                    }          
                }
                gamenumber = t;
                Decimal avg_payout = total_payout / players;            //counting the average payout
                label14.Text = avg_payout.ToString("#.##");
                label20.Text = total_winner.ToString();
                float winner_percent = (total_winner/players)*100;
                label28.Text = winner_percent.ToString();               //why the result is 0???


                //Shows Overview Data for default

                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Columns.Add("Player #");
                listView1.Columns.Add("Total games played");
                listView1.Columns.Add("End Cash");
                int k=0;
                while (k<players)
                {
                    listView1.Items.Add(A[k+1,2].ToString()); 
                    listView1.Items[k].SubItems.Add((A[k+1,0]-1).ToString());
                    listView1.Items[k].SubItems.Add(A[k + 1, 1].ToString("#.##"));
                    k++;
                }

               
                /*int y;
                for (y=0;y<players;y++) //NEVERMIND
                {
                    double[,] coba = new double[players + 2, 3];
                }*/


                /*for (int d = 1; d < players_public; d++)
                {
                    comboBox1.Items.Add(d); //NEVERMIND(2)
                }*/ 

            }           
            catch (FormatException)
            {
                MessageBox.Show("Put the number correctly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void button3_Click(object sender, EventArgs e) //SHOWING DATA OF DETAILED SIMULATION
        {
            try
            {
                MessageBox.Show("After You Close This Box, It Will Takes About 45 Seconds to Load The Data", "PLEASE CLOSE THIS BOX & BE PATIENT!",MessageBoxButtons.OK);
                label25.Text = comboBox1.SelectedIndex.ToString();
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Columns.Add("Player #");
                listView1.Columns.Add("Game #");
                listView1.Columns.Add("Amount of bet");
                listView1.Columns.Add("Generator Number");
                listView1.Columns.Add("Win(1)/Lose(0)");
                listView1.Columns.Add("Cash left");

                int m = 0;
                while (m < gamenumber)
                {
                    listView1.Items.Add(B_public[m+1, 0].ToString());
                    listView1.Items[m].SubItems.Add(B_public[m + 1, 5].ToString());
                    listView1.Items[m].SubItems.Add(B_public[m + 1, 1].ToString("#.##"));
                    listView1.Items[m].SubItems.Add(B_public[m + 1, 2].ToString());
                    listView1.Items[m].SubItems.Add(B_public[m + 1, 3].ToString());
                    listView1.Items[m].SubItems.Add(B_public[m + 1, 4].ToString("#.##"));
                    m++;
                }
            }

            catch (FormatException)
            {
                MessageBox.Show("Put the number correctly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //for testing only
            /*listView1.Items.Add(B_public[1, 0].ToString());
            listView1.Items[0].SubItems.Add(B_public[1, 1].ToString());
            listView1.Items[0].SubItems.Add(B_public[1, 2].ToString());
            listView1.Items[0].SubItems.Add(B_public[1, 3].ToString());
            listView1.Items[0].SubItems.Add(B_public[1, 4].ToString());
            listView1.Items.Add(B_public[1, 0].ToString());
            listView1.Items[1].SubItems.Add(B_public[2, 1].ToString());
            listView1.Items[1].SubItems.Add(B_public[2, 2].ToString());
            listView1.Items[1].SubItems.Add(B_public[2, 3].ToString());
            listView1.Items[1].SubItems.Add(B_public[2, 4].ToString());
            listView1.Items.Add(B_public[1, 0].ToString());
            listView1.Items[2].SubItems.Add(B_public[3, 1].ToString());
            listView1.Items[2].SubItems.Add(B_public[3, 2].ToString());
            listView1.Items[2].SubItems.Add(B_public[3, 3].ToString());
            listView1.Items[2].SubItems.Add(B_public[3, 4].ToString());*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //SHOWING DATA OF OVERVIEW SIMULATION
        {          
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add("Player #");
            listView1.Columns.Add("Total games played");
            listView1.Columns.Add("End Cash");
            int v = 0;
            while (v < players_public)
            {
                listView1.Items.Add(A_public[v + 1, 2].ToString());
                listView1.Items[v].SubItems.Add((A_public[v + 1, 0] -1).ToString());
                listView1.Items[v].SubItems.Add(A_public[v + 1, 1].ToString("#.##"));
                v++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" ", "Help", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


    }
}
