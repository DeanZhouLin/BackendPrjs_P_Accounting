using System;
using System.Collections.Generic;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Accounting.DataContracts;

namespace Jufine.Backend.Accounting.ServiceContracts
{
	public interface IVideoListService: IServiceBase
	{
		
        
        VideoListInfo Get(Int32 id);
        List<VideoListInfo> GetAll();
        void Create(VideoListInfo videoList);
        void Update(VideoListInfo videoList);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<VideoListInfo> Query(QueryConditionInfo<VideoListInfo> queryCondition);
    }
}
