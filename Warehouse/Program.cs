using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Class;
using WarehouseApp.Interface;

namespace WarehouseApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ISupplier supplier = new Supplier();
            IWarehouse warehouse = new Warehouse(1000);
            WarehouseManager warehouseManager = new WarehouseManager(warehouse, supplier);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Warehouse Management System ---");
                Console.WriteLine("1. Supplier Operations");
                Console.WriteLine("2. Warehouse Operations");
                Console.WriteLine("3. Exit");
                Console.Write("Please select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowSupplierMenu(supplier);
                        break;
                    case "2":
                        ShowWarehouseMenu(warehouseManager, supplier);
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }

            
        }

        public static void ShowSupplierMenu(ISupplier supplier)
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.WriteLine("\n--- Supplier Menu ---");
                Console.WriteLine("1. Create New Product");
                Console.WriteLine("2. Update Product Quantity");
                Console.WriteLine("3. Show Available Products");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewProduct(supplier);
                        break;
                    case "2":
                        UpdateProductQuantity(supplier);
                        break;
                    case "3":
                        ShowAvailableProducts(supplier);
                        break;
                    case "4":
                        backToMain = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        static void ShowWarehouseMenu(WarehouseManager warehouseManager, ISupplier supplier)
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.WriteLine("\n--- Warehouse Menu ---");
                Console.WriteLine("1. Get Products from Supplier");
                Console.WriteLine("2. Show Warehouse Status");
                Console.WriteLine("3. Remove Product from Warehouse");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductFromSupplierToWarehouse(warehouseManager);
                        break;
                    case "2":
                        warehouseManager.ShowWarehouseStatus();
                        break;
                    case "3":
                        RemoveProductFromWarehouse(warehouseManager);
                        break;
                    case "4":
                        backToMain = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public static void AddProductFromSupplierToWarehouse(WarehouseManager warehouseManager)
        {
            List<Product> availableProducts = warehouseManager.GetAvailableProductsFromSupplier();

            if (availableProducts.Count == 0)
            {
                Console.WriteLine("No products available from the supplier");
                return;
            }

            Console.WriteLine("\n--- Available Products from Supplier");
            for (int i = 0; i < availableProducts.Count; i++)
            {
                var product = availableProducts[i];
                Console.WriteLine($"{i + 1}. {product.Name} - {product.Quantity} units available, {product.SizeInMq} m² each");
            }

            Console.Write("Select the product number to add to the warehouse: ");
            int selectedProductIndex = ReadIntInput() - 1;

            Console.Write("Enter quantity to add: ");
            int quantityToAdd = ReadIntInput();

            warehouseManager.AddProductToWarehouse(selectedProductIndex, quantityToAdd);

        }
        public static void CreateNewProduct(ISupplier supplier)
        {
            Console.WriteLine("\n--- Create New Product ---");
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product price: ");
            double price = ReadDoubleInput();
            Console.Write("Enter product size in square meters: ");
            double sizeMq = ReadDoubleInput();
            Console.Write("Enter product quantity: ");
            int quantity = ReadIntInput();

            supplier.CreateNewProduct(name, price, sizeMq, quantity);
            Console.WriteLine($"Product '{name}' created successfully");
        }

        public static void UpdateProductQuantity(ISupplier supplier)
        {
            Console.WriteLine("\n--- Update Product Quantity ---");
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter new quantity to add: ");
            int quantity = ReadIntInput();

            supplier.UpdateProductQuantity(productName, quantity);
            
        }

        public static void ShowAvailableProducts(ISupplier supplier)
        {
            Console.WriteLine("\n--- Available Products from Supplier ---");
            List<Product> products = supplier.GetAvailableProducts();
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    Console.WriteLine($"{product.Name}: {product.Quantity} units available, {product.SizeInMq} m2 each.");
                }
            }
            else
            {
                Console.WriteLine("No products available.");
            }
        }

        static void RemoveProductFromWarehouse(WarehouseManager warehouseManager)
        {
            Console.WriteLine("\n--- Remove Product from Warehouse ---");
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter quantity to remove: ");
            int quantity = ReadIntInput();

            warehouseManager.RemoveProductFromWarehouse(productName, quantity);
            Console.WriteLine($"Removed {quantity} units of '{productName}' from the warehouse.");
        }

        public static double ReadDoubleInput()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number: ");
                }
            }
        }

        public static int ReadIntInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number: ");
                }
            }
        }
    }
}
