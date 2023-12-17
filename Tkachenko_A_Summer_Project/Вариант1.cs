using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
<<<<<<< HEAD
using System.Globalization;
using System.Threading;
=======
>>>>>>> 595bb0288e30f30d71a2f86f8913bbc4f2029099

namespace Tkachenko_A_Summer_Project
{
    public partial class Вариант1 : Form
    {
        float  R = 10;
        /// <summary>
        /// Wolf and his params
        /// </summary>
        float  Xv; // wolf's x coordinate
        float  XvOld; // wolf's old x coordinate

        float Vvx; // wolf's x speed
        float Vv0; // wolf's starting x speed
        float Vv1; // wolf's top speed

        /// <summary>
        /// hair and his params
        /// </summary>
        float  Xz; // hair's x coordinate
        float  XzOld; // hair's old x coordinate
        float  Vzx; // hairs x speed

        float  r; // Distance between the wolf and the hair
        float r0Real; // Distance between the wolf and the hair
        float vZReal; // Distance between the wolf and the hair

        float XBase;
        float XBaset;
        float t; // time
        float dt; // time step
        float DeltaX; // allowed error 
        float koef; // needed for accurate visualisation

        float VvMax; // max wolf's speed

<<<<<<< HEAD
        bool Axis1InitDrawFlag = false;
        bool Axis2InitDrawFlag = false;
        bool Axis1update = false;
        bool Axis2update = false;
        // float testparam = 0;
        bool Axis1last = false;
        float Dkoef;
        float Ykoef;

        List<Point> HairXes = new List<Point>();
        List<List<Point>> WolfXes = new List<List<Point>>();
        int itterationcounter = -1;
=======
        bool drawAxis = false;

>>>>>>> 595bb0288e30f30d71a2f86f8913bbc4f2029099

        public Вариант1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void initializeBtn_Click(object sender, EventArgs e) // CommandInit_Click
        {
            XBase = float.Parse(XbaseTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            XBaset = float.Parse(XbasetTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            koef = Axis2.Width / XBase;
            Ykoef = koef / 2;
            Dkoef = Axis2.Size.Height;
            if (clearChBox.Checked)
            {
                HairXes = new List<Point>();
                WolfXes = new List<List<Point>>();
                itterationcounter = 0;
            }
            else
            {
                itterationcounter++;
            }
            WolfXes.Add(new List<Point>());
            t = 0;
            Xz = 1; 
            Xv = 0;
            if (xdeviationTxtBox.Text == ""){
                DeltaX = 0.01F;
            }
            else
            {
                DeltaX = float.Parse(xdeviationTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            }
            Vzx = 1;
            Vv0 = float.Parse(Vv0TxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            Vv1 = float.Parse(VlimTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (Vv0 < 0) { Vv0 = -Vv0; }
            if (Vv1 < 0) { Vv1 = -Vv1; }
            Vvx = Vv1 - (Vv1 - Vv0) * (1 + t - Xv);
            if (Vv0 > Vv1)
            {
                VvMax = Vv0;
            }
            else
            {
                VvMax = Vv1;
            }
            dt = float.Parse(DtTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            ///Drawing axis///
<<<<<<< HEAD
            Axis1InitDrawFlag = true;
            Axis2InitDrawFlag = true;
            Refresh();
        }

        private void Вариант1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void CalculateBtn_Click(object sender, EventArgs e)// пересчитывает по заданной
        // скорости зайца и начальной дистанции безразмерное время погони в реальное
        {
            r0Real = float.Parse(startdistanceTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            vZReal = float.Parse(bunnyspeedTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (vZReal == 0)
            {
                return;
            }
            chasetimesecTxtBox.Text = Convert.ToString(t * Math.Round(r0Real / vZReal, 2));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            t = t + dt; //time
            XvOld = Xv;
            XzOld = Xz;
            Xz = 1 + t;
            Xv = Xv + Vvx * dt;
            Vvx = Vv1 - (Vv1 - Vv0) * (1 + t - Xv);
            if (Vvx < 0) { Vvx = 0; }
            if (Vvx > VvMax) { Vvx = VvMax; }
            r = Xz - Xv;
            WolfXes[itterationcounter].Add(new Point(Convert.ToInt32((t - dt) * koef * 2), Convert.ToInt32(Dkoef - XvOld * Ykoef)));
            HairXes.Add(new Point(Convert.ToInt32((t - dt) * koef * 2), Convert.ToInt32(Dkoef - XzOld * Ykoef)));
            Axis1update = true;
            Axis2update = true;
=======
            
            drawAxis = true;
>>>>>>> 595bb0288e30f30d71a2f86f8913bbc4f2029099
            Refresh();

        }

        private void Вариант1_Load(object sender, EventArgs e)
        {
            Axis1.Paint += new PaintEventHandler(panel3_Paint);
            Axis2.Paint += new PaintEventHandler(panel4_Paint);
        }

        private void timerBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void DtTxtBox_TextChanged(object sender, EventArgs e)
        {
            //dt = float.Parse(DtTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
        }

<<<<<<< HEAD
        private void startBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Axis1update = false;
            Axis2update = false;
        }

        private void variant2Btn_Click(object sender, EventArgs e)
        {
            Вариант2 f3 = new Вариант2();
            f3.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e) // Axis 1
=======
        private void initAxis()
        {
            /// инициализирует параметры систем координат и перерисовывает оси 
        }
        private void AxisDraw(PaintEventArgs e)
>>>>>>> 595bb0288e30f30d71a2f86f8913bbc4f2029099
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(Color.Black);
            Pen p = new Pen(br, 3);
<<<<<<< HEAD
            g.DrawLine(p, 0, Axis1.Size.Height / 2, 976, Axis1.Size.Height / 2);
            if (Axis1InitDrawFlag)
            {
                p.Color = Color.Red;
                g.DrawEllipse(p, Xz * koef, 30, R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, Xv * koef, 30, R * 2, R * 2);
                chasetimesecTxtBox.Text = "";
                chasetimeTxtBox.Text = "";
                Axis1InitDrawFlag = false;

            }
            
            if (Axis1update)
            {
                p.Color = Color.Red;
                g.DrawEllipse(p, Xz * koef, 30, R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, Xv * koef, 30, R * 2, R * 2);

            }
            if (Axis1last)
            {
                p.Color = Color.Red;
                g.DrawEllipse(p, Xz * koef, 30, R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, Xv * koef, 30, R * 2, R * 2);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e) // Axis 2
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(Color.Black);
            Pen p = new Pen(br, 2);
            if (Axis2InitDrawFlag)
            {
                p.Color = Color.Red;
                for (int i = 0; i < HairXes.Count - 1; i++)
                {
                    g.DrawLine(p, HairXes[i].X, HairXes[i].Y, HairXes[i + 1].X, HairXes[i + 1].Y);
                }
                g.DrawLine(p, (t - dt) * koef * 2, Dkoef - XzOld * Ykoef, t * koef * 2, Dkoef - Xz * Ykoef);
                p.Color = Color.Black;
                for (int i = 0; i < WolfXes.Count; i++)
                {
                    for (int j = 0; j < WolfXes[i].Count - 1; j++)
                    {
                        g.DrawLine(p, WolfXes[i][j].X, WolfXes[i][j].Y, WolfXes[i][j + 1].X, WolfXes[i][j + 1].Y);
                    }
                }
                g.DrawLine(p, (t - dt) * koef * 2, Dkoef - XvOld * Ykoef, t * koef * 2, Dkoef - Xv * Ykoef);
                Axis2InitDrawFlag = false;
            }
            if (Axis2update)
            {
                if (Math.Abs(Xv - Xz) < DeltaX || t > XBaset)
                {
                    timer1.Enabled = false;
                    Axis1update = false;
                    Axis2update = false;
                    Axis1last = true;
                }
                p.Color = Color.Red;
                for (int i = 0; i < HairXes.Count - 1; i++)
                {
                    g.DrawLine(p, HairXes[i].X, HairXes[i].Y, HairXes[i + 1].X, HairXes[i + 1].Y);
                }
                g.DrawLine(p, (t - dt) * koef * 2, Dkoef - XzOld * Ykoef, t * koef * 2, Dkoef - Xz * Ykoef);

                p.Color = Color.Black;
                for (int i = 0; i < WolfXes.Count; i++)
                {
                    for (int j = 0; j < WolfXes[i].Count - 1; j++)
                    {
                        g.DrawLine(p, WolfXes[i][j].X, WolfXes[i][j].Y, WolfXes[i][j + 1].X, WolfXes[i][j + 1].Y);
                    }
                }
                g.DrawLine(p, (t - dt) * koef * 2, Dkoef - XvOld * Ykoef, t * koef * 2, Dkoef - Xv * Ykoef);
                if (rChBox.Checked)
                {
                    p.Color = Color.Green;
                    g.DrawLine(p, t * koef * 2, Dkoef - Xv * Ykoef, t * koef * 2, Dkoef - Xz * Ykoef);
                }
                chasetimeTxtBox.Text = Convert.ToString(Math.Round(t, 2));

            }
        }

        private void rChBox_CheckedChanged(object sender, EventArgs e)
=======
            g.DrawLine(p, 0, 180, 955, 180);
            g.DrawLine(p, 477, 0, 477, 477);
        }

        private void DisplayDraw(PaintEventArgs e)
        {

        }

        private void Вариант1_Paint(object sender, PaintEventArgs e)
        {
            if (drawAxis)
            {
                AxisDraw(e);
            }
            
        }

        private void CalculateBtn_Click(object sender, EventArgs e)// пересчитывает по заданной
        // скорости зайца и начальной дистанции безразмерное время погони в реальное
        {
            r0Real = Convert.ToDouble(startdistanceTxtBox.Text);
            vZReal = Convert.ToDouble(bunnyspeedTxtBox.Text);
            if (vZReal == 0)
            {
                return;
            }
            chasetimesecTxtBox.Text = Convert.ToString(Math.Round(r0Real / vZReal, 2));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Вариант1_Load(object sender, EventArgs e)
>>>>>>> 595bb0288e30f30d71a2f86f8913bbc4f2029099
        {

        }
    }
}
