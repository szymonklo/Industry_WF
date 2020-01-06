using System;
using System.Collections.Generic;
using System.Text;
using Industry_WF;

namespace Industry_WF
{
    class World
    {
        public static List<City> Cities = new List<City>();
        public static List<Factory> Factories = new List<Factory>();
        public World()
        {
            ProductType water = new ProductType(0, 1, "water", 1);
            ProductType bread = new ProductType(1, 1, "bread", 3, new List<ProductType> { water });
            List<ProductType> ProductsTypes = new List<ProductType>
            {
                water,
                bread
            };
            ProductsKeyed products = new ProductsKeyed();
            foreach (ProductType productType in ProductsTypes)
            {
                products.Add(new Product(productType));
            }

            Factory waterSupply = new Factory("Water supply", 100, water, 4);
            Factory bakery = new Factory("Bakery", 60, bread, 3);
            Factories.Add(waterSupply);
            Factories.Add(bakery);
            //temp
            bakery.Products[0].AmountIn = 200;
            bakery.Products[0].ProductPrice = 1.1;

            City krakow = new City("Krakow", 80);
            City warszawa = new City("Warszawa", 100);
            Cities.Add(krakow);
            Cities.Add(warszawa);
            //Create cities demand
            //Console.WriteLine("**** Cities demand ****\n");
            foreach (City city in Cities)
            {
                //form1.AddCityLabel(city);
                foreach (ProductType productType in ProductsTypes)
                {
                    city.Products.Add(new Product(productType));
                }
                city.ProductWasSold += new Write().HandleProductSold;   //event
                //city.ProductWasSold += Form1.OnTransactionDone;
                city.TransactionDone += Form1.OnTransactionDone;

            }

            foreach (Factory factory in Factories)
            {
                factory.NoComponents += Form1.OnNoComponentsMessage;
                factory.TransactionDone += Form1.OnTransactionDone;
            }
        }
    }
}