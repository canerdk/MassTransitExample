using Response.Api.Entities;
using Response.Api.Repositories.Abstract;

namespace Response.Api.Repositories.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<Order> GetById(string id)
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = "1", OrderNo = "2323", UserId = 1 },
                new Order() { Id = "2", OrderNo = "456234", UserId = 2 },
                new Order() { Id = "3", OrderNo = "435445", UserId = 2 },
                new Order() { Id = "4", OrderNo = "123123123", UserId = 3 },
            };

            var result = orders.FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
