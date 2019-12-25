﻿using System;
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
            "Product",
            "Income"
        };
        private static List<string> headersF = new List<string>
        {
            "Factory",
            "Product",
            "Cost"
        };
        private int headerLabelsPos = 0;


        private static List<Label> citiesLabels= new List<Label>();
        private int citiesLabelsPos = 20;
        private static List<Label> factoriesLabels = new List<Label>();
        private int factoriesLabelsPos = 320;
        //private int citiesProductLabelsPos = 0;
        private static List<Label> citiesProductsLabels = new List<Label>();
        private static List<Label> citiesIncomeLabels = new List<Label>();
        private static List<Label> factoriesProductsLabels = new List<Label>();
        private static List<Label> factoriesCostLabels = new List<Label>();


        private void OnbNextRoundClick(object sender, EventArgs ea)
        {
            Text = "Round: " + Round.RoundNumber;
            Round.Go();
        }

        public static void OnTransactionDone(object sender, ProductEventArgs a)
        {
            money.Text = "Money: " + Program.Money;
            //int idd = (sender as City).Id;
            //citiesIncomeLabels[(sender as City).Id].Text = (sender as City).Income.ToString();
            //int b = (sender as City).Id;
            //int c = (sender as City).Products.Count();
            //int d = a.Product.Id;
            if (sender is City)
                citiesIncomeLabels[((sender as City).Id*(sender as City).Products.Count()+a.Product.Id)].Text = (a.Product.AmountDone * a.Product.ProductPrice).ToString();
            else if (sender is Factory)
                //factoriesCostLabels[((sender as Factory).Id * (sender as Factory).Products.Count() + a.Product.Id)].Text = (a.Product.AmountDone * a.Product.ProductCost).ToString();

            if (Program.Money < 0)
            {
                MessageBox.Show("You are bankrupt", "This is the end");
                Application.Exit();
            }
        }
        public static void OnFewProductsToSendMessage(Facility sender, ProductEventArgs ea)
        {
            MessageBox.Show($"Factory {sender.Name} has only {ea.Product.AmountOut}  {ea.Product.Name} to send", "Few products to send warning");
        }

        public static void OnNoComponentsMessage(Facility sender, ProductEventArgs ea)
        {
                MessageBox.Show($"Factory {sender.Name} has no {ea.Product.Name}", "No components warning");
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
            headerLabelsPos = 0;
            foreach (String header in headersF)
            {
                Label headerL = new Label();
                headerL.Text = header;

                headerL.Font = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
                headerL.Location = new Point(headerLabelsPos, 300);
                headerLabelsPos += 100;
                Controls.Add(headerL);
            }

            foreach (City city in World.Cities)
            {
                Label cityLabel = new Label();
                citiesLabels.Add(cityLabel);
                cityLabel.Text = city.Name;
                cityLabel.Location = new Point(0, citiesLabelsPos);
                Controls.Add(cityLabel);

                foreach (Product product in city.Products)
                {

                    Label cityProductLabel = new Label();
                    citiesProductsLabels.Add(cityProductLabel);
                    cityProductLabel.Text = product.Name.ToString();
                    cityProductLabel.Location = new Point(100, citiesLabelsPos);
                    Controls.Add(cityProductLabel);

                    Label cityIncomeLabel = new Label();
                    citiesIncomeLabels.Add(cityIncomeLabel);
                    cityIncomeLabel.Text = (city.Products[product.Id].AmountDone* city.Products[product.Id].ProductPrice).ToString();
                    cityIncomeLabel.Location = new Point(200, citiesLabelsPos);
                    citiesLabelsPos += 20;
                    Controls.Add(cityIncomeLabel);
                }
                //citiesLabelsPos += 20;

            }
            foreach (Factory factory in World.Factories)
            {
                Label factoryLabel = new Label();
                factoriesLabels.Add(factoryLabel);
                factoryLabel.Text = factory.Name;
                factoryLabel.Location = new Point(0, factoriesLabelsPos);
                Controls.Add(factoryLabel);

                foreach (Product product in factory.Products)
                {

                    Label factoryProductLabel = new Label();
                    factoriesProductsLabels.Add(factoryProductLabel);
                    factoryProductLabel.Text = product.Name.ToString();
                    factoryProductLabel.Location = new Point(100, factoriesLabelsPos);
                    Controls.Add(factoryProductLabel);

                    Label factoryIncomeLabel = new Label();
                    factoriesCostLabels.Add(factoryIncomeLabel);
                    factoryIncomeLabel.Text = (factory.Products[product.Id].AmountDone * factory.Products[product.Id].ProductCost).ToString();
                    factoryIncomeLabel.Location = new Point(200, factoriesLabelsPos);
                    factoriesLabelsPos += 20;
                    Controls.Add(factoryIncomeLabel);
                }
                //citiesLabelsPos += 20;

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
