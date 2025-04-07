namespace Cinema.Infrastructure.Data
{
    public interface IRepository
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// Get all records as no tracking
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> AllReadonly<T>() where T : class;

        /// <summary>
        /// Add record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Add records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Save all changes
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
