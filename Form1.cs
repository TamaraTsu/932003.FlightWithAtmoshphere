using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const decimal g = 9.81M;
        const decimal C = 0.15M;
        const decimal rho = 1.29M;
        decimal dt = 0.05M;
        decimal x1;
        int color=0;
        private void btClear_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i=0; i<7; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            labDistance.Text = "0";
            labTime.Text = "0";
            labHigh.Text = "0";
            labSpeed.Text = "0";
        }

        decimal x, y, v0, cosa, sina, S, m, k, vx, vy;

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dt = 0.009M;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dt = 0.01M;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dt = 0.05M;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            decimal v = (decimal)Math.Sqrt((double)(vx * vx + vy * vy));
            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
            chart1.Series[color].Points.AddXY(x, y);
            if (x1 < y)
            {
                x1 = y;
            }


            if (y <= 0)
            {
                timer1.Stop();
                labDistance.Text = "s=" + Math.Round(x, 3);
                labTime.Text = "dt=" + dt;
                labHigh.Text = "h=" + Math.Round(x1, 3);
                labSpeed.Text = "speed=" + Math.Round(v, 3);

            }
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            color++;
            if (color == 7)
            {
                color = 0;
            }
            timer1.Stop();
            labDistance.Text = "calculating...";
            labTime.Text = "calculating...";
            labHigh.Text = "calculating...";
            labSpeed.Text = "calculating...";
            if (!timer1.Enabled)
            {
                x = 0;
                y = edHeight.Value;
                v0 = edSpeed.Value;
                double a = (double)edAngle.Value * Math.PI / 180;
                cosa = (decimal)Math.Cos(a);
                sina = (decimal)Math.Sin(a);
                S = edSize.Value;
                m = edMass.Value;
                k = 0.5M * C * rho * S / m;
                vx = v0 * cosa;
                vy = v0 * sina;
                chart1.Series[color].Points.AddXY(x, y);
                timer1.Start();
            }
            x1 = 0;
            
        }

        
    }
}
