using System;
using System.Windows.Forms;
using System.Drawing;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    //public class TestForm : Form
    //{
    //    Panel panel;
    //    Label[] labels;
    //    PictureBox picBox;
    //    Size difference;

    //    public TestForm()
    //    {
    //        this.SuspendLayout();
    //        panel = new Panel();
    //        labels = new Label[4];
    //        picBox = new PictureBox();
    //        for (int i = 0; i < labels.Length; i++)
    //        {
    //            labels[i] = new Label();
    //        }
    //        labels[1].Location = new Point(labels[0].Location.X, labels[0].Location.Y + labels[0].Height);
    //        labels[2].Location = new Point(labels[1].Location.X, labels[1].Location.Y + labels[1].Height);
    //        labels[3].Location = new Point(labels[2].Location.X, labels[2].Location.Y + labels[2].Height);

    //        panel.Location = new Point(200,0);
    //        panel.Size = new Size(50,200);
    //        panel.BorderStyle = BorderStyle.Fixed3D;
    //        panel.AutoScroll = true;
    //        difference = this.Size - panel.Size;
    //        picBox.Size = panel.Size + difference;
    //        this.Controls.Add(panel);
    //        this.Controls.Add(labels[0]);
    //        this.Controls.Add(labels[1]);
    //        this.Controls.Add(labels[2]);
    //        this.Controls.Add(labels[3]);
    //        this.ResumeLayout(false);

    //        panel.SuspendLayout();
    //        panel.Controls.Add(picBox);
    //        panel.ResumeLayout(false);

    //        picBox.Paint += new PaintEventHandler(panel_Paint);
    //        this.Resize += new EventHandler(TestForm_Resize);
    //    }

    //    void TestForm_Resize(object sender, EventArgs e)
    //    {
    //        panel.Size = this.Size - difference;
    //        picBox.Size = panel.Size + difference;
    //    }

    //    void panel_Paint(object sender, PaintEventArgs e)
    //    {
    //        int[] values = new int[4];
    //        values[0] = e.ClipRectangle.X;
    //        values[1] = e.ClipRectangle.Y;
    //        values[2] = e.ClipRectangle.Width;
    //        values[3] = e.ClipRectangle.Height;
    //        UpdateLabels(values);
    //    }

    //    void UpdateLabels(int[] values)
    //    {
    //        labels[0].Text = "X: " + values[0];
    //        labels[1].Text = "Y: " + values[1];
    //        labels[2].Text = "Width: " + values[2];
    //        labels[3].Text = "Heigth: " + values[3];
    //    }
    //}

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
