using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Interface;

namespace WarehouseApp.Class
{
    public class WarehouseManager
    {
        private readonly IWarehouse _warehouse;
        private readonly ISupplier _supplier;

        public WarehouseManager(IWarehouse warehouse, ISupplier supplier)
        {
            _warehouse = warehouse;
            _supplier = supplier;
        }

        public void AddProductToWarehouse(int selectedProductIndex, int quantity)
        {
            List<Product> availableProducts = _supplier.GetAvailableProducts();

            if (selectedProductIndex < 0 || selectedProductIndex >= availableProducts.Count)
            {
                Console.WriteLine("Invalid product seleciton.");
                return;
            }

            Product selectedProduct = availableProducts[selectedProductIndex];

            if (quantity <= 0 || quantity > selectedProduct.Quantity)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            Product productToAdd = new Product
            {
                Name = selectedProduct.Name,
                SizeInMq = selectedProduct.SizeInMq,
                Price = selectedProduct.Price,
                Quantity = quantity
            };
            
            if (_warehouse.AddProduct(productToAdd))
            {
                _supplier.UpdateProductQuantity(selectedProduct.Name, -quantity);
                Console.WriteLine($"Product {productToAdd.Name} has been succesfully added to the Warehouse");
            }
            else
            {
                Console.WriteLine($"Not enough space to add {productToAdd.Name} to the Warehouse.");
            }
        }

        public void RemoveProductFromWarehouse(string productName, int quantity)
        {
            if (_warehouse.RemoveProduct(productName, quantity))
            {
                Console.WriteLine($"Product {productName} has been successfully removed from the warehouse.");
            }
            else
            {
                Console.WriteLine($"Failed to remove {quantity} of {productName}. Either the product doesn't exist or quantity is insufficient.");
            }
        }

        public void ShowWarehouseStatus()
        {
            List<Product> products = _warehouse.GetProducts();
            Console.WriteLine("Current Warehouse Status:");
            foreach (Product product in products)
            {
                Console.WriteLine($"Product: {product.Name}, Quantity: {product.Quantity}, Size: {product.SizeInMq}m²");
            }
            Console.WriteLine($"Available space: {_warehouse.AvailableSpace}m²");
        }

        public List<Product> GetAvailableProductsFromSupplier()
        {
            return _supplier.GetAvailableProducts();
        }
    }
}
