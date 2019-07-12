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
        int IndActiveFL = -1;  //индекс активной линии, с которой в данный момент работает пользователь
        int IndActivePoint = -1;  //индекс активной точки, с которой в данный момент работает пользователь
        float ThicknesLine = 1;  //толщина активной линии
        Color ColorLine = Color.Black; //цвет активной линии
        
        bool flMoveLine = false;  //флаг разрешения перемещения линии
        bool flMovePoint = false; //флаг разрешения перемещения точки
        bool flAddLine = false;  //флаг добавления линии
        bool flAddPoint = false; //флаг добавления точки

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
                    if (flMovePoint)
                    {
                        FL[IndActiveFL].points[IndActivePoint] = new Point(e.X, e.Y);
                        flMovePoint = false;
                        pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
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
                    for (int ti = 0; ti < FL.Count; ti++)
                    {
                        int dx = 0; int dy = 0;
                        for (int pi = 0; pi < FL[ti].points.Count; pi++)
                        {
                            dx = e.X - FL[ti].points[pi].X;
                            dy = e.Y - FL[ti].points[pi].Y;
                            //будем считать что попали в точку, даже если есть отклонения в 1 пиксель в любую сторону
                            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
                            {
                                IndActiveFL = ti;
                                IndActivePoint = pi;
                                flMovePoint = true;
                                break;
                            }
                        }
                    }
                    if (!flMovePoint) flMoveLine = true;
                    //pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                    
                }
            }
        }
        private void pbMainGrafWin_MouseMove(object sender, MouseEventArgs e)
        {
            FractureLine FL_tmp = new FractureLine();   //временная линия
            if (IndActiveFL >= 0)
            {
                FL_tmp.Pero = new Pen(Color.Silver, 1);
                if (flMoveLine)
                {
                    //PMoveEnd = new Point(e.X, e.Y);

                    for (int ti = 0; ti < FL[IndActiveFL].points.Count; ti++)
                    {
                        FL_tmp.AddPoint(FL[IndActiveFL].points[ti]);
                    } 
                    FL_tmp.Move(e.X - PMoveBegin.X, e.Y - PMoveBegin.Y);
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                    pbMainGrafWin.Image = PaintBitmap((Bitmap)pbMainGrafWin.Image, FL_tmp);
                }
                if (flMovePoint)
                {
                    if (IndActivePoint == 0)
                    {
                        //обработка перемещения первой точки
                        FL_tmp.AddPoint(new Point(e.X, e.Y));
                        FL_tmp.AddPoint(FL[IndActiveFL].points[IndActivePoint + 1]);
                    }
                    else if (IndActivePoint > 0 && IndActivePoint < FL[IndActiveFL].points.Count - 1)
                    {
                        //обработка перемещения излома
                        FL_tmp.AddPoint(FL[IndActiveFL].points[IndActivePoint - 1]);
                        FL_tmp.AddPoint(new Point(e.X, e.Y));
                        FL_tmp.AddPoint(FL[IndActiveFL].points[IndActivePoint + 1]);
                    }
                    else if (IndActivePoint == FL[IndActiveFL].points.Count - 1)
                    {
                        //обработка перемещения первой точки
                        FL_tmp.AddPoint(FL[IndActiveFL].points[IndActivePoint - 1]);
                        FL_tmp.AddPoint(new Point(e.X, e.Y));
                    }
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


        private void SelectColor_Click(object sender, EventArgs e)
        {
            //тут пока плохо работает
            //выбор цвета линии кликами по лейблам
            //p1 = new Pen(label11.BackColor, Convert.ToInt16(cbThicknes.Text));
            //toolStripStatusLabel1.BackColor = label11.BackColor;
            //if (IndActiveFL >= 0) FL[IndActiveFL].Pero = p1;
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
