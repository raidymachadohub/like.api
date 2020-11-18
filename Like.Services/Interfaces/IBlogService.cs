using Like.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Like.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAll();
        Task<Blog> GetById(int id);
        Task Put(int id, Blog obj);
        Task<Blog> Post(Blog obj);
        Task<Blog> Delete(int id);
        void Like(int id);
        void UnLike(int id);
    }
}
