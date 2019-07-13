using System;
using Apex.DAL.Repositories;
using System.Data;
using System.Data.SqlClient;
using Apex.Utils;

namespace Apex.DAL.UOW
{
	public class UnitOfWork : IUnitOfWork
	{
		//context
		private ApexDBContext _context;
		private IDbConnection _connection;

		//repositories
		private IUserRepository _userRepository;

		//ctor
		public UnitOfWork()
		{
			_context = new ApexDBContext();
			_connection = new SqlConnection(WebConfigHelper.ApexDbConnectionString);
		}

		public UnitOfWork(ApexDBContext context)
		{
			_context = context;
		}

		public UnitOfWork(IDbConnection connection)
		{
			_connection = connection;
		}

		public UnitOfWork(ApexDBContext context, IDbConnection connection)
		{
			_context = context;
			_connection = connection;
		}

		public IUserRepository UserRepository
		{
			get
			{
				if (_userRepository == null)
					_userRepository = new UserRepository(_context, _connection);

				return _userRepository;
			}
		}

		public void SaveDBChanges()
		{
			_context.SaveChanges();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~UnitOfWork() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			_connection.Close();
			
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
