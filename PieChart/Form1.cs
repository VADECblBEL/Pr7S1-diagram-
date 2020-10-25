using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

namespace PieChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            ArrayList data = new ArrayList();
            data.Add(new PieChartElement("East", (float)50.75));
            data.Add(new PieChartElement("West", (float)22));
            data.Add(new PieChartElement("North", (float)72.32));
            data.Add(new PieChartElement("South", (float)12));
            data.Add(new PieChartElement("Central", (float)44));

            chart.Image = drawPieChart(data, new Size(chart.Width, chart.Height));
        }

        private Image drawPieChart(ArrayList elements, Size s)
        {
            Color[] colors = { Color.Red, Color.Violet, Color.Aqua, Color.FromArgb(0xAA00FF),
                Color.Lavender, Color.Indigo, Color.DarkSalmon, Color.DarkRed,
                Color.DarkOrange, Color.DarkSalmon, Color.DarkGreen,
                Color.DarkBlue, Color.Lavender, Color.LightBlue, Color.Coral };

            if (elements.Count > colors.Length)
            {
                throw new ArgumentException("Pie chart must have " + colors.Length + " or fewer elements");
            }

            Bitmap bm = new Bitmap(s.Width, s.Height);
            Graphics g = Graphics.FromImage(bm);
            g.SmoothingMode = SmoothingMode.HighQuality;

            
            float total = 0;

            foreach (PieChartElement e in elements)
            {
                if (e.value < 0)
                {
                    throw new ArgumentException("All elements must have positive values");
                }
                total += e.value;
            }

            if (!(total > 0))
            {
                throw new ArgumentException("Must provide at least one PieChartElement with a positive value");
            }

            
            Rectangle rect = new Rectangle(1, 1, s.Width - 2, s.Height - 2);

            Pen p = new Pen(Color.Black, 5);

            
            float startAngle = 0;
            int colorNum = 0;

            
            foreach (PieChartElement e in elements)
            {
                
                Brush b = new LinearGradientBrush(rect, colors[colorNum++], Color.White, (float)45);
                float sweepAngle = (e.value / total) * 360;
                g.FillPie(b, rect, startAngle, sweepAngle);
                g.DrawPie(p, rect, startAngle, sweepAngle);
                startAngle += sweepAngle;
            }
            return bm;
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }
    }
}