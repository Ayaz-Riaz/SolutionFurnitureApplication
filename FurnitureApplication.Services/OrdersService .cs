using FurnitureApplication.Database;
using FurnitureApplication.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApplication.Services
{
    public class OrdersService
    {
        #region singleton
        public static OrdersService Instance
        {
            get
            {
                if (instance == null) instance = new OrdersService();
                return instance;
            }
        }
        private static OrdersService instance { get; set; }

        private OrdersService()
        {
        }
        #endregion

        public List<Order> SearchOrders(string userID, string status, int pageNo, int pageSize)
        {
            using (var context = new FAContext())
            {
                var Orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    Orders = Orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(status))
                {
                    Orders = Orders.Where(x => x.status.ToLower().Contains(status.ToLower())).ToList();
                }

                return Orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchOrdersCount(string userID, string status)
        {
            using (var context = new FAContext())
            {
                var Orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    Orders = Orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(status))
                {
                    Orders = Orders.Where(x => x.status.ToLower().Contains(status.ToLower())).ToList();
                }

                return Orders.Count;
            }
        }

        public Order GetOrderByID(int ID)
        {
            using (var context = new FAContext())
            {
                return context.Orders.Where(x => x.ID == ID).Include(x=>x.OrderItems).Include("orderItems.product").FirstOrDefault();
            }
        }

        public object UpdateOrderStatus(int ID, string status)
        {
            using (var context = new FAContext())
            {
                var order = context.Orders.Find(ID);
                order.status = status;
                context.Entry(order).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }
    }
}
