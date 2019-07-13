using Apex.DAL.UOW;
using Apex.Domain.DBModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Apex.Services
{
	public class UsersService
	{
		public IUnitOfWork uow;

		public UsersService()
		{
			uow = new UnitOfWork();
		}

		//GetPaginatedUsers
		public List<User> GetPaginatedUsers()
		{
			return uow.UserRepository.GetPaginatedRecords().ToList();
		}

		//GetUser
		public User GetUser(int id)
		{
			
			return uow.UserRepository.GetRecordById(id);
		}

		//AddUser
		public bool AddUser(User user)
		{
			bool retVal = false;

			uow.UserRepository.InsertRecord(user);

			try
			{
				uow.SaveDBChanges();
				retVal = true;
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				throw;
			}

			return retVal;
		}

		//EditUser
		public bool EditUser(User user)
		{
			bool retVal = false;

			uow.UserRepository.UpdateRecord(user);

			try
			{
				uow.SaveDBChanges();
				retVal = true;
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				throw;
			}

			return retVal;
		}

		//DeleteUser
		public bool DeleteUser(int id)
		{
			bool retVal = false;

			uow.UserRepository.DeleteRecord(id);

			try
			{
				uow.SaveDBChanges();
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				throw;
			}

			return retVal;
		}

		//user login
		//public User LoginUser(UserLogin userLogin)
		public User LoginUser(User userLogin)
		{
			User retVal = null;

			return retVal;
		}

		//business rules validataion

	}
}
