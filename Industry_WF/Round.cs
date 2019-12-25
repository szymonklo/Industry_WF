using System;
using System.Collections.Generic;
using System.Text;
using Industry_WF;
using System.Collections.ObjectModel;
using System.Linq;

namespace Industry_WF
{
    class Round
    {
        public static int RoundNumber { get; set; }
        public static void Go()
        {
            Console.WriteLine("Round: " + RoundNumber);
            //Cities demand
            Console.WriteLine("**** Cities demand ****\n");
            foreach (City city in World.Cities)
            {
                city.Demand();
            }
            Console.WriteLine("\n");

            //Factories produce
            Console.WriteLine("**** Factories produce ****\n");
            foreach (Factory factory in World.Factories)
            {
                factory.Produce(factory.Product);
            }
            Console.WriteLine("\n");

            // Products are transported from "tier n" to "tier n-1" factories
            Console.WriteLine("**** Products are transported from tier n to tier n - 1 factories ****\n");
            for (int t = 4; t > 0; t--)
            {
                foreach (Factory factoryS in World.Factories.Where(factory => factory.Tier == t))
                {
                    foreach (Factory factoryR in World.Factories.Where(factory => factory.Tier == t-1))
                    {
                        if (factoryR.ProductType.Components.Contains(factoryS.ProductType))
                        {
                            TransportOrder transportOrder = new TransportOrder(factoryS, factoryR, factoryS.ProductType, 40);
                        }
                    }
                }
            }
            Console.WriteLine("\n");

            //Products are transported from factories to cities
            Console.WriteLine("**** Products are transported from factories to cities ****\n");
            foreach (Factory factory in World.Factories)
            {
                foreach (City city in World.Cities)
                {
                    TransportOrder transportOrder = new TransportOrder(factory, city, factory.ProductType, 40);
                }
            }
            Console.WriteLine("\n");

            //Cities consume
            Console.WriteLine("**** Cities consume ****\n");
            foreach (City city in World.Cities)
            {
                city.Consume();
            }
            Console.WriteLine("\n");

            RoundNumber++;

        }
    }
}
