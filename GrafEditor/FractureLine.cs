using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GrafEditor
{
    class FractureLine
    {
        public Pen pen {get; set;};//перо
        public List<Point> tchk {get; set;} = new List<Point>();//массив точек
    }
}
