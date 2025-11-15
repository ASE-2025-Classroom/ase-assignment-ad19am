namespace MyBooseAppUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProgram
            // 
            this.txtProgram.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgram.Location = new System.Drawing.Point(27, 12);
            this.txtProgram.Multiline = true;
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProgram.Size = new System.Drawing.Size(400, 180);
            this.txtProgram.TabIndex = 0;
            this.txtProgram.TextChanged += new System.EventHandler(this.txtProgram_TextChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(27, 198);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(400, 120);
            this.txtOutput.TabIndex = 1;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(27, 370);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(400, 400);
            this.canvas.TabIndex = 2;
            this.canvas.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(27, 324);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 40);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 782);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtProgram);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button btnRun;
    }
}