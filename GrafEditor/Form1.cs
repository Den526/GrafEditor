using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GrafEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Pen p1;       //перо
        Point PBegin; // точки для рисования простой линии
        Point PEnd;
        List<factureLine> FL = new List<factureLine>();
        int IndActiveFL = 0;  //индекс активной линии, с которой в данный момент работает пользователь

        private void pbMainGrafWin_Click(object sender, EventArgs e)
        {
            
        }

        private void pbMainGrafWin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IndActiveFL >= 0)
                {
                    //рисуем точку
                    Point ptmp = new Point(e.X, e.Y);

                    FL[IndActiveFL].tchk.Add(ptmp);
                    pbMainGrafWin.Image = PaintGraffics(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //закончить рисовать линию
                IndActiveFL = -1;
            }

        }
        private void pbMainGrafWin_MouseMove(object sender, MouseEventArgs e)
        {
            lblCoordPB1.Text = e.X.ToString() + " : " + e.Y.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            factureLine fl_tmp = new factureLine();
            FL.Add(fl_tmp);
            IndActiveFL = FL.Count - 1;
            listBox1.Items.Add("Линия" + IndActiveFL.ToString());
        }


        private Bitmap PaintGraffics(int width, int height, List<factureLine> flt)
        {
            Bitmap btmBack = new Bitmap(width, height);
            Graphics grBack = Graphics.FromImage(btmBack);

            Point p1 = new Point();
            Point p2 = new Point();
            Pen P = new Pen(Color.LimeGreen, 2);
            int xxx = width;  //Смещения по х и по y
            int yyy = height;
            int mx = 1; //Масштаб по x и y
            int my = 1;
            mx = 1;
            my = 1;

            
            int step = 0;
            for (int x = 0; x < flt.Count; x++)
            {
                P = flt[x].pen;
                for(int t = 0; t < flt[x].tchk.Count; t++)
                {
                    step++;

                    p2 = flt[x].tchk[t]; // new Point(Convert.ToInt32(step * mx), yyy - Convert.ToInt32(ltchk[x].Volume) * my);
                    grBack.DrawLine(P, p1, p2);
                    p1 = p2;
                        //if (x * mx > btmBack.Width)  
                }
            }
            return btmBack;
        }

        //класс ломаной линии
        class factureLine
        {
            public Pen pen = new Pen(Color.Black, 1);  //перо
            public List<Point> tchk = new List<Point>(); //массив точек

        }

        private void SelectColor_Click(object sender, EventArgs e)
        {
            p1 = new Pen(label11.BackColor, 2);
            toolStripStatusLabel1.BackColor = label11.BackColor;
        }

    }
}
