using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBooseAppFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
 
            e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
 
            using (var red = new Pen(Color.Red, 2))
                e.Graphics.DrawLine(red, 10, 10, 200, 10);

            e.Graphics.DrawString("Hello, Windows Forms!",
                new Font("Arial", 12), Brushes.Black, 10, 30);

            using (var blue = new Pen(Color.Blue, 3))
                e.Graphics.DrawRectangle(blue, 50, 60, 150, 90);

            using (var green = new Pen(Color.Green, 3))
                e.Graphics.DrawEllipse(green, 230, 60, 100, 100);
        }
    }
}
