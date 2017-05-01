using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace 五子棋
{
    public partial class pvp : Form
    {
        TcpClient tcp = new TcpClient();
        IPAddress ip = IPAddress.Parse("123.207.151.145");
        int[,] chess = new int[20, 20];
        int chesstot = 0;


        public pvp()
        {
            InitializeComponent();
        }

        private void pvp_Load(object sender, EventArgs e)
        {
            try
            {
                tcp.Connect(ip, 2030);
            }
            catch (SocketException)
            {
                MessageBox.Show("连接服务器失败");
                tcp.Close();
                Application.Exit();
            }
            
        }

        private void pvp_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcp.Close();
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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

        private bool judge(Point pos)
        {
            int x, y, now, tot;
            x = pos.X;
            y = pos.Y;
            now = chess[x, y];
            tot = 1;
            for (int i = x, j = y + 1; chess[i, j] == now; j++) tot++;
            for (int i = x, j = y - 1; chess[i, j] == now; j--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x + 1, j = y; chess[i, j] == now; i++) tot++;
            for (int i = x - 1, j = y; chess[i, j] == now; i--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x + 1, j = y + 1; chess[i, j] == now; i++, j++) tot++;
            for (int i = x - 1, j = y - 1; chess[i, j] == now; i--, j--) tot++;
            if (tot >= 5) return true;
            tot = 1;
            for (int i = x - 1, j = y + 1; chess[i, j] == now; i--, j++) tot++;
            for (int i = x + 1, j = y - 1; chess[i, j] == now; i++, j--) tot++;
            if (tot >= 5) return true;
            return false;
        }
        private void panel1_Click(object sender, EventArgs e)
        {

        }
    }
}
