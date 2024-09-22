using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Class;

namespace WarehouseApp.Interface
{
    public interface IWarehouse
    {
        
        double AvailableSpace { get; }
        bool AddProduct(Product product);
        bool RemoveProduct(string productName, int quantity);
        List<Product> GetProducts();
    }
}
