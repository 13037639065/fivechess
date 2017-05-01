using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public partial class machine : Form
    {
        int[,] chess = new int[20, 20];
        int[,] sorce = new int[20, 20];

        Point up_step = new Point(7, 7);
        int chesstotal;



        public machine()
        {
            InitializeComponent();
        }


        //  绘制棋盘+当前状态
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Form1_MaximumSizeChanged(sender,e);
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 1);
            Point start = new Point(30, 30);
            Point end = new Point(30, 450);
            // 棋盘 大小为 30 个像素
            for (int i = 0; i < 15; i++) // 
            {
                g.DrawLine(p, start, end);
                start.X += 30;
                end.X += 30;
            }
            start.X = 30;
            start.Y = 30;
            end.X = 450;
            end.Y = 30;
            for (int i = 0; i < 15; i++) // 
            {
                g.DrawLine(p, start, end);
                start.Y += 30;
                end.Y += 30;
            }
            Form1_Paint(sender, e); // 绘画棋子
        }
        private double dist(int x,int y,Point b) //计算两点距离
        {
            double ans=System.Math.Sqrt((double)(x-b.X)*(x-b.X)+(double)(y-b.Y)*(y-b.Y));
            return ans;
        }
        private Point getpos(Point pos) //获取与鼠标最近的棋子落点,返回值为像素坐标位置
        {
            Point ans=new Point();
            double dis=999999999;
            for (int i = 30; i <= 450; i += 30)
            {
                for (int j = 30; j <= 450; j += 30)
                {
                    double temp=dist(i, j, pos);
                    if (temp< dis)
                    {
                        ans.X = i;
                        ans.Y = j;
                        dis = temp;
                    }
                }
            }
            return ans;
        }
        private void drawchess(Point pos, int i)// 画棋子;0 代表白色棋子,1为黑色
        {
            Graphics g = panel1.CreateGraphics();
            Pen blackchess = new Pen(Color.Black, 1);
            Pen whitechess = new Pen(Color.White, 1);
            Brush blackbrush = new SolidBrush(Color.Black);
            Brush whitebrush = new SolidBrush(Color.White);
            Rectangle rec = new Rectangle(pos.X - 10, pos.Y - 10, 20, 20);
            if (i == 0)
            {
                g.DrawEllipse(whitechess, rec);
                g.FillEllipse(whitebrush, rec);
            }
            else
            {
                g.DrawEllipse(blackchess, rec);
                g.FillEllipse(blackbrush, rec);
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            
        }
        private bool judge(Point pos)
        {
            int x, y,now,tot;
            x = pos.X;
            y = pos.Y;
            now=chess[x,y];
            tot = 1;
            for (int i = x, j = y + 1; chess[i, j] == now; j++) tot++;
            for (int i = x, j = y - 1; chess[i, j] == now; j--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x+1, j = y ; chess[i, j] == now; i++) tot++;
            for (int i = x-1, j = y; chess[i, j] == now; i--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x+1, j = y + 1; chess[i, j] == now; i++,j++) tot++;
            for (int i = x-1, j = y - 1; chess[i, j] == now; i--,j--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x-1, j = y + 1; chess[i, j] == now; i--,j++) tot++;
            for (int i = x+1, j = y - 1; chess[i, j] == now; i++,j--) tot++;
            if (tot >= 5) return true;
            return false;
        }
        private void chess_init(object sender, EventArgs e)
        {
            this.MaximizeBox = false; 
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++) chess[i, j] = -1;
            chesstotal = 1;
            chess[8, 8] = 1;
            Point pos=new Point(8*30,8*30);
            drawchess(pos, 1);
           
        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {

        }
        private Point AIchess()
        {
            Point ans = new Point();
            int max_sorce=-1;
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    Point temp=new Point(i,j);
                    int lzq=judge_sorce(1, temp)+judge_sorce(0,temp);
                    if (lzq > max_sorce)
                    {
                        max_sorce = lzq;
                        ans = temp;
                    }
                }
            }
            return ans;
            //return ans;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
            // 白棋
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for(int i=1;i<=15;i++)
                for (int j = 1; j <= 15; j++)
                {
                    if (chess[i, j] != -1)
                    {
                        Point pos = new Point(i * 30, j * 30);
                        drawchess(pos, chess[i, j]);
                    }
                }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        private void myinit()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++) chess[i, j] = -1;
            chesstotal = 1;
            chess[8, 8] = 1;
            Point pos = new Point(8 * 30, 8 * 30);
            drawchess(pos, 1);
        }
        private void black_chess()
        {
            Point pos = new Point();
            Point drawpos = new Point();
            pos = AIchess();
            drawpos.X = pos.X * 30;
            drawpos.Y = pos.Y * 30;
            chess[pos.X, pos.Y] = 1;
            chesstotal++;
            drawchess(drawpos, 1);
            if (judge(pos))
            {
                if (chesstotal % 2 == 1) MessageBox.Show("黑棋获胜      >  . <            ");
                else MessageBox.Show("白棋获胜      > . <                ");
                //Form1.chess_init();
                Application.Restart();
              //  myinit();
         
            }
            if (chesstotal == 15 * 15)
            {
                MessageBox.Show("  平  局       - . -                ");
                Application.Restart();
             //   myinit();
            }
        }




        private int key_sorce(int len)
        {
            if (len >= 5) return 100000000;
            if (len == 4) return 100000;
            if (len == 3) return 50000;
            if (len == 2) return 10000;
            return 0;
        }
        private int judge_sorce(int now,Point pos)
        {
            if (chess[pos.X, pos.Y] != -1) return 0;
            int res = 0,tot;
            tot = 1;
            int x, y;
            x = pos.X;
            y = pos.Y;
            for (int i = x, j = y + 1; chess[i, j] == now; j++) tot++;
            for (int i = x, j = y - 1; chess[i, j] == now; j--) tot++;
            res += key_sorce(tot);
            tot = 1;
            for (int i = x + 1, j = y; chess[i, j] == now; i++) tot++;
            for (int i = x - 1, j = y; chess[i, j] == now; i--) tot++;
            res += key_sorce(tot);
            tot = 1;
            for (int i = x + 1, j = y + 1; chess[i, j] == now; i++, j++) tot++;
            for (int i = x - 1, j = y - 1; chess[i, j] == now; i--, j--) tot++;
            res += key_sorce(tot);
            tot = 1;
            for (int i = x - 1, j = y + 1; chess[i, j] == now; i--, j++) tot++;
            for (int i = x + 1, j = y - 1; chess[i, j] == now; i++, j--) tot++;
            res += key_sorce(tot);
            return res;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click_1(object sender, EventArgs e)
        {
            if (chesstotal % 2 == 0) return;
            Point pos = new Point();
            pos = panel1.PointToClient(MousePosition);
            pos = getpos(pos);
            int x = pos.X / 30, y = pos.Y / 30;
            if (chess[x, y] == -1)
            {
                chesstotal++;
                Point mdzz = new Point();
                mdzz.X = x;
                mdzz.Y = y;
                chess[x, y] = chesstotal % 2;
                drawchess(pos, chesstotal % 2);//绘画白色棋子 
                up_step = mdzz;
                if (judge(mdzz))
                {
                    if (chesstotal % 2 == 1) MessageBox.Show("黑棋获胜      >  . <            ");
                    else MessageBox.Show("白棋获胜      > . <                ");
                    //Form1.chess_init();
                    Application.Restart();
                   // myinit();
                }
                if (chesstotal == 15 * 15)
                {
                    MessageBox.Show("  平  局       - . -                ");
                    Application.Restart();
                   // myinit();
                }
                black_chess();
            }
        }
        private void machine_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}


