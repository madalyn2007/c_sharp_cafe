using System.IO;
using System.Linq;
using System.Collections.Generic;

public class FileManager
{

    //save cart to file
    public void SaveFile(List<Item> cartItems, List<int> cartQuantities)
    {
        var lines = cartItems.Select((item, x) => $"{item.Name},{cartQuantities[x]}");
        File.WriteAllLines("mycart.txt", lines);
    }

    public static void DeleteData()
    {
        if (File.Exists("mycart.txt"))
        {
            File.Delete("mycart.txt");
        }
    }

    public static void LoadCart(List<Item> cartItems, List<int> cartQuantities, List<Item> menuItems)
    {
        //check if file exists
        if (!File.Exists("mycart.txt"))
        {
            return;
        }

        // read file lines and store in cartData array
        string[] cartData = File.ReadAllLines("mycart.txt");


        //loop through each line in the array
        foreach (var line in cartData)
        {
            //split line into name and quantity
            string[] split = line.Split(',');
            //if split length is less than 2, skip line
            if (split.Length < 2) continue;

            string name = split[0].Trim();
            int qty = int.Parse(split[1].Trim());

            //set item reference to null initially
            Item menuItem = null;

            //loop through and find each item in menuItems, stored in x variable, matching by name
            foreach (var x in menuItems)
            {
                if (x.Name == name)
                {
                    //if found, set menuItem to x and break loop
                    menuItem = x;
                    break;
                }
            }

            //if menuItem is found, add it to cartItems and its quantity to cartQuantities
            if (menuItem != null)
            {
                cartItems.Add(menuItem);
                cartQuantities.Add(qty);
            }
            
        }
        
    }
}

