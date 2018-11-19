using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListCarExample
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            #region Creating List And Populating List
            var cars = new List<Car>();
            for (int i = 0; i < 8; i++)
            {
                var car = new Car()
                {
                    ID = i + 1,
                    Make = $"Mercedes C{200 + i}".ToUpper(),
                    Model = 2010 + i,
                    Price = (i + 1) * 60000
                };

                cars.Add(car);
            }
            #endregion

            string Continue = "Y";

            #region While Loop
            while (Continue == "Y")
            {
                try
                {
                    #region Option Select
                    int option = 0;
                    option = Convert.ToInt16(WriteThenRead("Options\n1. View\n2. Add\n3. Search\n4. Remove", ""));
                    #endregion

                    log.Info($"Menu Option selected {option}");

                    #region View All Function
                    //Viewing
                    if (option == 1)
                    {
                        WriteThenRead($"You have selected option {option} to View all cars\n****************************************************", cars);
                        log.Info("User Selected to view all cars");
                    }
                    #endregion

                    #region Add Function
                    //Adding
                    if (option == 2)
                    {
                        var newCar = new Car();

                        newCar.ID = cars.Count + 1;

                        newCar.Make = WriteThenRead($"You have selected option {option} to Add a car\n****************************************************", "Car Make");

                        newCar.Model = Convert.ToInt16(WriteThenRead($"You have selected option {option} to Add a car\n****************************************************", "Car Model"));

                        newCar.Price = Convert.ToDouble(WriteThenRead($"You have selected option {option} to Add a car\n****************************************************", "Price"));

                        cars.Add(newCar);

                        WriteThenRead($"Added a new Car\nID: {newCar.ID} | Make: {newCar.Make} | Model: {newCar.Model} | Price: R{newCar.Price}");

                        log.Info($"New car Added\nID: {newCar.ID} | Make: {newCar.Make} | Model: {newCar.Model} | Price: R{newCar.Price}");
                    }
                    #endregion

                    #region Searching Function
                    //Searching
                    if (option == 3)
                    {
                        var newCar = new Car();

                        string searchOptions = "Y";

                        while (searchOptions == "Y")
                        {
                            int search = Convert.ToInt16(WriteThenRead($"You have selected option {option} to Search for a car\n****************************************************", "Options\n1. Search by Make\n2. Search by Price\n3. Search by Model"));

                            //Searching by Make
                            if (search == 1)
                            {
                                string make = WriteThenRead($"You have selected to Search by make\n****************************************************", "Search by Car Make");

                                var searchMake = cars.Find(x => x.Make.Equals(make));
                                WriteThenRead($"ID: {searchMake.ID} | Make: {searchMake.Make} | Model: {searchMake.Model} | Price: R{searchMake.Price}");

                                log.Info($"Search Function by Make. ID: {searchMake.ID} | Make: {searchMake.Make} | Model: {searchMake.Model} | Price: R{searchMake.Price}");
                            }

                            //Searching by Price
                            if (search == 2)
                            {
                                double lowest = Convert.ToDouble(WriteThenRead($"You have selected to Search by Price", "\nSearch by Price, Enter Lowest Price"));
                                double highest = Convert.ToDouble(WriteThenRead($"You have selected to Search by Price", "\nSearch by Price, Enter Highest Price"));

                                log.Info($"Search Function by price. Lowest price: R{lowest} | Highest Price: R{highest}");

                                List<Car> searchPrice = cars.FindAll(x => x.Price >= lowest && x.Price <= highest);
                                if (searchPrice.Count != 0)
                                {
                                    WriteThenRead($"Searching for Cars between R{lowest} and R{highest}\n****************************************************", searchPrice);
                                    log.Info($"Cars Found!");
                                    searchPrice.ForEach(x => log.Info($"ID: {x.ID} | Make: {x.Make} | Model: {x.Model} | Price: R{x.Price}"));
                            }
                                else
                                {
                                    WriteThenRead("No record found");
                                    log.Info($"No Record Found!");
                                }
                            }

                            //Searching by Model
                            if (search == 3)
                            {

                                int Beginning = Convert.ToInt16(WriteThenRead($"You have selected to Search by Model\n****************************************************", "Search by Model, Enter From Year"));
                                int End = Convert.ToInt16(WriteThenRead($"You have selected to Search by Model\n****************************************************", "Search by Model, Enter To Year"));

                                log.Info($"Search Function by Year. Finding Car between {Beginning}-{End}");

                                Writing($"Searching for Cars between {Beginning} and {End}\n****************************************************");
                                List<Car> searchModel = cars.FindAll(x => x.Model >= Beginning && x.Model <= End);

                                if (searchModel.Count != 0)
                                {
                                    WriteThenRead("", searchModel);
                                    log.Info("Cars Found!");
                                    searchModel.ForEach(x => log.Info($"ID: {x.ID} | Make: {x.Make} | Model: {x.Model} | Price: R{x.Price}"));
                                }
                                else
                                {
                                    WriteThenRead("No record found");
                                    log.Info($"No Record Found!");
                                }
                            }
                            Console.Clear();
                            searchOptions = WriteThenRead("Return to Search \nY. For Yes\nN. For No", "");
                            if (searchOptions == "Y")
                            {
                                WriteThenRead("Returning to Search menu...");
                            }
                        }
                    }
                    #endregion

                    #region Remove Function
                    //Remove
                    if (option == 4)
                    {

                        WriteThenRead($"You have selected option {option} to Remove a car\n", cars);
                        int ID = Convert.ToInt16(WriteThenRead($"\nEnter ID of the car you wish to remove", ""));
                        Car findID = cars.Find(x => x.ID.Equals(ID));

                        bool remove = cars.Remove(findID);
                        if (remove == true)
                        {
                            WriteThenRead("Car was removed\n\nDisplaying new list\n", cars);
                            log.Info($"Removing car where ID: {ID}");
                        }
                        else
                        {
                            WriteThenRead("Car was not removed");
                            log.Info("Car was not removed");
                        }
                    }

                    #endregion

                    #region Continue Function For While Loop
                    Console.Clear();
                    Continue = WriteThenRead("Return to main menu \nY. For Yes\nN. For No", "");
                    if (Continue == "Y")
                    {
                        WriteThenRead("Returning to main menu...");
                    }
                    else
                    {
                        WriteThenRead("Exitting...");
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    WriteThenRead($"Error: {e.Message}");
                    log.Error($"Error! {e.Message}");
                }
            }
            #endregion
        }

        #region Write Methods
        //Generic WriteLine
        static void Writing(string value)
        {
            Console.WriteLine(value);
        }

        //WriteLine, ReadLine then Clear
        static void WriteThenRead(string value)
        {
            Console.WriteLine(value);
            Console.ReadLine();
            Console.Clear();
        }

        //WriteLine, Writes List of values out then Reads and Clears
        static void WriteThenRead(string value, List<Car> cars)
        {
            Console.WriteLine(value);
            cars.ForEach(x => Writing($"ID: {x.ID} | Make: {x.Make} | Model: {x.Model} | Price: R{x.Price}"));
            Console.ReadLine();
        }

        //ending while loop
        static string WriteThenRead(string value, string valueTwo)
        {
            Console.WriteLine(value + $"\n{valueTwo}");
            string Continue = Console.ReadLine().ToUpper();
            Console.Clear();

            return Continue;
        }
        #endregion
    }
}
