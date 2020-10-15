using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tekus.Datos.Repositories;

namespace Tekus.Datos
{
    public interface IUnitOfWork : IDisposable
    {
        IClientesRepository Clientes { get; }
        IPaisesRepository Paises { get; }
        IServiciosRepository Servicios { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IClientesRepository _clientesRepository;
        private IPaisesRepository _paisesRepository;
        private IServiciosRepository _serviciosRepository;
        private bool _disposed;

        public UnitOfWork()
        {
            var connectionString = DataConnection.ConnectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IClientesRepository Clientes
        {
            get { return _clientesRepository ?? (_clientesRepository = new ClientesRepository(_transaction)); }
        }
        public IPaisesRepository Paises
        {
            get { return _paisesRepository ?? (_paisesRepository = new PaisesRepository(_transaction)); }
        }
        public IServiciosRepository Servicios
        {
            get { return _serviciosRepository ?? (_serviciosRepository = new ServiciosRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _clientesRepository = null;
            _paisesRepository = null;
            _serviciosRepository = null;
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
