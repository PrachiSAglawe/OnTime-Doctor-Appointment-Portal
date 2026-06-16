using Infra.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
   public class GenericRepo<T>:IGeneric<T> where T : class
    {
        DbContext db;

        public GenericRepo(DbContext db)
        {
            this.db = db;
        }

        public async Task<RepoResultDto> Delete(long id)
        {
           RepoResultDto result =new RepoResultDto();
            try
            {
                var rec = await this.db.Set<T>().FindAsync(id);
                this.db.Set<T>().Remove(rec);
                await this.db.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Record Deleted Successfully...!";

            }
            catch (Exception ex) { 
                result.IsSuccess = false;
                result.Message = ex.Message;
            
            
            }
            return result;
        }

        public async Task<List<T>> GetAll()
        {
           return await this.db.Set<T>().ToListAsync();   
        }

        public async Task<T> GetByID(long id)
        {
            var res = await this.db.Set<T>().FindAsync(id);
            return res;
        }

        public async Task<RepoResultDto> Insert(T entity)
        {
            RepoResultDto result = new RepoResultDto();

            try
            {
                await this.db.Set<T>().AddAsync(entity);
                await this.db.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Record Created Sucessfully...!";
            }
            catch(Exception err)
            {
                result.IsSuccess = false;
                result.Message = err.Message;
            }
            return result;
        }

        public async Task<RepoResultDto> Update(T entity)
        {
            RepoResultDto result = new RepoResultDto();

            try
            {
              this.db.Entry(entity).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Record Updated Sucessfully..!";
            }catch(Exception err)
            {
                result.IsSuccess = false;
                result.Message = err.Message;
            }
            return result;
        }
    }
}
