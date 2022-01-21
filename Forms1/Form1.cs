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

        private Button buyClickerButton;
        private Button buyRecursiveClickerButton;
        private void Form1_Load(object sender, EventArgs e)
        {
            cashLabel = label1;
            tickLabel = label2;

            buyClickerButton = button3;
            buyRecursiveClickerButton = button4;

            updater = new Timer(1000 / tickRate);
            updater.Elapsed += FixedUpdate;
            updater.AutoReset = true;
            updater.Enabled = true;
        }

        private int clickWorth = 1;
        private int cash = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClick(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuyClicker(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            recursiveClicking = true;
            cash -= 50;
            buyRecursiveClickerButton.Enabled = false;
        }

        void ButtonClick(int clickTimes)
        {
            cash += 1 + clickWorth * clickTimes;
            UpdateCash();
        }

        private void BuyClicker(int amount)
        {
            clickers += amount;
            cash -= 1 * amount;
        }

        void UpdateCash()
        {
            cashLabel.Text = $"Pressed button {cash} times.";
        }

        private int ticks = 0;
        private int clickers = 0;
        private float clickTime = 0;
        private bool recursiveClicking;

        private void FixedUpdate(Object source, ElapsedEventArgs e)
        {
            ticks++;
            clickTime += (float)clickers / tickRate;
        }

        private void fixedDeltaTime_Tick(object sender, EventArgs e)
        {
            if (clickTime >= 1)
            {
                int clickAmount = (int)clickTime;
                ButtonClick(clickAmount);
                if (recursiveClicking) BuyClicker(clickAmount);
                clickTime -= clickAmount;
            }
            UpdateCash();
            tickLabel.Text = $"TICKS: {ticks}, Pseudoseconds: {ticks / tickRate}";

            buyClickerButton.Enabled = cash >= 1;
            if (cash >= 50) buyRecursiveClickerButton.Visible = true;
        }
    }
}
