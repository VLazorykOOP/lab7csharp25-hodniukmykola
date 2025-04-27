using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficLightSimulation
{
    public partial class Form1 : Form
    {
        private int state = 0; // 0 - Red, 1 - Yellow, 2 - Green

        public Form1()
        {
            InitializeComponent();
            InitializeTrafficLight();
        }

        private void InitializeTrafficLight()
        {
            // Початкові кольори
            panelRed.BackColor = Color.Gray;
            panelYellow.BackColor = Color.Gray;
            panelGreen.BackColor = Color.Gray;

            timer1.Tick += Timer1_Tick;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown.Value * 1000; // Перетворити секунди в мілісекунди
            timer1.Start();
            state = 0;
            UpdateTrafficLight();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            state = (state + 1) % 3; // 0 -> 1 -> 2 -> 0
            UpdateTrafficLight();
        }

        private void UpdateTrafficLight()
        {
            panelRed.BackColor = state == 0 ? Color.Red : Color.Gray;
            panelYellow.BackColor = state == 1 ? Color.Yellow : Color.Gray;
            panelGreen.BackColor = state == 2 ? Color.Lime : Color.Gray;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDownInterval_Load(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click_1(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
