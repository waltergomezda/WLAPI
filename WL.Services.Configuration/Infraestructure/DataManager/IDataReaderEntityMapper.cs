using System.Data;

namespace Infraestructure.DataManager
{
    public interface IDataReaderEntityMapper<out TEntity>
    {
        TEntity MapToEntity(IDataReader row);
    }
}