using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBooseAppFramework;
using System.Diagnostics;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppUI
{
    public partial class Form1 : Form, IOutput
    {
        public Form1()
        {
            InitializeComponent();
            
            BooseContext.Instance.Output = this;

            string aboutText = BooseProgramRunner.About();
            txtOutput.AppendText("ABOUT: " + aboutText + "\r\n");

            Debug.WriteLine("ABOUT: " + aboutText);
        }

        public void WriteLine(string message)
        {
            txtOutput.AppendText(message + Environment.NewLine);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var runner = new BooseProgramRunner();
            string program = txtProgram.Text;

            txtOutput.Clear();

            Debug.WriteLine("Run button clicked");

            try
            {
                runner.Run(program);

                using (Graphics g = canvas.CreateGraphics())
                {
                    g.Clear(Color.White);

                    foreach (string cmd in runner.Commands)
                    {
                        var parts = cmd.Split(' ');

                        if (cmd.StartsWith("circle"))
                        {
                            var args = parts[1].Split(',');
                            int x = int.Parse(args[0]);
                            int y = int.Parse(args[1]);
                            int r = int.Parse(args[2]);
                            int rr = int.Parse(args[3]);
                            int gg = int.Parse(args[4]);
                            int bb = int.Parse(args[5]);

                            using (Pen p = new Pen(Color.FromArgb(rr, gg, bb)))
                            {
                                g.DrawEllipse(p, x - r, y - r, r * 2, r * 2);
                            }
                        }
                        else if (cmd.StartsWith("rect"))
                        {
                            var args = parts[1].Split(',');
                            int x = int.Parse(args[0]);
                            int y = int.Parse(args[1]);
                            int w = int.Parse(args[2]);
                            int h = int.Parse(args[3]);
                            int rr = int.Parse(args[4]);
                            int gg = int.Parse(args[5]);
                            int bb = int.Parse(args[6]);

                            using (Pen p = new Pen(Color.FromArgb(rr, gg, bb)))
                            {
                                g.DrawRectangle(p, x, y, w, h);
                            }
                        }
                        else if (cmd.StartsWith("write"))
                        {
                            var args = parts[1].Split(',');
                            int x = int.Parse(args[0]);
                            int y = int.Parse(args[1]);
                            string text = args[2].Trim('"');
                            int rr = int.Parse(args[3]);
                            int gg = int.Parse(args[4]);
                            int bb = int.Parse(args[5]);

                            using (Brush b = new SolidBrush(Color.FromArgb(rr, gg, bb)))
                            {
                                g.DrawString(text, this.Font, b, x, y);
                            }
                        }
                    }
                }

                txtOutput.AppendText($"Completed.\r\n");
                txtOutput.AppendText($"Final Pen Position: ({runner.Pen.X}, {runner.Pen.Y})\r\n");

                Debug.WriteLine($"Pen is now at ({runner.Pen.X}, {runner.Pen.Y})");
            }
            catch (BooseSyntaxException ex)
            {
                txtOutput.AppendText("SYNTAX ERROR: " + ex.Message + "\r\n");
            }
            catch (BooseRuntimeException ex)
            {
                txtOutput.AppendText("RUNTIME ERROR: " + ex.Message + "\r\n");
            }
            catch (Exception ex)
            {
                txtOutput.AppendText("UNEXPECTED ERROR: " + ex.Message + "\r\n");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtProgram_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
