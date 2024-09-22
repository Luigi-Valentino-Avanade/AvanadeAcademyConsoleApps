using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Interface;

namespace WarehouseApp.Class
{
    public class Warehouse : IWarehouse
    {
        private readonly double _maxCapacity;
        private double availableSpace;
        private List<Product> products;
        public double AvailableSpace => availableSpace;
        public Warehouse(double capacity)
        {
            products = new List<Product>();
            _maxCapacity = capacity;
            availableSpace = capacity;
           
        }
        
        

        public bool AddProduct(Product product)
        {
            double requiredSpace = product.SizeInMq * product.Quantity;
            if (availableSpace >= requiredSpace)
            {
                Product existingProduct = products.FirstOrDefault(p => p.Name == product.Name);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += product.Quantity;
                }
                else
                {
                    products.Add(product);
                }
                availableSpace -= requiredSpace;
                return true;
                
            }
            else
            {
                return false;
                
            }
        }

        public bool RemoveProduct(string productName, int quantity) {
            Product product = products.FirstOrDefault(p => p.Name == productName);
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                availableSpace += product.SizeInMq * quantity;

                if (product.Quantity == 0)
                {
                    products.Remove(product);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> GetProducts() {
            return products;
        }   
    }
}
