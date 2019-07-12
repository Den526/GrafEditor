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
        Point PMoveBegin; // точки для рисования простой линии
        Point PMoveEnd;
        List<FractureLine> FL = new List<FractureLine>();
        //List<FractureLine> FL_tmp = new List<FractureLine>();
        int IndActiveFL = -1;  //индекс активной линии, с которой в данный момент работает пользователь
        int IndActivePoint = -1;  //индекс активной точки, с которой в данный момент работает пользователь
        float ThicknesLine = 1;  //толщина активной линии
        Color ColorLine = Color.Black; //цвет активной линии
        bool flMoveLine = false;
        bool flAddLine = false;
        bool flAddPoint = false;

        private void pbMainGrafWin_Click(object sender, EventArgs e)
        {
            
        }

        private void pbMainGrafWin_MouseClick(object sender, MouseEventArgs e)
        {
            //событие клика = MouseUP
            if (e.Button == MouseButtons.Left)
            {
                if (IndActiveFL >= 0)
                {
                    if (flMoveLine)
                    {
                        FL[IndActiveFL].Move(e.X - PMoveBegin.X, e.Y - PMoveBegin.Y); //переместить всю ломаную на разницу перемещения
                        flMoveLine = false;  //отпустили мышь - хватит двигать
                        pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);  //рисуем обновленную картину
                    }
                    if (flAddPoint)
                    {
                        //добавляем новую точку и рисуем новый отрезок
                        Point ptmp = new Point(e.X, e.Y);
                        FL[IndActiveFL].AddPoint(ptmp);
                        pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //закончить рисовать линию
                flAddPoint = false;
                pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
            }

        }
        private void pbMainGrafWin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IndActiveFL >= 0)
                {
                    PMoveBegin = new Point(e.X, e.Y);
                    flMoveLine = true;
                    //pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                }
            }
        }
        private void pbMainGrafWin_MouseMove(object sender, MouseEventArgs e)
        {
            FractureLine FL_tmp = new FractureLine();   //временная линия
            if (IndActiveFL >= 0)
            {
                if (flMoveLine)
                {
                    PMoveEnd = new Point(e.X, e.Y);
                    FL_tmp.Pero = new Pen(Color.Silver, 1);
                    for (int ti = 0; ti < FL[IndActiveFL].points.Count; ti++)
                    {
                        FL_tmp.AddPoint(FL[IndActiveFL].points[ti]);
                    } 
                    FL_tmp.Move(e.X - PMoveBegin.X, e.Y - PMoveBegin.Y);
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                    pbMainGrafWin.Image = PaintBitmap((Bitmap)pbMainGrafWin.Image, FL_tmp);
                }
                if (flAddPoint)
                {
                    if (FL[IndActiveFL].points.Count > 0)
                    {
                        FL_tmp.Pero = new Pen(Color.Silver, 1);
                        IndActivePoint = FL[IndActiveFL].points.Count - 1;
                        FL_tmp.AddPoint(FL[IndActiveFL].points[IndActivePoint]);
                        FL_tmp.AddPoint(new Point(e.X, e.Y));
                        pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);

                        pbMainGrafWin.Image = PaintBitmap((Bitmap)pbMainGrafWin.Image, FL_tmp);
                    }
                }

            }
            lblCoordPB1.Text = e.X.ToString() + " : " + e.Y.ToString();  //показать координаты мышки в рабочей области
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //создать новую линию
            FractureLine fl_tmp = new FractureLine();
            flAddLine = true;
            flAddPoint = true;
            fl_tmp.Pero = p1;
            FL.Add(fl_tmp);
            IndActiveFL = FL.Count - 1;
            //FL[IndActiveFL].Pero = p1;
            listBox1.Items.Add("Линия" + IndActiveFL.ToString());
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        //*********************************************** Прорисовка полная *****************************************//
        private Bitmap PaintBitmap(int width, int height, List<FractureLine> flt)
        {
            //русуем массив линий
            Bitmap btmBack = new Bitmap(width, height);
            Graphics grBack = Graphics.FromImage(btmBack);

            Point p1 = new Point();
            Point p2 = new Point();
            Pen P;
            Pen Ptch;
            
            for (int x = 0; x < flt.Count; x++)
            {
                P = flt[x].Pero;
                Ptch = new Pen(Color.LimeGreen, (float)(flt[x].Pero.Width + 1));
                if (flt[x].points.Count > 0) p1 = flt[x].points[0];  //начало рисования
                for(int t = 0; t < flt[x].points.Count; t++)
                {
                    p2 = flt[x].points[t];
                    Rectangle r1 = new Rectangle(new Point(p2.X - (int)(Ptch.Width / 2), p2.Y - (int)(Ptch.Width / 2)), new Size((int)Ptch.Width, (int)Ptch.Width));
                    
                    grBack.DrawLine(P, p1, p2);
                    grBack.DrawEllipse(Ptch, r1);
                    p1 = p2;
                }
            }
            return btmBack;
        }
        //*********************************************** Дорисовка *****************************************//
        private Bitmap PaintBitmap(Bitmap btmBack, FractureLine flt)
        {
            //добавить на существующий битмап одну линию
            //нужна для временного отображения ожидаемого результата (рисования отрезка. Или 2-х отрезков, если переносится промежуточная точка
            Graphics grBack = Graphics.FromImage(btmBack);

            Point p1 = new Point();
            Point p2 = new Point();
            Pen P;
            Pen Ptch;

            //int mx = 1; //Масштаб по x и y
            //int my = 1;

            //for (int x = 0; x < flt.Count; x++)
            {
                P = flt.Pero;
                Ptch = new Pen(Color.LimeGreen, (float)(flt.Pero.Width + 1));
                if (flt.points.Count > 0) p1 = flt.points[0];  //начало рисования
                for (int t = 0; t < flt.points.Count; t++)
                {
                    p2 = flt.points[t];
                    Rectangle r1 = new Rectangle(new Point(p2.X - (int)(Ptch.Width / 2), p2.Y - (int)(Ptch.Width / 2)), new Size((int)Ptch.Width, (int)Ptch.Width));

                    grBack.DrawLine(P, p1, p2);
                    grBack.DrawEllipse(Ptch, r1);
                    p1 = p2;
                }
            }
            return btmBack;
        }
        //******************************************************************************************************//

        private void SelectColor_Click(object sender, EventArgs e)
        {
            //выбор цвета линии кликами по лейблам
            p1 = new Pen(label11.BackColor, Convert.ToInt16(cbThicknes.Text));
            toolStripStatusLabel1.BackColor = label11.BackColor;
            if (IndActiveFL >= 0) FL[IndActiveFL].Pero = p1;
        }

        private void cmdSelectColor_Click(object sender, EventArgs e)
        {
            //выбор цвета линии из диалогового окна
            ColorDialog SelColorDialog = new ColorDialog();
            DialogResult result = SelColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ColorLine = SelColorDialog.Color;

                p1 = new Pen(ColorLine, ThicknesLine);
                toolStripStatusLabel1.BackColor = SelColorDialog.Color;
                if (IndActiveFL >= 0)
                {
                    FL[IndActiveFL].Pero = p1;
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                }
            }
        }

        private void cbThicknes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThicknesLine = (float)Convert.ToDecimal(cbThicknes.Text);
            p1 = new Pen(ColorLine, ThicknesLine);
            if (IndActiveFL >= 0)
            {
                FL[IndActiveFL].Pero = p1;
                pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p1 = new Pen(ColorLine, ThicknesLine);
                
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndActiveFL = listBox1.SelectedIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //удалиние линии
            if (IndActiveFL >= 0)
            {
                DialogResult res = MessageBox.Show("Будет удалена следующая фигура: " + listBox1.Items[IndActiveFL].ToString() + ". Вы действительно хотите удалить?", "Удаление объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    //FL[IndActiveFL].Pero.;
                    FL[IndActiveFL].points.Clear();
                    FL.RemoveAt(IndActiveFL);
                    listBox1.Items.RemoveAt(IndActiveFL);
                    IndActiveFL = -1;
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                }
            }
            else
            {
                MessageBox.Show("Не выбранна фигура. Воспользуйтесь списком фигур в левой области или кликнете по фигуре левой кнопкой в рабочей области", "Удаление объекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flAddPoint = true;
            flMoveLine = false;
        }

    }
}
