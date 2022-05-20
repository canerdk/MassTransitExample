using Response.Api.Entities;

namespace Response.Api.Repositories.Abstract
{
    public interface IOrderRepository
    {
        Task<Order> GetById(string id);
    }
}
