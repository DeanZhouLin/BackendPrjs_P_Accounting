
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Accounting.DataContracts;
using Jufine.Backend.Accounting.ServiceContracts;
using Jufine.Backend.Accounting.DataAccess;

namespace Jufine.Backend.Accounting.Business
{
	public class UVConsumerDetailsBL :IUVConsumerDetailsService
	{
       
		public UVConsumerDetails Get(Int32 id)
        {
            try
            {
                return UVConsumerDetailsDA.DAO.Get(id);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
        
        public void Delete(Int32 id)
        {
            try
            {
                 UVConsumerDetailsDA.DAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
        
        public void BatchDelete(List<Int32> keyList)
        {
            try
            {
                UVConsumerDetailsDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
    
        public List<UVConsumerDetails> GetAll()
        {
            try
            {
                return UVConsumerDetailsDA.DAO.GetAll();
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UVConsumerDetails uVConsumerDetails)
        {
            try
            {
                UVConsumerDetailsDA.DAO.Create(uVConsumerDetails);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UVConsumerDetails uVConsumerDetails)
        {
            try
            {
                UVConsumerDetailsDA.DAO.Update(uVConsumerDetails);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
        
        public QueryResultInfo<UVConsumerDetails> Query(QueryConditionInfo<UVConsumerDetails> queryCondition)
        {
           try
            {
               return UVConsumerDetailsDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            } 
        }

    }
}
