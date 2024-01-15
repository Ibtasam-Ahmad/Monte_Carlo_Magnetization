using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo_Mangetization
{
    internal class MonteCarlo
    {
            //data variables
            public Graphics gg, gg1;
            public SolidBrush bred, bblue;
            public Pen pblue, pred;
            public Font f;

            //take two dimensional array to represent two dimensional spin system
            public int[,] spinsystem;
            public int n_spins = 21;

            bool test;
            Random obj;

            public double E1, E2, E_Flip, P_Flip, r, J, KB, Z, mu, H;

            public MonteCarlo(Form1 frm1)
            {
                gg = frm1.CreateGraphics();
                gg1 = frm1.CreateGraphics();
                bred = new SolidBrush(Color.Red);
                bblue = new SolidBrush(Color.Blue);
                pblue = new Pen(Color.Blue, 5);
                pred = new Pen(Color.Red, 5);
                f = new Font("Arial", 16);

                spinsystem = new int[n_spins, n_spins];
                obj = new Random();
                test = false;

                E1 = 0; E2 = 0; E_Flip = 0; P_Flip = 0; r = 0; J = 1; KB = 1;
                mu = 1; Z = 4; H = 0;

                //show the spin system
                for (int i = 0; i < n_spins; i++)
                {
                    for (int j = 0; j < n_spins; j++)
                    {
                        r = obj.NextDouble();
                        if (r <= 0.5)
                        {
                            spinsystem[i, j] = 1;//spin up
                            gg.FillEllipse(bblue, 50 + j * 10, 200 + i * 10, 8, 8);
                        }
                        else
                        {
                            spinsystem[i, j] = +1;//spin up
                            gg.FillEllipse(bblue, 50 + j * 10, 200 + i * 10, 8, 8);
                        }
                    }
                }//spin system shown
            }//end of constructor
             //other functions

            public double EnergyCal(int r, int c)
            {
                return (-(int)J * spinsystem[r, c] * (spinsystem[r - 1, c]
                    + spinsystem[r + 1, c] +
                spinsystem[r, c - 1] + spinsystem[r, c + 1] + mu * H));
            }

            public bool Flip(double T)
            {
                E_Flip = E2 - E1;
                if (E_Flip <= 0)
                {
                    test = true;//spin will flip
                }
                else//E_flip>0
                {
                    P_Flip = Math.Exp(-E_Flip / (KB * T));//probability of flipping
                    r = obj.NextDouble();//random probability
                    if (P_Flip >= r)
                    {
                        test = true;
                    }
                    else
                    {
                        test = false;
                    }
                }
                return test;
            }
    }
}
