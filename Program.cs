using System.Collections.Generic;

public class Program
{
    //main function
    public static void Main(String[] args)
    { 
        //list menu items
        List<Item> menuItems = new List<Item>
        {
            new Item("Fruit Cup", 0, 5.00m),
            new Item("Coffee", 0, 3.00m),
            new Item("Muffin", 0, 2.00m)
        };
        //create user cart
        Cart UserCart = new Cart();

        //load saved cart data if it exists
        FileManager.LoadCart(Cart.cartItems, Cart.cartQuantities, menuItems);

        //call main menu method
        Main_Menu(UserCart, menuItems, "");
       

    }

    //Cafe Banner + Tax Rate
    public static void ShowBanner()
    {
        Console.WriteLine("____________________________________");
        Console.WriteLine("|                                  |");
        Console.WriteLine("|  Café Du Merde - Tax Rate: 0.095 |");
        Console.WriteLine("|__________________________________|");
    }

    // main menu method
    public static void Main_Menu(Cart userCart, List<Item> menuItems, string howMany)
    {

        while (true)
        {         
            //define cart item and quantity lists
            List<Item> cartItems = new List<Item>();
            List<int> cartQuantities = new List<int>();

            //display banner
            ShowBanner();

            //display options
            Console.WriteLine("[1] Add Item");
            Console.WriteLine("[2] View Cart");
            Console.WriteLine("[3] Remove Item by Number");
            Console.WriteLine("[4] Checkout");
            Console.WriteLine("[5] QUIT");


            //ask for input and store the input as a string
            Console.Write("Please Choose an Option: ");
            string choice = (Console.ReadLine());


            //run different methods depending on user input
            try
            {
                int choiceNum = int.Parse(choice);

                if (choiceNum >= 1 && choiceNum <= 6)
                {

                    if (choice == "1") userCart.AddItems(menuItems, howMany);
                    else if (choice == "2") userCart.ViewCart(menuItems);
                    else if (choice == "3") userCart.RemoveItem(menuItems);
                    else if (choice == "4") userCart.Checkout(menuItems);
                    else if (choice == "5")
                    {
                        Console.WriteLine("Have a great day! Press any key...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                else
                    {

                    }
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!!!");
            }
        }
            
    } 
}
