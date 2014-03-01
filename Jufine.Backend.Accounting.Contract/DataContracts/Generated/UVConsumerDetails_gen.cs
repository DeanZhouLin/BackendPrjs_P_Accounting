using System;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Accounting.DataContracts
{
	[Serializable]
	public partial class UVConsumerDetails: DataContractBase
	{	
		public decimal Amount{get;set;}
		public Int32 Type{get;set;}
		public Int32 MemoTypeID{get;set;}
		public string Memo{get;set;}
		public string CreateUser{get;set;}
		public DateTime CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public Int32? ResponsiblePersonID{get;set;}
		public Int32? Status{get;set;}
		public string TypeText{get;set;}
		public string MemoTypeText{get;set;}
		public string ResponsiblePersonText{get;set;}
		
	}
}