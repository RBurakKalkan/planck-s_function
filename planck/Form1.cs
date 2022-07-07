using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Collections;

namespace planck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.chart1.MouseWheel += chart1_MouseWheel;
            textBox1.Enabled = false;
        }
        public double wave, BV, h, c, k, XKT, XKT2, XKT3, eu, hclambda, lambdaMax5, lambdaMax7, BV2, BVort, eu2, eu3,Temp;

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart2.Series[0].Points.Clear();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Enabled == false && checkBox3.Checked == true)
            {
                textBox1.Enabled = true;
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(textBox1.Text);
            }
            else if(textBox1.Enabled == true && checkBox3.Checked == false)
            {
                textBox1.Enabled = false;
                chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            { trackBar2.Enabled = true; label1.Text = "";label2.Text = "";label3.Text = "";checkBox1.Enabled = false; }
            else { trackBar2.Enabled = false; checkBox1.Enabled = true; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            { trackBar1.Enabled = true; label1.Text = ""; label2.Text = ""; label3.Text = "";checkBox2.Enabled = false; }
            else { trackBar1.Enabled = false; checkBox2.Enabled = true; }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[2].Points.Clear();
            Temp = Convert.ToDouble(trackBar2.Value);
            double X;
            h = 6.625e-27;
            c = 2.997e10;
            k = 1.38e-16;
            for (X = 0; X <= 2e-4; X = X + 1e-7)
            {
                wave = X;
                XKT3 = (h * c) / (wave * k * Temp);
                hclambda = Math.Pow(wave, 5);
                eu3 = Math.Pow(Math.E, XKT3); 
                BVort = (2 * ((h * c * c) / hclambda)) / (eu3 - 1);
                chart1.Series[2].Points.AddXY(X, BVort);
            }
            chart1.Series[2].Name = "T=" + Temp;
            chart1.Series[2].ChartType = SeriesChartType.FastLine;
            chart1.Series[2].Color = Color.Crimson;
        }

        public static double CalculateSlope(Point a, Point b)
        {
            if (b.Y == a.Y)
                return double.PositiveInfinity;

            if (b.X == a.X)
                return 0.0;

            return (Convert.ToDouble(b.Y) - Convert.ToDouble(a.Y)) / (Convert.ToDouble(b.X) - Convert.ToDouble(a.X));
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            chart1.Series[0].ToolTip = "#VALY, #VALX";
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
        double a, b, Sıcort;
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            a = Convert.ToDouble(trackBar1.Value) * 1e-4;
            b = 1 - a;
            label1.Text = (Math.Round(a, 4)).ToString() + " * βλ(T=5100)";
            label2.Text = (Math.Round(b, 4)).ToString() + " * βλ(T=7120)";
            double X;
            h = 6.625e-27;
            c = 2.997e10;
            k = 1.38e-16;
            for (X = 0; X <= 2e-4; X = X + 1e-7)
            {
                wave = X;
                XKT = (h * c) / (wave * k * 5100);
                XKT2 = (h * c) / (wave * k * 7120);
                hclambda = Math.Pow(wave, 5);
                eu = Math.Pow(Math.E, XKT);
                BV = (2 * ((h * c * c) / hclambda)) / (eu - 1);
                eu2 = Math.Pow(Math.E, XKT2);
                BV2 = (2 * ((h * c * c) / hclambda)) / (eu2 - 1);
                BVort = a * BV + b * BV2;
                chart1.Series[0].Points.AddXY(X, BV);
                chart1.Series[1].Points.AddXY(X, BV2);
                chart1.Series[2].Points.AddXY(X, BVort);
            }

            Sıcort = 0.28977721 / (chart1.Series[2].Points.FindMaxByValue().XValue);

            chart1.Series[2].Name = "T=" + Math.Round(Sıcort, 1);
            chart1.Series[0].ChartType = SeriesChartType.FastLine;
            chart1.Series[1].ChartType = SeriesChartType.FastLine;
            chart1.Series[2].ChartType = SeriesChartType.FastLine;
            chart1.Series[0].Color = Color.Black;
            chart1.Series[1].Color = Color.Black;
            chart1.Series[2].Color = Color.Purple;
            chart1.Series[1].MarkerSize = 1;
            chart1.Series[0].MarkerSize = 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            double X;
            h = 6.625e-27;
            c = 2.997e10;
            k = 1.38e-16;
            if (chart1.Series[3].Points.Count == 0)
            {
                for (X = 0; X <= 2e-4; X = X + 1e-6)
                {
                    wave = X;
                    XKT = (h * c) / (wave * k * 5780);
                    hclambda = Math.Pow(wave, 5);
                    eu = Math.Pow(Math.E, XKT);
                    BV = (2 * ((h * c * c) / hclambda)) / (eu - 1);
                    chart2.Series[0].Points.AddXY(X, BV);
                    chart1.Series[3].Points.AddXY(X, BV);
                }
                chart2.Series[0].ChartType = SeriesChartType.FastLine;
                chart1.Series[3].ChartType = SeriesChartType.FastLine;
                chart2.Series[0].Color = Color.Black;
                chart1.Series[3].Color = Color.Red;
                chart2.Series[0].MarkerSize = 3;
            }
            else {
                chart2.Series[0].Points.Clear();
                chart1.Series[3].Points.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            double X;
            h = 6.625e-27;
            c = 2.997e10;
            k = 1.38e-16;


            if (chart1.Series[0].Points.Count == 0 && chart1.Series[1].Points.Count == 0)
            {
                for (X = 0; X <= 2e-4; X = X + 1e-6)
                {
                    wave = X;
                    XKT = (h * c) / (wave * k * 5100);
                    XKT2 = (h * c) / (wave * k * 7120);
                    hclambda = Math.Pow(wave, 5);
                    eu = Math.Pow(Math.E, XKT);
                    BV = (2 * ((h * c * c) / hclambda)) / (eu - 1);
                    eu2 = Math.Pow(Math.E, XKT2);
                    BV2 = (2 * ((h * c * c) / hclambda)) / (eu2 - 1);
                    lambdaMax5 = 0.6666666666666;
                    lambdaMax7 = 0.3333333333333;
                    BVort = lambdaMax5 * BV + lambdaMax7 * BV2;
                    chart1.Series[0].Points.AddXY(X, BV);
                    chart1.Series[1].Points.AddXY(X, BV2);
                }
                chart1.Series[0].ChartType = SeriesChartType.FastLine;
                chart1.Series[1].ChartType = SeriesChartType.FastLine;
                chart1.Series[0].Color = Color.Black;
                chart1.Series[1].Color = Color.Black;
                chart1.Series[1].MarkerSize = 1;
                chart1.Series[0].MarkerSize = 1;
            }
            else if(chart1.Series[0].Points.Count != 0 && chart1.Series[0].Points.Count != 0) {
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Color = Color.Black;
            chart1.Series[1].Color = Color.Black;
            chart1.Series[2].Color = Color.Purple;
            chart1.Series[1].MarkerSize = 3;
            chart1.Series[0].MarkerSize = 3;
            chart2.Series[0].ChartType = SeriesChartType.FastLine;
            chart1.Series[3].ChartType = SeriesChartType.FastLine;
            chart2.Series[0].Color = Color.Black;
            chart1.Series[3].Color = Color.Red;
            chart2.Series[0].MarkerSize = 3;
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            this.AutoSize = false;
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;


                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    chart1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }
    }
}
