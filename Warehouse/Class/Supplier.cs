using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Interface;

namespace WarehouseApp.Class
{
    public class Supplier : ISupplier
    {
        
        private List<Product> _products{ get; set; }

        public Supplier() {
            
            _products = new List<Product>();
        }

        public void CreateNewProduct(string name, double price, double sizeMq, int quantity)
        {
            Product newProduct = new Product
            {
                Name = name,
                Price = price,
                SizeInMq = sizeMq,
                Quantity = quantity
            };

            _products.Add(newProduct);
        }

        public void UpdateProductQuantity(string productName, int quantity)
        {
            Product existingProduct = _products.FirstOrDefault(p => p.Name == productName);
            if (existingProduct != null)
            {
                existingProduct.Quantity += quantity;
                Console.WriteLine($"Product '{productName}' quantity updated successfully!");
            }
            else
            {
                Console.WriteLine("Produc not found!");
            }
        }

        public List<Product> GetAvailableProducts()
        {
            return _products.Where(w => w.Quantity > 0).ToList();
        }
    }
}
