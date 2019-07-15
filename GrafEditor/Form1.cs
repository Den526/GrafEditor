using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GrafEditor
{
    public partial class frMainForm : Form
    {
        public frMainForm()
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
        bool flFindLine = false;

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
                flAddLine = false;
                pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
            }

        }
        private void pbMainGrafWin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IndActiveFL >= 0)
                {
                    //если закрыты флаги добавления или движения точки
                    if (!flAddPoint && !flMovePoint)
                    {
                        if (IndActiveFL >= 0 && IndActivePoint >= 0)
                        {
                            //Поиск линии и отрезка прошло в модуле MouseClick
                            //т.е. если индексы активных линии и точки не 0 - добавляем новую точку
                            FL[IndActiveFL].points.Insert(IndActivePoint+1, new Point(e.X, e.Y));
                            pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                        }
                    }
                }
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
                    if (!flAddPoint && !flMovePoint)
                    {
                        //ищем наведение мышкой на какую либо линию
                        for (int i = 0; i < FL.Count; i++)
                        {
                            IndActivePoint = FL[i].InsidePoint(new Point(e.X, e.Y));
                            if (IndActivePoint >= 0)
                            {
                                flMoveLine = true;
                                IndActiveFL = i;
                                listBox1.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void pbMainGrafWin_MouseMove(object sender, MouseEventArgs e)
        {
            //ищем наведение мышкой на какую либо линию и подсвечиваем
            if (!flAddLine && !flAddPoint && !flMoveLine && !flMovePoint)
                for (int i = 0; i < FL.Count; i++)
                {
                    int ti = FL[i].InsidePoint(new Point(e.X, e.Y));
                    if (ti >= 0)
                    {
                        lblTempX.BackColor = Color.LimeGreen;
                        listBox1.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        //listBox1.SelectedIndex = -1;
                        lblTempX.BackColor = Color.Coral;
                    }
                }
            //определяем наведение курсора на какую либо фигуру
            if (IndActiveFL >= 0)
            {
                FractureLine FL_tmp = new FractureLine();   //временная линия    
                FL_tmp.Pero = new Pen(Color.Silver, 1);
                if (flMoveLine && e.Button == MouseButtons.Left)
                {
                    //двигать линию
                    //созданаем копию из активной линии во временную линию
                    FL_tmp = FL[IndActiveFL].Clone();
                    FL_tmp.Pero = new Pen(Color.Silver, 1);

                    //таскаем временную линию за мышью
                    FL_tmp.Move(e.X - PMoveBegin.X, e.Y - PMoveBegin.Y);
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL);
                    pbMainGrafWin.Image = PaintBitmap((Bitmap)pbMainGrafWin.Image, FL_tmp);
                }
                if (flMovePoint)
                {//перемещаем точки в активной линии
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
                        //обработка перемещения последней точки
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
            fl_tmp.Name = "Линия" + (FL.Count+1).ToString();
            FL.Add(fl_tmp);
            IndActiveFL = FL.Count - 1;
            listBox1.Items.Add(fl_tmp.Name);

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
            //добавление новой точки в конец линии
            flAddPoint = true;
            flMoveLine = false;
            flMovePoint = false;
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            //обратока клика по меню Файл - Сохранить как...
            SaveFileDialog SFD = new SaveFileDialog();  //создаем диалоговое окно сохранения в файл
            SFD.Filter = "*.gve files (*.gve)|*.gve|All files (*.*)|*.*";
            DialogResult result = SFD.ShowDialog();     //открываем окно и ждем его завершения
            if (result == DialogResult.OK)              //если пользователь нажал Сохранить (или ОК)
            {
                FileInfo fi = new FileInfo(SFD.FileName);
                MessageBox.Show(fi.DirectoryName);
                if (Directory.Exists(fi.DirectoryName) && fi.Name != "" && fi.Name != String.Empty)
                {
                    //если папка сужествуюет и имя файла не пустое и не null
                    if (!SaveFile(fi.FullName)) MessageBox.Show("При сохранении файла произошла ошибка. Проверьте правильность указания пути и имени файла. Если эти данные верны и вы все равно видите эту ошибку - обратитесь к разработчику", "Ошибка сохранения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            //обратока клика по меню Файл - Открыть
            //готовим и открываем диалоговое окно открытие файла
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "*.gve files (*.gve)|*.gve|All files (*.*)|*.*";
            DialogResult result = OFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                //если пользователь нажал ОК, проверим на всякий сл. а существует ли такой файл
                FileInfo fi = new FileInfo(OFD.FileName);
                if (fi.Exists)
                {
                    if (!OpenFile(fi.FullName)) MessageBox.Show("При открытии файла произошла ошибка. Проверьте правильность указания пути и имени файла. Если эти данные верны и вы все равно видите эту ошибку - обратитесь к разработчику", "Ошибка открытия файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool SaveFile(string Path)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(SFD.FileName, FileMode.OpenOrCreate)))
                {
                    writer.Write("file graphic vector editor GrafEdit");
                    writer.Write(FL.Count); //int - кол-во линий
                    //перебираем линии с изломами
                    foreach (FractureLine fl in FL)
                    {
                        writer.Write(fl.Name);       //str - Имя
                        writer.Write("InfoPen;");    //str - Перо
                        writer.Write(fl.Pero.Width); //float - толщина пера
                        writer.Write(fl.Pero.Color.ToArgb());  //int32 - цвет пера
                        writer.Write("InfoPoints;");   //str   - инфо о точках
                        writer.Write(fl.points.Count); //int   - сколько всего точек в линии с изломами
                        foreach (Point p1 in fl.points)
                        {
                            writer.Write(p1.X);       //int - коор.X
                            writer.Write(p1.Y);       //int - коор.Y
                        }
                    }
                    writer.Close();
                }
                return true;
            }
            catch { return false; }

        }
        private bool OpenFile(string Path)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.Open)))
                {
                    string info = reader.ReadString(); //вводное инфо о файле
                    int countLine = reader.ReadInt32(); //кол-во строк
                    FL.Clear();                         //чистим существующий список линий
                    for (int i = 0; i < countLine; i++)
                    {
                        //читаем с файла линии во временную линию
                        FractureLine fl_tmp = new FractureLine();
                        fl_tmp.Name = reader.ReadString();

                        info = reader.ReadString();  //инфо о пере для  линии
                        float PenWidth = reader.ReadSingle();  //указываем значение пера - толщина, затем цвет
                        Color PenColor = Color.FromArgb(reader.ReadInt32());
                        fl_tmp.Pero = new Pen(PenColor, PenWidth);

                        info = reader.ReadString();  //метка о начале инфо о точках
                        int countPoint = reader.ReadInt32(); //кол-во точек
                        for (int ti = 0; ti < countPoint; ti++)
                        {
                            //координаты, и добавляем точку
                            int x = reader.ReadInt32();
                            int y = reader.ReadInt32();
                            Point p1 = new Point(x, y);
                            fl_tmp.AddPoint(p1);
                        }
                        listBox1.Items.Add(fl_tmp.Name); //добавляем в список линию
                        FL.Add(fl_tmp);                  //добавляем линию в List
                    }
                    pbMainGrafWin.Image = PaintBitmap(pbMainGrafWin.Width, pbMainGrafWin.Height, FL); //рисуем в PictBoxe
                    reader.Close(); //закрываем файл
                }
                return true;
            }
            catch { return false; }
        }
    }
}
