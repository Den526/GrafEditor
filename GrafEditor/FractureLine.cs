﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GrafEditor
{
    class FractureLine
    {
        private Pen pen;    //перо
        private string _Name; //отображаемое имя линии
        public string Name
        {
            get {return _Name; }
            set {_Name = value;}
        }
        public Pen Pero
        {
            get {return pen;  }
            set {pen = value; }
        }
        //массив точек
        private List<Point> tchk = new List<Point>();
        //Список точек
        public List<Point> points
        {
            get
            {
                return tchk;
            }
            set
            {
                tchk = value;
            }
        }
        //Двигаем точки всей линии
        public void Move(int x, int y)
        {
            for (int i = 0; i < tchk.Count; i++)
            {
                this.tchk[i] = new Point(this.tchk[i].X + x, this.tchk[i].Y + y);
            }
        }
        //добавить точку
        public void AddPoint(Point p)
        {
            this.tchk.Add(p);
        }
        //копия линии
        public FractureLine Clone()
        {
            FractureLine f = new FractureLine();
            f.pen = (Pen)this.pen.Clone();
            for (int i = 0; i < this.points.Count; i++)
            {
                f.points.Add(this.points[i]);
            }
            return f;
        }
        //поиск вхождения точки в линию и возврат номера первой точки отрезка на котором находится указанная точка
        public int InsidePoint(Point p)
        {
            Point p1;
            Point p2;
            int dx = 0;
            int dy = 0;
            int ex = 0;
            int ey = 0;
            for (int ti = 0; ti < this.points.Count - 1; ti++)
            {
                p1 = this.points[ti];
                p2 = this.points[ti + 1];
                dx = p2.X - p1.X;
                dy = p2.Y - p1.Y;
                ex = p.X - p1.X;
                ey = p.Y - p1.Y;
                //Векторное произведение векторов p1-p и p1-p2 (100 - допустимая уставка отклонения, Note! для тонких линий - важна точность наведения)
                //для более тонких линий - уставка промаха должна быть побольше, для жирных линий - поменьше
                if (Math.Abs(ey * dx - ex * dy) <= (100 - 10 * this.pen.Width)  * this.pen.Width)
                {
                    //вычисляем скалярное прозведение p-p1 и p-p2, если оно <= 0 - точка принадлежит отрезку и вернуть номер
                    if ((p1.X - p.X) * (p2.X - p.X) + (p1.Y - p.Y) * (p2.Y - p.Y) <= 0) return ti;
                }
            }
            return -1; //если не надйено - вернуть -1
        }
    }
}
