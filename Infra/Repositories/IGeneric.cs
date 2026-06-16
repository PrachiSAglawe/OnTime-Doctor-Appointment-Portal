using Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IGeneric<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T>GetByID(Int64 id);
        Task<RepoResultDto> Insert(T entity);
        Task<RepoResultDto> Update(T entity);
        Task<RepoResultDto> Delete(Int64 id);
    }
}
