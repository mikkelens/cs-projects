using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Forms1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int tickRate = 50;
        private Timer updater;

        private Label cashLabel;
        private Label tickLabel;
        private Button manualClickButton;

        private Button buyUpgradeButton;
        private Button buyClickerButton;
        private void Form1_Load(object sender, EventArgs e)
        {
            cashLabel = label1;
            tickLabel = label2;
            manualClickButton = button1;

            buyUpgradeButton = button2;
            buyClickerButton = button3;

            updater = new Timer(1000 / tickRate);
            updater.Elapsed += FixedUpdate;
            updater.AutoReset = true;
            updater.Enabled = true;
        }

        private int clickWorth = 1;
        private int cash = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            cash += clickWorth;
            UpdateCash();
        }

        void UpdateCash()
        {
            label1.Text = $"Pressed button {cash} times.";
        }

        Color darkColor = Color.FromArgb(255, 16, 16, 16);

        private void button2_Click(object sender, EventArgs e)
        {
            if (cash >= 3)
            {
                clickWorth++;
                cash -= 3;
            }
        }

        private int ticks = 0;
        private int clickers = 0;
        private float clickTime = 0;
        private void FixedUpdate(Object source, ElapsedEventArgs e)
        {
            ticks++;
            clickTime += clickers / tickRate;
            if (clickTime >= 1)
            {
                cash++;
                clickTime--;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cash >= 5)
            {
                clickers++;
                cash -= 5;
            }
        }

        private void fixedDeltaTime_Tick(object sender, EventArgs e)
        {
            UpdateCash();
            tickLabel.Text = $"TICKS: {ticks}, Pseudoseconds: {ticks / tickRate}";

            if (cash < 3) buyUpgradeButton.Enabled = false;
            else buyUpgradeButton.Enabled = true;

            if (cash < 5) buyClickerButton.Enabled = false;
            else buyClickerButton.Enabled = true;
        }
    }
}
