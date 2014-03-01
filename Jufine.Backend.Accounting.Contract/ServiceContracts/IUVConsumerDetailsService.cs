using System;
using System.Collections.Generic;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Accounting.DataContracts;

namespace Jufine.Backend.Accounting.ServiceContracts
{
	public interface IUVConsumerDetailsService: IServiceBase
	{
		
        
        UVConsumerDetails Get(Int32 id);
        List<UVConsumerDetails> GetAll();
        void Create(UVConsumerDetails uVConsumerDetails);
        void Update(UVConsumerDetails uVConsumerDetails);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UVConsumerDetails> Query(QueryConditionInfo<UVConsumerDetails> queryCondition);
    }
}
