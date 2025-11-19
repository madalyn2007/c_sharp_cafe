using System;
using System.Collections.Generic;



//cart class
public class Cart
{

    //list for the cafe items in the cart
    public static List<Item> cartItems = new List<Item>();

    //int list for how many of each item is in the cart
    public static List<int> cartQuantities = new List<int>();

    //save cart object to access save method
    FileManager saveCart = new FileManager();
 

    //discount code
    public string Discount { get; set; } = "STUDENT10";

    //add items method
    public void AddItems(List<Item> menuItems, string howMany)
    {
        while (true)
        {
            Console.WriteLine("---------------------------");

            //loop through the item list and display item list w prices
            for (int x = 0; x < menuItems.Count; x++)
            {
                Console.WriteLine($"{x + 1}. {menuItems[x].Name} ${menuItems[x].Price}");
            }

            //ask for user input and store it in a variable. return to main menu if nothing is entered.
            Console.Write("What would you like to add to your cart? Or press ENTER to exit: ");
            string input = Console.ReadLine();


            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            try
            {
                //convert input to an int and check if it's valid
                int choiceNum = int.Parse(input);
                if (choiceNum >= 1 && choiceNum <= menuItems.Count)
                {
                    //valid input, continue
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!!!");
                return;
            }

            //convert input to an int and subtract from one bc C# index starts at 1. stored in variable 'index'
            int index = int.Parse(input) - 1;

            Console.WriteLine("---------------------------");

            //ask for quantity of item chosen
            Console.Write("How many?: ");
            string howManyInput = Console.ReadLine();

            if (string.IsNullOrEmpty(howManyInput))
            {
                return;
            }

            //convert input to an int
            int howManyNum = int.Parse(howManyInput);

            try
            {
                howManyNum = int.Parse(howManyInput);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!!!");
                return;
            }
            Console.WriteLine("---------------------------");



            //append chosen items + quantities to cartItems and cartQuantities
            cartItems.Add(menuItems[index]);
            cartQuantities.Add(howManyNum);

            //display what the user added to their cart
            Console.WriteLine($"You added {howManyInput} {menuItems[index].Name} to your cart.");

            //save cart to file
            saveCart.SaveFile(cartItems, cartQuantities);
        }

    }
    
    //view cart method
    public void ViewCart(List<Item> menuItems)
        {
        Console.WriteLine("---------------------------");
        Console.WriteLine("Your Cart:");

        decimal subtotal = 0;

     //loop through cartItems and cartQuantities to display items in cart with their quantities and total price
         for (int x = 0; x < cartItems.Count; x++)
          {
               decimal itemTotal = cartItems[x].Price * cartQuantities[x];
               subtotal += itemTotal;
               Console.WriteLine($"{x + 1}. {cartItems[x].Name} x{cartQuantities[x]} - ${itemTotal:F2}");
          }


        if (cartItems.Count == 0)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Your cart is empty. Add some items!");
            return;
        }

       Console.WriteLine("---------------------------");
       Console.WriteLine($"Total: ${subtotal:F2}");
       Console.WriteLine("Press ENTER to return to the main menu: ");
       string input = Console.ReadLine();
       if (string.IsNullOrEmpty(input))
        {
          return;
        }
       Console.WriteLine("---------------------------");

        

    }



    //remove item method
    public void RemoveItem(List<Item>menuItems)
    {
        while (true)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Your Cart:");


            //loop through cartItems and cartQuantities to display items in cart with their quantities + total price
            for (int x = 0; x < cartItems.Count; x++)
            {
                decimal subtotal = cartItems[x].Price * cartQuantities[x];
                Console.WriteLine($"{x + 1}. {cartItems[x].Name} x {cartQuantities[x]} = {subtotal:F2}");
            }
            //ask for user input on which item to remove
            Console.WriteLine("Enter the number of the item you want to remove. Or press ENTER to exit: ");
            string num = Console.ReadLine();

            int indexToRemove;

            if (string.IsNullOrEmpty(num))
            {
                return;
            }

            indexToRemove = int.Parse(num) - 1;

            if (indexToRemove >= 0 && indexToRemove < cartItems.Count)
            {
                //continue;
            }

            //remove item and quantity at the specified index
            cartItems.RemoveAt(indexToRemove);
            cartQuantities.RemoveAt(indexToRemove);

            saveCart.SaveFile(cartItems, cartQuantities);

            Console.WriteLine("---------------------------");
            Console.WriteLine("Item removed from cart.");
            Console.WriteLine("Press ENTER to return to the Main Menu.");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
        }
    }

    public void Checkout(List<Item> menuItems)
    {

        while (true)
        {
            Console.WriteLine("---------------------------");

            //set everything to 0, discountApplied to false, and declare tax rate
            decimal subtotal = 0;
            decimal discount = 0;
            bool discountApplied = false;
            decimal taxRate = 0.095m;


            //loops through items in the cart and displays them w/ their quantities and total price
            for (int x = 0; x < cartItems.Count; x++)
            {
                decimal itemTotal = cartItems[x].Price * cartQuantities[x];
                subtotal += itemTotal;
                Console.WriteLine($"{x + 1}. {cartItems[x].Name} x{cartQuantities[x]} - ${itemTotal:F2}");
            }

            //FileManager.LoadCart(cartItems, cartQuantities, menuItems);
            decimal tax = subtotal * taxRate;

            Console.WriteLine("Enter discount code or press ENTER to skip: ");
            string discountCodeInput = Console.ReadLine();

            //try/catch blocks for invalid input
            try
            {
                //if no discount code is entered, discount stays 0
                if (string.IsNullOrEmpty(discountCodeInput))
                {
                    discount = 0;
                }

                //if the correct discount code is entered, apply 10% discount
                else if (discountCodeInput == Discount)
                {
                    discount = subtotal * 0.10m;
                    Console.WriteLine("Discount code applied! You saved 10%.");
                    discountApplied = true;
                }
            }
            catch
            {
                Console.WriteLine("Invalid discount code.");
            }

            decimal total = subtotal + tax - discount;

            Console.WriteLine("---------------------------");
            Console.WriteLine("Your Total is:");
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Tax: ${tax:F2}");

            //if discount was applied, show the discount line
            if (discountApplied == (true))
            {
                Console.WriteLine($"Discount: -${discount:F2}");
            }
            Console.WriteLine($"Total: ${total:F2}");
            Console.WriteLine("---------------------------");

            Console.WriteLine("Press ENTER to pay:");
            string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    FileManager.DeleteData();

                    Console.WriteLine("---------------------------");
                    Console.WriteLine("Payment processed! Thank you for your purchase. Press ENTER to return to the Main Menu.");

                    string input2 = Console.ReadLine();

                    if (string.IsNullOrEmpty(input2))
                    {
                        return;
                    }
                }
            }
            
            }
    }
