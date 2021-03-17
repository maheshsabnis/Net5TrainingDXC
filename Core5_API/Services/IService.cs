using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core5_API.Services
{
	/// <summary>
	/// TEntity : The ENtity Class
	/// in, the input parameter
	/// TPk, the Primary key type
	/// where TEntity: class, the generic constraint the TEntity will always be class
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TPk"></typeparam>
	public interface IService<TEntity, in TPk> where TEntity: class
	{
		Task<IEnumerable<TEntity>> GetAsync();
		Task<TEntity> GetAsync(TPk id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TPk id,TEntity entity);
		Task<bool> DeleteAsync(TPk id);
	}
}
