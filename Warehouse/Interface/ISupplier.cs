using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Class;

namespace WarehouseApp.Interface
{
    public interface ISupplier
    {
        void CreateNewProduct(string name, double price, double sizeMq, int quantity);
        void UpdateProductQuantity(string productName, int quantity);
        List<Product> GetAvailableProducts();
    }
}
