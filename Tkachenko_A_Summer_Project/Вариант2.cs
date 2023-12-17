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
using System.Windows.Forms.DataVisualization.Charting;

namespace Tkachenko_A_Summer_Project
{
    public partial class Вариант2 : Form
    {
        ///wolf
        float Xv; // x - coordinate
        float XvOld; // x - coordinate
        float Vvx; // x - компонента скорости
        float Vv0; // начальная скорости

        ///hair
        float Xz; // x - coordinate
        float XzOld; // x - coordinate
        float Yz; // y - coordinate
        float YzOld; // y - coordinate
        float Vzx; // x - компонента скорости
        float r; // расстояние между волком и зайцем

        float r0Real; // размерное расстояние между волком и зайцем

        float vZReal; // размерная скорость зайца
        float XBase;
        float XBaset;
        float t; // время
        float dt; // шаг по времени
        float DeltaX; // погрешность вычисления координат зайца и волка

        float Yv; // координата
        float Vv; // скорость
        float YvOld;
        float Vvy;

        //hair for circle
        float Vz; // скорость
        float Vzy; // y - компонента скорости
        float Omegaz; // угловая скорость
        float Fi; // угол направления движения

        float IntdtCount;
        float Intdrel;
        float Rz;

        bool InitializeAnimalPaint = false;
        bool InitializeTragectoryPaint = false;
        bool UpdateAnimal = false;
        bool UpdateTragectory = false;
        bool finish = false;

        float koef; // change according to mode
        float Dkoef; // change according to mode

        List<Point> HairXes = new List<Point>(); // change to 2 lists
        List<List<Point>> WolfXes = new List<List<Point>>();
        int itterationcounter = -1;

        int R = 15;
        Random rnd = new Random();

        public Вариант2()
        {
            InitializeComponent();
        }

        private void Вариант2_Load(object sender, EventArgs e)
        {
            AnimalPanel.Paint += new PaintEventHandler(AnimalPanel_Paint);
            TragectoryPanel.Paint += new PaintEventHandler(TragectoryPanel_Paint);
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            r0Real = float.Parse(startdistanceTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            vZReal = float.Parse(bunnyspeedTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (vZReal == 0)
            {
                return;
            }
            chasetimesecTxtBox.Text = Convert.ToString(Math.Round((t * (r0Real / vZReal)), 2));
        }

        private void relLabel_Click(object sender, EventArgs e)
        {

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void variant1Btn_Click(object sender, EventArgs e)
        {
            Вариант1 f2 = new Вариант1();
            f2.Show();
            this.Close();
        }

        private void initializeBtn_Click(object sender, EventArgs e)
        {
            XBase = float.Parse(XbaseTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            XBaset = float.Parse(XbasetTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            koef = AnimalPanel.Width / XBase;
            Dkoef = AnimalPanel.Size.Height / 2;
            finish = false;


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
            Xz = 0;
            Yz = 4;
            Rz = Yz;
            Vz = 1;
            Vzx = 1;
            Vzy = 1;
            Vv = float.Parse(Vv0TxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            DeltaX = float.Parse(xdeviationTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            Intdrel = float.Parse(DiametrTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat); // before zigzags counter
            if (forwardRBtn.Checked)
            {
                Dkoef = 0;
                Yz = 2;
                XvTxtBox.Text = "0";
                YvTxtBox.Text = "0";
            }
            else if (circleRBtn.Checked)
            {
                Omegaz = Vz / Rz;
            }
            Xv = float.Parse(XvTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            Yv = float.Parse(YvTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (xdeviationTxtBox.Text == "")
            {
                DeltaX = 0.05f;
            }
            else
            {
                DeltaX = float.Parse(xdeviationTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (Vv0 < 0)
            {
                Vv0 = -Vv0;
            }
            dt = float.Parse(DtTxtBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            InitializeAnimalPaint = true;
            InitializeTragectoryPaint = true;
            Refresh();
        } // CommandInit

        private void AnimalPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(Color.Red);
            Pen p = new Pen(br, 3);
            if (InitializeAnimalPaint)
            {
                g.DrawEllipse(p, (float)(Xz * koef + Dkoef), (float)(Yz * koef + Dkoef), R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, (float)(Xv * koef + Dkoef), (float)(Yv * koef + Dkoef), R * 2, R * 2);
                chasetimesecTxtBox.Text = "";
                chasetimeTxtBox.Text = "";
                InitializeAnimalPaint = false;
            }
            if (UpdateAnimal)
            {
                g.DrawEllipse(p, (float)(Xz * koef + Dkoef), (float)(Yz * koef + Dkoef), R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, (float)(Xv * koef + Dkoef), (float)(Yv * koef + Dkoef), R * 2, R * 2);
            }
            if (finish)
            {
                g.DrawEllipse(p, (float)(Xz * koef + Dkoef), (float)(Yz * koef + Dkoef), R, R);
                p.Color = Color.Black;
                g.DrawEllipse(p, (float)(Xv * koef + Dkoef), (float)(Yv * koef + Dkoef), R * 2, R * 2);
            }

        }

        private void TragectoryPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(Color.Black);
            Pen p = new Pen(br, 2);
            // Draw Axes
            if (finish)
            {
                p.Color = Color.Red;
                for (int i = 0; i < HairXes.Count - 1; i++)
                {
                    g.DrawLine(p, HairXes[i].X, HairXes[i].Y, HairXes[i + 1].X, HairXes[i + 1].Y);
                }
                g.DrawLine(p, XzOld * koef + Dkoef, YzOld * koef + Dkoef, Xz * koef + Dkoef, Yz * koef + Dkoef);

                p.Color = Color.Black;
                for (int i = 0; i < WolfXes.Count; i++)
                {
                    for (int j = 0; j < WolfXes[i].Count - 1; j++)
                    {
                        g.DrawLine(p, WolfXes[i][j].X, WolfXes[i][j].Y, WolfXes[i][j + 1].X, WolfXes[i][j + 1].Y);
                    }
                }
                g.DrawLine(p, XvOld * koef + Dkoef, YvOld * koef + Dkoef, Xv * koef + Dkoef, Yv * koef + Dkoef);

            }
            if (UpdateTragectory)
            {
                p.Color = Color.Red;
                for (int i = 0; i < HairXes.Count - 1; i++)
                {
                    g.DrawLine(p, HairXes[i].X, HairXes[i].Y, HairXes[i + 1].X, HairXes[i + 1].Y);
                }
                g.DrawLine(p, XzOld * koef + Dkoef, YzOld * koef + Dkoef, Xz * koef + Dkoef, Yz * koef + Dkoef);

                p.Color = Color.Black;
                for (int i = 0; i < WolfXes.Count; i++)
                {
                    for (int j = 0; j < WolfXes[i].Count - 1; j++)
                    {
                        g.DrawLine(p, WolfXes[i][j].X, WolfXes[i][j].Y, WolfXes[i][j + 1].X, WolfXes[i][j + 1].Y);
                    }
                }
                g.DrawLine(p, XvOld * koef + Dkoef, YvOld * koef + Dkoef, Xv * koef + Dkoef, Yv * koef + Dkoef);
                chasetimeTxtBox.Text = Convert.ToString(Math.Round(t, 2));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAnimal = true;
            UpdateTragectory = true;
            t = t + dt; // time
            IntdtCount = IntdtCount + 1;
            if (IntdtCount > Intdrel)
            {
                IntdtCount = 0;
            }
            XzOld = Xz;
            YzOld = Yz;
            XvOld = Xv;
            YvOld = Yv;
            if ((Math.Abs(Xv - Xz) < DeltaX) && (Math.Abs(Yv - Yz) < DeltaX))
            {
                timer1.Enabled = false;
                chasetimesecTxtBox.Text = chasetimeTxtBox.Text;
                UpdateAnimal = false;
                UpdateTragectory = false;
                finish = true;
            }
            r = (float)Math.Sqrt((Xz - Xv) * (Xz - Xv) + (Yz - Yv) * (Yz - Yv));
            Vvx = Vv * (Xz - Xv) / r;
            Vvy = Vv * (Yz - Yv) / r;
            if (forwardRBtn.Checked)
            {
                Xz = Vz * t;
            }
            else if (circleRBtn.Checked)
            {
                Xz = Rz * (float)(Math.Cos(Omegaz * t + Math.PI / 2));
                Yz = Rz * (float)Math.Sin(Omegaz * t + Math.PI / 2);
            }
            else if (randomRBtn.Checked)
            {
                if (IntdtCount == 0)
                {
                    // changing hairs direction
                    Fi = (float)(rnd.Next() * 2 * Math.PI); // ???
                }
                Vzx = Vz * (float)Math.Cos(Fi);
                Vzy = Vz * (float)Math.Sin(Fi);
                Xz = Xz + Vzx * dt;
                Yz = Yz + Vzy * dt;
            }
            Xv = Xv + Vvx * dt;
            Yv = Yv + Vvy * dt;
            // Drawing the animals
            HairXes.Add(new Point(Convert.ToInt32((XzOld) * koef + Dkoef), Convert.ToInt32(YzOld * koef + Dkoef)));
            WolfXes[itterationcounter].Add(new Point(Convert.ToInt32(XvOld * koef + Dkoef), Convert.ToInt32(YvOld * koef + Dkoef)));
            Refresh();
        }
    }
}
