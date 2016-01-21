using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Bounce
{
    public partial class Form1 : Form
    {
        Ball ball = new Ball(300, 200);
        int score = 0;
        List<Square> enemy = new List<Square>();
        //System.Windows.Forms.Timer tmr;
        System.Threading.Timer animationTimer = null;
        System.Threading.Timer createTimer = null;

        //int seconds;

        public Form1()
        {
            InitializeComponent();
            /*
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 3000;
            tmr.Tick += new EventHandler(create);
            tmr.Start();
             * */
            textBox1.Text = "Score:" + score;
            TimerCallback timercb = new TimerCallback(TimerCB);
            TimerCallback timercbCreate = new TimerCallback(TimerCBCreate);
            animationTimer = new System.Threading.Timer(timercb, null, 25, 40);
            createTimer = new System.Threading.Timer(timercbCreate, null, 25, 3000);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            //base.OnPaint(e);
            if (!this.DesignMode)
            {
                Graphics g = e.Graphics;
                ball.Draw(g);
                Graphics c = e.Graphics;
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].Draw(c);
                }
            }
        }
        private void create(object sender)
        {
            Random randomGenerator = new Random();
            enemy.Add(new Square(ClientSize.Width, randomGenerator.Next(50, ClientSize.Height - 100), randomGenerator.Next(-15, -3)));
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:    
                    ball.setBallYV(-30);
                    if (ball.getBallY() < 0)
                    {
                        ball.setLocationY(1);
                    }
                    break;
                case Keys.Left:
                    ball.setBallXV(ball.getBallXV() - 10);
                    if (ball.getBallX() < 0)
                    {
                        ball.setLocationX(1);
                    }
                    break;
                case Keys.Right:
                    ball.setBallXV(ball.getBallXV() + 10);
                    if(ball.getBallX() > ClientSize.Width)
                    {
                        ball.setLocationX(ClientSize.Width - 50);
                    }
                    break;
                case Keys.Space:
                    ball.setLocationX(300);
                    ball.setLocationY(200);
                    ball.setBallXV(3);
                    ball.setBallYV(0);
                    score = 0;
                    enemy.Clear();
                    animationTimer.Dispose();
                    createTimer.Dispose();
                    TimerCallback timercb = new TimerCallback(TimerCB);
                    TimerCallback timercbCreate = new TimerCallback(TimerCBCreate);
                    animationTimer = new System.Threading.Timer(timercb, null, 25, 40);
                    createTimer = new System.Threading.Timer(timercbCreate, null, 25, 3000);
                    this.Refresh();
                    break;

            }
        }
        protected void TimerCB(object o)
        {
            //Move the ball
            game_animate(o);
            //Redraw the form
            Invalidate();
        }
        protected void TimerCBCreate(object o)
        {
            //Create square
            create(o);
            //Redraw the form
            Invalidate();
        }
        private void game_animate(object o)
        {
            //ball
            /*
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = "X:" + ball.getBallX() + " Y:" + ball.getBallY(); }));
            }
            if (textBox2.InvokeRequired)
            {
                textBox2.Invoke(new MethodInvoker(delegate { textBox2.Text = "X:" + ClientSize.Width + " Y:" + ClientSize.Height; }));
            }
            if (textBox3.InvokeRequired)
            {
                textBox3.Invoke(new MethodInvoker(delegate { textBox3.Text = "X:" + ball.getBallXV() + " Y:" + ball.getBallYV(); }));
            }
             * */
            
            //X
            if (ball.getBallX() + ball.getBallXV() >= 0 && ball.getBallX() + ball.getBallXV() + 50 <= ClientSize.Width)
            {

                ball.setLocationX(ball.getBallX() + ball.getBallXV());
            }
            else if (ball.getBallX() + ball.getBallXV() < 0)
            {
                ball.setLocationX(0);
            }
            else
            {
                ball.setLocationX(ClientSize.Width - 50);
            }
            if (ball.getBallX() <= 0)
            {
                ball.setBallXV(-ball.getBallXV());
            }
            if (ball.getBallX() + 50 >= ClientSize.Width)
            {
                ball.setBallXV(-ball.getBallXV());
            }
            //Y
            if (ball.getBallY() + ball.getBallYV() > 0 && ball.getBallY() + ball.getBallYV() + 50 < ClientSize.Height)
            {
                ball.setLocationY(ball.getBallY() + ball.getBallYV());
            }
            else if (ball.getBallY() + ball.getBallYV() <= 0)
            {
                ball.setLocationY(0);
                ball.setBallYV(-(ball.getBallYV() + 4));
                /*
                if (textBox4.InvokeRequired)
                {
                    textBox4.Invoke(new MethodInvoker(delegate { textBox4.Text = "Speed: " + ball.getBallYV(); }));
                }
                 * */
            }
            else
            {
                ball.setLocationY(ClientSize.Height - 50);
                if (ball.getBallYV() - 4 < 4)
                {
                    friction();
                }
                else
                {
                    ball.setBallYV(-(ball.getBallYV() - 2));
                }
                /*
                if (textBox4.InvokeRequired)
                {
                    textBox4.Invoke(new MethodInvoker(delegate { textBox4.Text = "Speed: " + ball.getBallYV(); }));
                }
                 * */
            }
            if (ball.getBallY() + 50 < ClientSize.Height)
            {
                ball.setBallYV(ball.getBallYV() + 4);

            }
            //square
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].setLocationX(enemy[i].getSquareX() + enemy[i].getSquareXV());

                /*
                if (enemy[i].getSquareX() <= 0)
                {
                    enemy[i].setSquareXV(-enemy[i].getSquareXV());
                }
                if (enemy[i].getSquareX() + 50 >= ClientSize.Width)
                {
                    enemy[i].setSquareXV(-enemy[i].getSquareXV());
                }
                 * */
                //Y
                if (enemy[i].getSquareY() + enemy[i].getSquareYV() >= 0 && enemy[i].getSquareY() + enemy[i].getSquareYV() + 50 <= ClientSize.Height)
                {
                    enemy[i].setLocationY(enemy[i].getSquareY() + enemy[i].getSquareYV());
                }
                else if (enemy[i].getSquareY() + enemy[i].getSquareYV() < 0)
                {
                    enemy[i].setLocationY(0);
                }
                else
                {
                    enemy[i].setLocationY(ClientSize.Height - 50);
                }
                if (enemy[i].getSquareY() == 0)
                {
                    enemy[i].setSquareYV(-enemy[i].getSquareYV());
                }
                else if (enemy[i].getSquareY() + 50 == ClientSize.Height)
                {
                    enemy[i].setSquareYV(-enemy[i].getSquareYV());
                }

                if (enemy[i].getSquareY() + 50 < ClientSize.Height)
                {
                    enemy[i].setSquareYV(enemy[i].getSquareYV() + 4);
                }
            }
            //remove out of bound box and update score
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy[i].getSquareX() < -50)
                {
                    enemy.Remove(enemy[i]);
                    score++;
                    if (textBox1.InvokeRequired)
                    {
                        textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = "Score:" + score; }));
                    }
                }
            }
            collision();
            
        }
        private void collision()
        {
            foreach(Square square in enemy){
                if (((ball.getBallX() >= square.getSquareX() && ball.getBallX() <= square.getSquareX() + 50) 
                    || (ball.getBallX() + 50 >= square.getSquareX() && ball.getBallX() + 50 <= square.getSquareX() + 50))
                    && ((ball.getBallY() >= square.getSquareY() && ball.getBallY() <= square.getSquareY() + 50) 
                    || (ball.getBallY() + 50 >= square.getSquareY() && ball.getBallY() + 50 <= square.getSquareY() + 50)))
                    //|| ((ball.getBallX() + 50 >= square.getSquareX() && ball.getBallX() + 50 <= square.getSquareX() + 50)
                    //&& (ball.getBallY() + 50 >= square.getSquareY() && ball.getBallY() + 50 <= square.getSquareY() + 50)))
                {
                    gameOver();
                }
            }
        }
        private void friction()
        {
            ball.setBallYV(0);
            ball.setLocationY(ClientSize.Height - 50);
            if (ball.getBallXV() > 0)
            {
                ball.setBallXV((ball.getBallXV() - 1));
            }
            else if (ball.getBallXV() < 0)
            {
                ball.setBallXV((ball.getBallXV() + 1));
            }
            if (Math.Abs(ball.getBallXV()) < 1.1)
            {
                ball.setBallXV(0);
            }
        }
        public void gameOver()
        {
            animationTimer.Dispose();
            createTimer.Dispose();
        }
    }
}

