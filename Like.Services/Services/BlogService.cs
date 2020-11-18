using Like.Domain;
using Like.Domain.Entities;
using Like.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Like.Services.Services
{
    public class BlogService : IBlogService
    {
        private readonly LikeContext _context;

        private bool disposed = false;
        public BlogService(LikeContext context)
        {
            _context = context;
        }

        public async Task<Blog> Delete(int id)
        {
            var obj = await _context.Set<Blog>().FindAsync(id);

            if (obj == null)
            {
                throw new NotImplementedException();
            }

            _context.Set<Blog>().Remove(obj);
            await _context.SaveChangesAsync();

            return obj;
        }


        public async Task<Blog> GetById(int id)
        {
            return await _context.Set<Blog>().FindAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Set<Blog>().ToListAsync();
        }

        public async Task<Blog> Post(Blog obj)
        {
            _context.Set<Blog>().Add(obj);
            var rs = await _context.SaveChangesAsync();

            return _context.Set<Blog>().Find(rs);
        }

        public async Task Put(int id, Blog obj)
        {
            if (id > 0)
            {
                _context.Entry(obj).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Like(int id)
        {
            IncrementLike(id);
        }

        public void UnLike(int id)
        {
            DecrementLike(id);
        }

        public void DecrementLike(int id)
        {
            Blog blog = _context.Set<Blog>().Find(id);
            if (blog.QuantityLike <= 0)
            {
                blog.QuantityLike = 0;
            }
            else
            {
                blog.QuantityLike--;
            }
            blog.DtUpdate = DateTime.Now;
            _context.Entry(blog).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void IncrementLike(int id)
        {
            Blog blog = _context.Set<Blog>().Find(id);
            if (blog.QuantityLike == 0)
            {
                blog.QuantityLike = 1;
            }
            else
            {
                blog.QuantityLike++;
            }
            blog.DtUpdate = DateTime.Now;
            _context.Entry(blog).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}


