using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GrafEditor
{
    class FractureLine
    {
        private Pen pen;    //перо
        public Pen Pero
        {
            get 
            {
                return pen;
            }
            set 
            {
                pen = value;
            }
        }
        //массив точек
        private List<Point> tchk = new List<Point>();
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

    }
}
