using System;
using System.Linq;
using System.Data.Objects;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Accounting.DataContracts;

namespace Jufine.Backend.Accounting.DataAccess 
{
	internal class UVConsumerDetailsDA: DataBase<UVConsumerDetails, AccountingEntities>
	{
        
        internal static UVConsumerDetailsDA DAO = new UVConsumerDetailsDA();
		
        private UVConsumerDetailsDA(){ }
        
        protected override void AttachValue(UVConsumerDetails newEntity, UVConsumerDetails oldEntity)
		{
            oldEntity.Amount = newEntity.Amount;
            oldEntity.Type = newEntity.Type;
            oldEntity.MemoTypeID = newEntity.MemoTypeID;
            oldEntity.Memo = newEntity.Memo;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.ResponsiblePersonID = newEntity.ResponsiblePersonID;
            oldEntity.Status = newEntity.Status;
            oldEntity.TypeText = newEntity.TypeText;
            oldEntity.MemoTypeText = newEntity.MemoTypeText;
            oldEntity.ResponsiblePersonText = newEntity.ResponsiblePersonText;
		}
        
        protected override IQueryable<UVConsumerDetails> SetWhereClause(QueryConditionInfo<UVConsumerDetails> queryCondition, IQueryable<UVConsumerDetails> query)
		{
                    if(queryCondition.Condtion.ID > 0)
                    {
                        query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                    }
                    if(queryCondition.Condtion.Amount > 0)
                    {
                        query = query.Where(c => c.Amount==queryCondition.Condtion.Amount);
                    }
                    if(queryCondition.Condtion.Type > 0)
                    {
                        query = query.Where(c => c.Type==queryCondition.Condtion.Type);
                    }
                    if(queryCondition.Condtion.MemoTypeID > 0)
                    {
                        query = query.Where(c => c.MemoTypeID==queryCondition.Condtion.MemoTypeID);
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.Memo ))
                    {
                        query = query.Where(c => c.Memo.StartsWith(queryCondition.Condtion.Memo));
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.CreateUser ))
                    {
                        query = query.Where(c => c.CreateUser.StartsWith(queryCondition.Condtion.CreateUser));
                    }
                    if(queryCondition.Condtion.CreateDate>DateTime.MinValue)
                    {
                        query = query.Where(c => c.CreateDate > queryCondition.Condtion.CreateDate);
                    }
                    if(queryCondition.Condtion.CreateDateTo!=null)
                    {
                        query = query.Where(c => c.CreateDate <=queryCondition.Condtion.CreateDateTo);
                    }
                    if(queryCondition.Condtion.ResponsiblePersonID > 0)
                    {
                        query = query.Where(c => c.ResponsiblePersonID==queryCondition.Condtion.ResponsiblePersonID);
                    }
                    if(queryCondition.Condtion.Status > 0)
                    {
                        query = query.Where(c => c.Status==queryCondition.Condtion.Status);
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.TypeText ))
                    {
                        query = query.Where(c => c.TypeText.StartsWith(queryCondition.Condtion.TypeText));
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.MemoTypeText ))
                    {
                        query = query.Where(c => c.MemoTypeText.StartsWith(queryCondition.Condtion.MemoTypeText));
                    }
                    if(!string.IsNullOrEmpty( queryCondition.Condtion.ResponsiblePersonText ))
                    {
                        query = query.Where(c => c.ResponsiblePersonText.StartsWith(queryCondition.Condtion.ResponsiblePersonText));
                    }
            return query;
		}
        
        protected override IQueryable<UVConsumerDetails> SetOrder(QueryConditionInfo<UVConsumerDetails> queryCondition, IQueryable<UVConsumerDetails> query)
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
					    if (item.FieldName == "Amount")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Amount) : query.OrderByDescending(c => c.Amount);
					    }
					    if (item.FieldName == "Type")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Type) : query.OrderByDescending(c => c.Type);
					    }
					    if (item.FieldName == "MemoTypeID")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.MemoTypeID) : query.OrderByDescending(c => c.MemoTypeID);
					    }
					    if (item.FieldName == "Memo")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Memo) : query.OrderByDescending(c => c.Memo);
					    }
					    if (item.FieldName == "CreateUser")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateUser) : query.OrderByDescending(c => c.CreateUser);
					    }
					    if (item.FieldName == "CreateDate")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateDate) : query.OrderByDescending(c => c.CreateDate);
					    }
					    if (item.FieldName == "ResponsiblePersonID")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ResponsiblePersonID) : query.OrderByDescending(c => c.ResponsiblePersonID);
					    }
					    if (item.FieldName == "Status")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Status) : query.OrderByDescending(c => c.Status);
					    }
					    if (item.FieldName == "TypeText")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.TypeText) : query.OrderByDescending(c => c.TypeText);
					    }
					    if (item.FieldName == "MemoTypeText")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.MemoTypeText) : query.OrderByDescending(c => c.MemoTypeText);
					    }
					    if (item.FieldName == "ResponsiblePersonText")
					    {
                            query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ResponsiblePersonText) : query.OrderByDescending(c => c.ResponsiblePersonText);
					    }
				}
			}
			else
			{
				query = query.OrderByDescending(c => c.ID);
			}
            return query;
		}
        
        public void ChangeStatus(UVConsumerDetails entity)
		{
			using ( AccountingEntities entities = new  AccountingEntities())
			{
				ObjectSet<UVConsumerDetails> objectSet = entities.CreateObjectSet<UVConsumerDetails>();
				UVConsumerDetails UVConsumerDetails = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				if (UVConsumerDetails != null)
				{
					UVConsumerDetails.Status = entity.Status;
					entities.SaveChanges();
				}
			}
		}
        
    }
}
