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
    public partial class choose : Form
    {

        public choose()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            machine renji = new machine();
            renji.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pvp pvpchess = new pvp();
            pvpchess.Show();
            this.Hide();
        }
    }
}
