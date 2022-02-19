using SampleModel.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleModel
{
    public partial class MainForm : Form
    {
        private ComplexBlock Block;
        private LimitBlock xLimit = new LimitBlock(0,100);
        private double y;
        private double x;
        private double time = 0;
        private double dt = 0.1;

        public MainForm() {
            InitializeComponent();
            Block = new ComplexBlock();
            Block.Add(new NoiseBlock(2));
            Block.Add(new DelayBlock(dt,5));
            Block.Add(new GainBlock(5));
            Block.Add(new APBlock(dt, 5));            
            Block.Add(new APBlock(dt, 10));
            Block.Add(new NoiseBlock(2));
        }


        private void tmModel_Tick(object sender, EventArgs e) {
            y = Block.Calc(x);
            time += dt; 
            lbY.Text = y.ToString("F2");
            chMainPlot.Series[0].Points.AddXY(time,y);
            chMainPlot.Series[1].Points.AddXY(time,x);
        }

        private void btnStart_Click(object sender, EventArgs e) {
            tmModel.Start();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            tmModel.Stop();
        }

        private void btnUp_Click(object sender, EventArgs e) {
            x += 1;
            x = xLimit.Calc(x);
            tbX.Text = x.ToString("F2");
        }

        private void btnDn_Click(object sender, EventArgs e) {
            x -= 1;
            x = xLimit.Calc(x); 
            tbX.Text = x.ToString("F2");
        }

        private void btnX1_Click(object sender, EventArgs e) {
            tmModel.Interval = 1000;
        }

        private void btnX10_Click(object sender, EventArgs e) {
            tmModel.Interval = 100;
        }
    }
}
