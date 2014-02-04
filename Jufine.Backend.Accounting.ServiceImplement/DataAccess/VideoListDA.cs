using System;
using System.Linq;
using System.Data.Objects;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Accounting.DataContracts;

namespace Jufine.Backend.Accounting.DataAccess 
{
	internal class VideoListDA: DataBase<VideoListInfo, AccountingEntities>
	{
        
        internal static VideoListDA DAO = new VideoListDA();
		
        private VideoListDA(){ }
        
        protected override void AttachValue(VideoListInfo newEntity, VideoListInfo oldEntity)
		{
            oldEntity.ReplayCount = newEntity.ReplayCount;
            oldEntity.LinkUrl = newEntity.LinkUrl;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.UploadUser = newEntity.UploadUser;
            oldEntity.VideoType = newEntity.VideoType;
		}
        
        protected override IQueryable<VideoListInfo> SetWhereClause(QueryConditionInfo<VideoListInfo> queryCondition, IQueryable<VideoListInfo> query)
		{
                    if(queryCondition.Condtion.ID > 0)
                    {
                        query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                    }
                    if(queryCondition.Condtion.ReplayCount > 0)
                    {
                        query = query.Where(c => c.ReplayCount==queryCondition.Condtion.ReplayCount);
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.LinkUrl ))
                    {
                        query = query.Where(c => c.LinkUrl.StartsWith(queryCondition.Condtion.LinkUrl));
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.DisplayName ))
                    {
                        query = query.Where(c => c.DisplayName.StartsWith(queryCondition.Condtion.DisplayName));
                    }
                    if(queryCondition.Condtion.CreateDate!=null)
                    {
                        query = query.Where(c => c.CreateDate>queryCondition.Condtion.CreateDate);
                    }
                    if(queryCondition.Condtion.CreateDateTo!=null)
                    {
                        query = query.Where(c => c.CreateDate <=queryCondition.Condtion.CreateDateTo);
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.CreateUser ))
                    {
                        query = query.Where(c => c.CreateUser.StartsWith(queryCondition.Condtion.CreateUser));
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.UploadUser ))
                    {
                        query = query.Where(c => c.UploadUser.StartsWith(queryCondition.Condtion.UploadUser));
                    }
                    if(queryCondition.Condtion.VideoType > 0)
                    {
                        query = query.Where(c => c.VideoType==queryCondition.Condtion.VideoType);
                    }
            return query;
		}
        
        protected override IQueryable<VideoListInfo> SetOrder(QueryConditionInfo<VideoListInfo> queryCondition, IQueryable<VideoListInfo> query)
		{
            int count = queryCondition.OrderFileds.Count;
			if (count > 0)
			{
                for (int i = count; i > 0; i--)
				{
                    OrderFiledInfo item = queryCondition.OrderFileds[i - i];   
					    if (item.FieldName == "ID")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ID) : query.OrderByDescending(c => c.ID);
					    }
					    if (item.FieldName == "ReplayCount")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ReplayCount) : query.OrderByDescending(c => c.ReplayCount);
					    }
					    if (item.FieldName == "LinkUrl")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.LinkUrl) : query.OrderByDescending(c => c.LinkUrl);
					    }
					    if (item.FieldName == "DisplayName")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.DisplayName) : query.OrderByDescending(c => c.DisplayName);
					    }
					    if (item.FieldName == "CreateDate")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateDate) : query.OrderByDescending(c => c.CreateDate);
					    }
					    if (item.FieldName == "CreateUser")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateUser) : query.OrderByDescending(c => c.CreateUser);
					    }
					    if (item.FieldName == "UploadUser")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.UploadUser) : query.OrderByDescending(c => c.UploadUser);
					    }
					    if (item.FieldName == "VideoType")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.VideoType) : query.OrderByDescending(c => c.VideoType);
					    }
				}
			}
			else
			{
				query = query.OrderByDescending(c => c.ID);
			}
            return query;
		}
        
        
    }
}
