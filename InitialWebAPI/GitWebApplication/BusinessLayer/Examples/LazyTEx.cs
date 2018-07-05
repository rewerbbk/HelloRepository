using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{
    class LazyTEx
    {
    }

    public class Customer
    {
        string Name { get; set; }
        Lazy<Orders> _orders = new Lazy<Orders>();
        Lazy<Orders> _ordersWithParam = new Lazy<Orders>(() => new Orders(100));

        public class Orders
        {
            public Orders()
            {
                //make expensive db call to load all MyOrders        
            }

            public Orders(int latest)
            {
                //make expensive db call to load most recent #latest MyOrders 
            }

            List<KeyValuePair<string, int>> MyOrders;
        }
    }
}
