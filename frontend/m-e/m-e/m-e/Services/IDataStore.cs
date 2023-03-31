using m_e.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace m_e.Services
{
    public interface IDataStore<T>
    {
        Task<Profile> GetProfileAsync(bool forceRefresh = false);
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
