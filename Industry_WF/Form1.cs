using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Industry_WF
{
    public partial class Form1 : Form
    {
        private Button bNextRound = new Button();

        private void OnbNextRoundClick(object sender, EventArgs ea)
        {
            Round.Go();
        }

        public Form1()
        {
            StartPosition = FormStartPosition.Manual;

            int screenWidth = 1920;
            int screenHeight = 1080;
            
            Location = new Point(screenWidth / 2, 0);
            Width = screenWidth / 2;
            Height = screenHeight - 35;
            //InitializeComponent();
            bNextRound.Text = "Next Round";
            bNextRound.Location = new Point(ClientSize.Width - bNextRound.Width, ClientSize.Height - bNextRound.Height);
            bNextRound.Click += new EventHandler(OnbNextRoundClick);

            Controls.Add(bNextRound);
        }
    }
}
