using System;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Accounting.DataContracts
{
	[Serializable]
	public partial class VideoListInfo: DataContractBase
	{	
		public Int32 ReplayCount{get;set;}
		public string LinkUrl{get;set;}
		public string DisplayName{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string CreateUser{get;set;}
		public string UploadUser{get;set;}
		public Int32 VideoType{get;set;}
		
	}
}