using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonteCarlo_Mangetization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MonteCarlo mc = new MonteCarlo(this);

            int n_sweeps = 100;//at every temperature value, spin system will be swept
            //n_sweep times

            double M = 0, Mave = 0, Tc = 0;//M is magnetization during one sweep
            //Mave is average magnetization of all sweeps

            bool check;

            //frm2.Show();
            for (double T = 0.1; T < 10; T = T + 0.2)//Temperature loop for MFT as well as Monte Carlo
            {
                Mave = 0;
                for (int sweep = 0; sweep < n_sweeps; sweep++)//sweep loop
                {
                    M = 0;//at the start of each sweep, M is taken zero
                    for (int i = 1; i < (mc.n_spins - 1); i++)
                        //both for loops represent
                        //single sweep
                    {
                        for (int j = 1; j < (mc.n_spins - 1); j++)
                        {
                            mc.E1 = mc.EnergyCal(i, j);
                            //it computes internal energy 
                            //of spin at ith row and jth column, before its flipping

                            //now assumingly flip the spin
                            mc.spinsystem[i, j] = mc.spinsystem[i, j] * -1;//flip the spin
                            mc.E2 = mc.EnergyCal(i, j);//it computes internal energy 
                                                       //of spin at ith row and jth column, after its flipping

                            check = mc.Flip(T);//check wheter spin flipping
                            //is accepted or not
                            if (check == true)
                            {
                                //accept the flip
                                if (mc.spinsystem[i, j] == -1)//if spin-down
                                    mc.gg.FillEllipse(mc.bred, 50 + j * 10, 200 + i * 10, 8, 8);
                                else//if spin-up
                                    mc.gg.FillEllipse(mc.bblue, 50 + j * 10, 200 + i * 10, 8, 8);
                            }
                            else//check==false means flipping is rejected
                            {
                                mc.spinsystem[i, j] = mc.spinsystem[i, j] * -1;
                            }
                            M = M + mc.spinsystem[i, j];//
                        }//internal for loop
                    }//external for loop
                    Mave = Mave + M / (mc.n_spins * mc.n_spins - 4 * mc.n_spins + 4);
                }//end of sweeps loop
                Mave = Mave / n_sweeps;//average magnetization at single value
                //of temperature
                //**********MFT Computations for the average magnetization******

                for (double s = 0; s < 1.5; s = s + 0.01)
                {
                    if (Math.Abs(s - Math.Tanh(mc.J * mc.Z * s / (mc.KB * T))) < 0.001)
                        mc.gg1.FillEllipse(mc.bred, 350 + (float)T * 100,
                        400 - (float)s * 300, 8, 8);
                }

                mc.gg1.FillEllipse(mc.bblue, 350 + (float)T * 100,
                    400 - (float)Mave * 300, 8, 8);
                if (Math.Abs(Mave) < 0.01)
                { 
                    Tc = T; 
                }
                //graph of average magnetization versus T
            }//Temperature Loop
            MessageBox.Show("Tc=" + Tc.ToString());
        }
    }
    
}
