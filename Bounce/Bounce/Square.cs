using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Bounce
{
    class Square
    {
        int SquareXV = 0, SquareYV = 0;
        int SquareX = 0, SquareY = 0;
        public Square(int x, int y, int xv)
        {
            SquareX = x;
            SquareY = y;
            SquareXV = xv;
        }
        public void setLocationX(int X)
        {
            SquareX = X;
        }
        public void setLocationY(int Y)
        {
            SquareY = Y;
        }
        public int getSquareX()
        {
            return SquareX;
        }
        public int getSquareY()
        {
            return SquareY;
        }
        public void setSquareXV(int X)
        {
            SquareXV = X;
        }
        public void setSquareYV(int Y)
        {
            SquareYV = Y;
        }
        public int getSquareXV()
        {
            return SquareXV;
        }
        public int getSquareYV()
        {
            return SquareYV;
        }
        public void Draw(Graphics g)
        {
            Brush b = Brushes.OrangeRed;

            g.FillRectangle(b, (int)SquareX, (int)SquareY, 50, 50);
        }
    }
}
