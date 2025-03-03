#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
using System;
using System.Collections.Generic;

class Program
{
    // Structure to represent an inventory item
    class Item
    {
        public bool Perishable { get; set; }
        public int Qty { get; set; }
        public required string Description { get; set; }
    }

    // Dictionary to store inventory items (Initialized properly)
    static Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    static void AddItem()
    {
        Console.Write("Enter item name: ");

        string name = Console.ReadLine().Trim();


        // Check if item already exists
        if (inventory.ContainsKey(name))
        {
            Console.WriteLine("Item already exists. Use edit option to update.");
            return;
        }

        Console.Write("Is the item perishable? (yes/no): ");
        bool perishable = Console.ReadLine().Trim().ToLower() == "yes";

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine().Trim(), out int qty))
        {
            Console.WriteLine("Invalid quantity. Please enter a valid number.");
            return;
        }

        Console.Write("Enter item description: ");
        string description = Console.ReadLine().Trim();

        // Ensure inventory dictionary is initialized before adding
        if (inventory == null)
        {
            inventory = new Dictionary<string, Item>();
        }

        inventory[name] = new Item { Perishable = perishable, Qty = qty, Description = description };
        Console.WriteLine($"Item '{name}' added successfully.");
    }

    static void ViewInventory()
    {
        if (inventory.Count <= 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("\nCurrent Inventory:");
        foreach (var item in inventory)
        {
            string perishableStatus = item.Value.Perishable ? "Yes" : "No";
            Console.WriteLine($"- {item.Key}: Perishable: {perishableStatus}, Qty: {item.Value.Qty}, Description: {item.Value.Description}");
        }
    }

    static void EditItem()
    {
        string editChoice;
        foreach (var item in inventory)
        {
            string perishableStatus = item.Value.Perishable ? "Yes" : "No";
            Console.WriteLine($"- {item.Key}: Perishable: {perishableStatus}, Qty: {item.Value.Qty}, Description: {item.Value.Description}");
        }
        do {
            Console.Write("\nWhich item would you like to edit?: ");
            editChoice = Console.ReadLine().Trim();
            if (inventory.ContainsKey(editChoice)) {
                break;
            }
            else {
                Console.WriteLine("Invalid selection. Try again.");
            }
        } while (true);
        Console.WriteLine("----------------------------------");
        Item itemChoice = inventory[editChoice];
        Console.WriteLine("\nCurrent item details:");
        Console.WriteLine($"Name: {editChoice}");
        Console.WriteLine($"Description: {itemChoice.Description}");
        Console.WriteLine($"Quantity: {itemChoice.Qty}");
        Console.WriteLine($"Perishable: {itemChoice.Perishable}");        
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"What would you like to edit about {editChoice}?");
        Console.WriteLine("1. Change Name");
        Console.WriteLine("2. Change Quantity");
        Console.WriteLine("3. Change Perishability");
        Console.WriteLine("4. Change Description");
        string? valueToEdit = Console.ReadLine();
        switch (valueToEdit) {
            case "1":
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine().Trim();
                if (inventory.ContainsKey(newName)) {
                    Console.WriteLine($"An item called {newName} already exists.");
                }
                else {
                    Item itemCopy = itemChoice;
                    inventory.Remove(editChoice);
                    inventory.Add(newName, itemCopy);
                    editChoice = newName;
                    Console.WriteLine("Name change success.");
                }
                break;
            case "4":
                Console.Write("Enter new description: ");
                string? newDescription = Console.ReadLine();
                itemChoice.Description = newDescription;
                Console.WriteLine("Description updated successfully.");
                break;
            case "2":
                Console.Write("Enter new quantity: ");
                itemChoice.Qty = Convert.ToInt32(Console.ReadLine().Trim());
                break;
            case "3":
                Console.Write($"The current state of this item's perishability is {itemChoice.Perishable}. If you want to change this, press 1: ");
                int perishChange = int.Parse(Console.ReadLine());
                if (perishChange == 1) {
                    itemChoice.Perishable = !itemChoice.Perishable;
                    Console.Write("Perishability was changed.");
                }
                break;
        }


    }

    static void RemoveItem()
    {
        string removeChoice;
        foreach (var item in inventory)
        {
            string perishableStatus = item.Value.Perishable ? "Yes" : "No";
            Console.WriteLine($"- {item.Key}: Perishable: {perishableStatus}, Qty: {item.Value.Qty}, Description: {item.Value.Description}");
        }
        do {
            Console.Write("\nWhich item would you like to remove?: ");
            removeChoice = Console.ReadLine().Trim();
            if (inventory.ContainsKey(removeChoice)) {
                break;
            }
            else {
                Console.WriteLine("Invalid selection. Try again.");
            }
        } while (true);
        Console.WriteLine("----------------------------------");
        Item itemChoice = inventory[removeChoice];
        Console.WriteLine("\nCurrent item details:");
        Console.WriteLine($"Name: {removeChoice}");
        Console.WriteLine($"Description: {itemChoice.Description}");
        Console.WriteLine($"Quantity: {itemChoice.Qty}");
        Console.WriteLine($"Perishable: {itemChoice.Perishable}");        
        Console.WriteLine("----------------------------------");
        Console.Write("Are you sure you want to remove this item? (yes/no) ");
        bool deleteConfirm = Console.ReadLine().Trim().ToLower() == "yes";
        if (deleteConfirm) {
            inventory.Remove(removeChoice);
            Console.WriteLine("Item successfully removed");
        }
        

    }

    static int Main()
    {
        while (true)
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Add Item");
            Console.WriteLine("3. Edit Item");
            Console.WriteLine("4. Remove Item");
            Console.WriteLine("5. Quit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1": ViewInventory(); break;
                case "2": AddItem(); break;
                case "3": EditItem(); break;
                case "4": RemoveItem(); break;
                case "5":
                    Console.WriteLine("Exiting Inventory System.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }
}
