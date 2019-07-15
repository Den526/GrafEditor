using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GrafEditor
{
    public partial class frMainForm
    {
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
                for (int t = 0; t < flt[x].points.Count; t++)
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

    }
}
