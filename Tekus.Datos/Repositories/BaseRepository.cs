using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Tekus.Datos.Filters;
using Tekus.Entidades;

namespace Tekus.Datos.Repositories
{
    public interface IReadRepository<TReadModel, TFilter>
    where TReadModel : class
    where TFilter : FilterBase
    {
        Task<QueryResult> Find(TFilter filter, Pagination queryControl);
        Task<QueryResult> Find(Pagination queryControl);
        Task<QueryResult> Find(TFilter filter);
        Task<QueryResult> Find();

        Task<TReadModel> FirstOrDefault(TFilter filter);
        Task<TReadModel> GetReadModel<TypeofId>(TypeofId id);
        Task<int> Count(TFilter filter);
    }

    public interface IWriteRepository<TWriteModel>
        where TWriteModel : class
    {
        Task<TWriteModel> GetWriteModel<TypeofId>(TypeofId id);
        Task<int> Create(TWriteModel entity);
        Task<int> Update(TWriteModel entity);
        Task<int> Delete<TypeofId>(TypeofId id);
        /// <summary>
        /// Elimina todos los registros de la tabla y reinicia el identity ID
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<int> Reset();
    }

    public abstract class BaseRepository<TWriteModel, TReadModel, TFilter> : IReadRepository<TReadModel, TFilter>, IWriteRepository<TWriteModel>
        where TReadModel : class
        where TFilter : FilterBase
        where TWriteModel : class
    {
        public abstract string CreateQuery { get; }
        public abstract string TableName { get; }
        public abstract string UpdateQuery { get; }
        public virtual string DeleteQuery => $"delete {TableName} where Id = @id";
        public virtual string CountQuery => $"select count(*) from  {TableName} {_whereClause}";
        public virtual string ExtistsQuery => $"select count(*) from  {TableName}";
        public virtual string QueryFirstReadModel => $"select * from {TableName} where Id = @id";
        public virtual string QueryFirstWriteModel => $"select * from {TableName} where Id = @id";
        public virtual string GetAllQuery => $"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( {_orderbyClause} ) AS RowNum, * FROM {TableName} {_whereClause}) AS QueryResult WHERE RowNum >= @MinRow AND RowNum <= @MaxRow ORDER BY RowNum";
        protected virtual void SetWhereClause(TFilter filter) { }

        protected string _baseClause;
        protected string _orderbyClause;
        protected string _whereClause;

        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
        private void SetBaseClause(Pagination queryControl)
        {
            _baseClause = $"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( {_orderbyClause} ) AS RowNum, * FROM {TableName} {_whereClause}) AS QueryResult WHERE RowNum >= {queryControl.MinRow} AND RowNum <= {queryControl.MaxRow} ORDER BY RowNum";
        }
        public async virtual Task<int> Count(TFilter filter)
        {
            SetWhereClause(filter);
            return await Connection.QueryFirstAsync<int>(CountQuery, filter,Transaction);
        }
        public async Task<int> Create(TWriteModel entity)
        {
            return await Connection.ExecuteAsync(CreateQuery, entity, Transaction);
        }
        public async Task<int> Delete<TypeofId>(TypeofId id)
        {
            return await Connection.ExecuteAsync(DeleteQuery, new { id }, Transaction);
        }
        public async virtual Task<bool> Exists(TFilter filter)
        {
            SetWhereClause(filter);
            return await Connection.QueryFirstAsync<bool>(string.Concat(ExtistsQuery), filter, Transaction);
        }
        
        public async virtual Task<QueryResult> Find(TFilter filter, Pagination queryControl)
        {
            /// Obtener el número de filas que retornará la consulta
            int count = await this.Count(filter);
            // Calcular los valores para armar paginación
            queryControl.SetPagination(count);
            // Armar sentencia order by
            _orderbyClause = $"order by {filter.OrderByColumnName} {queryControl.OrderType}";
            // Armar toda la sentencia que se envía a base de datos
            SetBaseClause(queryControl); 

            return new QueryResult()
            {
                TotalRows = count,
                RowsPerPage = queryControl.RowsPerPage,
                Pages = queryControl.TotalPages,
                CurrentPage = queryControl.Page,
                Data = await Connection.QueryAsync<TReadModel>(_baseClause, filter, Transaction)
            };
        }
        public async virtual Task<QueryResult> Find(TFilter filter)
        {
            return await Find(filter, new Pagination());
        }
        public async virtual Task<QueryResult> Find(Pagination pagination)
        {
            return await Find(Activator.CreateInstance<TFilter>(), pagination);
        }
        public async virtual Task<QueryResult> Find()
        {
            return await Find(Activator.CreateInstance<TFilter>(), new Pagination());
        }
        public async virtual Task<TReadModel> FirstOrDefault(TFilter filter)
        {
            SetWhereClause(filter);
            return await Connection.QueryFirstOrDefaultAsync<TReadModel>(GetAllQuery, filter, Transaction);
        }
        public async Task<TReadModel> GetReadModel<TypeofId>(TypeofId id)
        {
            return await Connection.QueryFirstAsync<TReadModel>(QueryFirstReadModel, new { id }, Transaction);
        }

        public async Task<TWriteModel> GetWriteModel<TypeofId>(TypeofId id)
        {
            return await Connection.QueryFirstOrDefaultAsync<TWriteModel>(QueryFirstWriteModel, new { id }, Transaction);
        }

        public async Task<int> Update(TWriteModel entity)
        {
            return await Connection.ExecuteAsync(UpdateQuery, entity, Transaction);
        }

        public async Task<int> Reset()
        {
            return await Connection.ExecuteAsync($"DELETE FROM {TableName}; DBCC CHECKIDENT ('{TableName}', RESEED, 0);", null, Transaction);
        }
    }
}