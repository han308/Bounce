using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Bounce
{
    class Ball
    {
        int ballXV = 3, ballYV = 0;
        int ballX = 0, ballY = 0;
        public Ball(int x, int y)
        {
            ballX = x;
            ballY = y;
        }
        public void setLocationX(int X)
        {
                ballX = X;
        }
        public void setLocationY(int Y)
        {
                ballY = Y;
        }
        public int getBallX()
        {
            return ballX;
        }
        public int getBallY()
        {
            return ballY;
        }
        public void setBallXV(int X)
        {
            ballXV = X;
        }
        public void setBallYV(int Y)
        {
            ballYV = Y;
        }
        public int getBallXV()
        {
            return ballXV;
        }
        public int getBallYV()
        {
            return ballYV;
        }
        public void Draw(Graphics g)
        {
            Brush b = Brushes.MidnightBlue;
            
            g.FillEllipse(b, (int)ballX, (int)ballY, 50, 50);
        }
    }
}
