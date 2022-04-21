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
        private ControlSystem system;
       
        
        private double dt = 0.1;

        public MainForm() {
            InitializeComponent();
            system = new ControlSystem(dt);
        }


        private void tmModel_Tick(object sender, EventArgs e) {
            system.Calc();
          
            lbY.Text = system.Output.ToString("F2");
            chMainPlot.Series[0].Points.AddXY(system.Time, system.Output);
            chMainPlot.Series[1].Points.AddXY(system.Time, system.Input1);
            chMainPlot.Series[2].Points.AddXY(system.Time, system.Input2);
        }

        private void btnStart_Click(object sender, EventArgs e) {
            tmModel.Start();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            tmModel.Stop();
        }

        private void btnUp_Click(object sender, EventArgs e) {
            system.Input1 += 1;
            tbX.Text = system.Input1.ToString("F2");
        }

        private void btnDn_Click(object sender, EventArgs e) {
            system.Input1 -= 1;
            tbX.Text = system.Input1.ToString("F2");
        }

        private void btnX1_Click(object sender, EventArgs e) {
            tmModel.Interval = 1000;
        }

        private void btnX10_Click(object sender, EventArgs e) {
            tmModel.Interval = 100;
        }

        private void lbYCaption_Click(object sender, EventArgs e) {

        }

        private void btnDn2_Click(object sender, EventArgs e) {
            system.Input2 -= 1;
            tbX2.Text = system.Input2.ToString("F2");
        }

        private void btnUp2_Click(object sender, EventArgs e) {
            system.Input2 += 1;
            tbX2.Text = system.Input2.ToString("F2");
        }

        private void btnSPDn_Click(object sender, EventArgs e) {
            system.SetPoint -= 1;
            tbSetpoint.Text = system.SetPoint.ToString("F2");
        }

        private void btnSPUp_Click(object sender, EventArgs e) {
            system.SetPoint += 1;
            tbSetpoint.Text = system.SetPoint.ToString("F2");
        }

        private void btnKDn_Click(object sender, EventArgs e) {
            system.K -= 1;
            tbK.Text = system.K.ToString("F2");
        }

        private void btnKUp_Click(object sender, EventArgs e) {
            system.K += 1;
            tbK.Text = system.K.ToString("F2");
        }

        private void btnTiDn_Click(object sender, EventArgs e) {
            system.Ti -= 1;
            tbTi.Text = system.Ti.ToString("F2");
        }

        private void btnTiUp_Click(object sender, EventArgs e) {
            system.Ti += 1;
            tbTi.Text = system.Ti.ToString("F2");
        }

        private void btnTdDn_Click(object sender, EventArgs e) {
            system.Td -= 1;
            tbTd.Text = system.Td.ToString("F2");
        }

        private void btnTdUp_Click(object sender, EventArgs e) {
            system.Td += 1;
            tbTd.Text = system.Td.ToString("F2");
        }

        private void tbTi_TextChanged(object sender, EventArgs e) {
            double val = 0;
            if(Double.TryParse(tbTi.Text, out val)) {
                system.Ti = val;
            }
        }
    }
}
