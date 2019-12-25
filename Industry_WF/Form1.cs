using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Industry_WF;

namespace Industry_WF
{
    class Form1 : Form
    {
        private Button bNextRound = new Button();
        private static Label money = new Label();
        private static List<string> headers = new List<string>
        {
            "City",
            "Income"
        };
        private int headerLabelsPos = 0;

        private static List<Label> citiesLabels= new List<Label>();
        private int citiesLabelsPos = 20;
        private static List<Label> citiesIncomeLabels = new List<Label>();


        private void OnbNextRoundClick(object sender, EventArgs ea)
        {
            Text = "Round: " + Round.RoundNumber;
            Round.Go();
        }

        public static void OnTransactionDone(Facility sender, EventArgs ea)
        {
            money.Text = "Money: " + Program.Money;
            citiesIncomeLabels[(sender as City).Id].Text = (sender as City).Income.ToString();
            if (Program.Money < 0)
            {
                MessageBox.Show("You are bankrupt", "This is the end");
                Application.Exit();
            }
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
            bNextRound.AutoSize = true;
            bNextRound.Location = new Point(ClientSize.Width - bNextRound.Width, ClientSize.Height - bNextRound.Height);
            bNextRound.Click += new EventHandler(OnbNextRoundClick);
            Controls.Add(bNextRound);

            money.Location = new Point(ClientSize.Width - bNextRound.Width, 0);
            money.Text = "Money: " + Program.Money;

            Controls.Add(money);

            //HEADER
            foreach (String header in headers)
            {
                Label headerL = new Label();
                headerL.Text = header;

                headerL.Font = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
                headerL.Location = new Point(headerLabelsPos, 0);
                headerLabelsPos += 100;
                Controls.Add(headerL);
            }

            foreach (City city in World.Cities)
            {
                Label cityLabel = new Label();
                citiesLabels.Add(cityLabel);
                cityLabel.Text = city.Name;
                cityLabel.Location = new Point(0, citiesLabelsPos);
                citiesLabelsPos += 20;
                Controls.Add(cityLabel);

                Label cityIncomeLabel = new Label();
                citiesIncomeLabels.Add(cityIncomeLabel);
                cityIncomeLabel.Text = city.Income.ToString();
                cityIncomeLabel.Location = new Point(100, citiesLabelsPos);
                citiesLabelsPos += 20;
                Controls.Add(cityIncomeLabel);
            }
        }

        //public void AddCityLabel(City city)
        //{
        //    Label label = new Label();
        //    citiesLabels.Add(label);
        //    label.Text = city.Name;
        //    label.Location = new Point(0, 0);
        //    Controls.Add(label);
        //}
    }
}
