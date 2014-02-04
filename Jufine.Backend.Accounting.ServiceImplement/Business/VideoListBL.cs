
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Accounting.DataContracts;
using Jufine.Backend.Accounting.ServiceContracts;
using Jufine.Backend.Accounting.DataAccess;

namespace Jufine.Backend.Accounting.Business
{
	public class VideoListBL :IVideoListService
	{
       
		public VideoListInfo Get(Int32 id)
        {
            try
            {
                return VideoListDA.DAO.Get(id);
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
                 VideoListDA.DAO.Delete(id);
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
                VideoListDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
    
        public List<VideoListInfo> GetAll()
        {
            try
            {
                return VideoListDA.DAO.GetAll();
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(VideoListInfo videoList)
        {
            try
            {
                VideoListDA.DAO.Create(videoList);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(VideoListInfo videoList)
        {
            try
            {
                VideoListDA.DAO.Update(videoList);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
        
        public QueryResultInfo<VideoListInfo> Query(QueryConditionInfo<VideoListInfo> queryCondition)
        {
           try
            {
               return VideoListDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            } 
        }

    }
}
