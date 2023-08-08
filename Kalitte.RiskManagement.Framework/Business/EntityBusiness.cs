using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Linq.Expressions;
using Kalitte.RiskManagement.Framework.Core;
using System.Data.Objects;
using Ext.Net;
using System.Threading;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Business
{


    public abstract class EntityBusiness<T> : BusinessBase where T : class
    {
        //private PropertyInfo unitProperty;
        //private PropertyInfo userProperty;

        public virtual T RetreiveOrDefault(int id)
        {
            try
            {
                return Retrieve(id);
            }
            catch (ObjectNotFoundException)
            {
                return null;
            }
        }

        protected EntityBusiness()
        {
            //unitProperty = typeof(T).GetProperty("BirimID");
            //userProperty = typeof(T).GetProperty("KullaniciID");

        }

        public virtual T Retrieve(int id)
        {
            object entity = Activator.CreateInstance<T>();
            PropertyInfo p = typeof(T).GetProperty("ID");
            p.SetValue(entity, id, null);
            EntityKey key = DataContext.CreateEntityKey(typeof(T).Name, entity);
            return (T)DataContext.GetObjectByKey(key);
        }

        protected MemberExpression GetPropertyExpression(string propertyName, Expression param)
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null)
                return null;
            object[] customAttributes = property.GetCustomAttributes(typeof(RelationAttribute), true);
            if (customAttributes.Length > 0)
            {
                Expression iterate = param;
                string[] parts = ((RelationAttribute)customAttributes[0]).Identifier.Split('.');
                foreach (var pr in parts)
                {
                    iterate = Expression.PropertyOrField(iterate, pr);
                }
                if (iterate != param)
                    return (MemberExpression)iterate;
                else return Expression.MakeMemberAccess(param, property);
            }
            else return Expression.MakeMemberAccess(param, property);
        }

        public override sealed System.Collections.IList RetreiveItems(ListingParameters parameters = null)
        {
            var baseQuery = GetQueryable(parameters);
            var query = CreateListingQuery(baseQuery, parameters);
            return ExecuteListQuery(query);
        }

        public List<T> RetreiveEntityItems(ListingParameters parameters = null)
        {
            return (List<T>)RetreiveItems(parameters);
        }


        protected virtual List<T> ExecuteListQuery(IQueryable<T> query)
        {
            return query.ToList();
        }

        protected virtual IQueryable<T> FilteredQuery(IQueryable<T> query, ListingParameters e = null)
        {
            Expression whereExp = null;
            ParameterExpression param = Expression.Parameter(typeof(T), "w");

            if (e != null)
            {
                foreach (var item in e.FieldFilters)
                {
                    MemberExpression memExp = GetPropertyExpression(item.Name, param);
                    PropertyInfo property = typeof(T).GetProperty(item.Name);
                    Expression boolExp = item.BuildExpression(memExp, param);
                    if (boolExp != null)
                    {
                        if (whereExp == null)
                            whereExp = boolExp;
                        else whereExp = e.FieldFilters.Operator == FilterListOperator.And ? Expression.AndAlso(whereExp, boolExp) :
                            Expression.Or(whereExp, boolExp);
                    }
                }
            }
            if (whereExp != null)
            {
                var exp = Expression.Lambda<Func<T, bool>>(whereExp, param);
                query = query.Where(exp);
            }
            return query;
        }

        protected HashSet<int> EnabledUnits(ListingParameters listingParams = null)
        {
            HashSet<int> forcedUnits = null;
            if (listingParams != null && listingParams.HasUnits)
            {
                forcedUnits = new UnitBusiness().GetRelatedUnits(listingParams.Units.ToArray());
            }

            HashSet<int> units = forcedUnits == null ? 
                (UserBusiness.UserHasGlobalRights() ? new HashSet<int>() : new HashSet<int>(AuthenticationManager.UnitsUserHasPermission)) :
                forcedUnits;

            return units;
        }

        protected IQueryable<E> PermissionQueryFor<E>(IQueryable<E> query, ListingParameters listingParams = null) where E: class
        {
            var unitProperty = typeof(E).GetProperty("BirimID");
            var userProperty = typeof(E).GetProperty("KullaniciID");
            if (PermissionMode == EntityPermissonMode.Unit && unitProperty != null)
            {
                ParameterExpression param = Expression.Parameter(typeof(E), "w");
                var memExp = Expression.MakeMemberAccess(param, unitProperty);
                LambdaExpression left = Expression.Lambda(memExp, param);
                Expression temp = null, boolExp = null;
                HashSet<int> forcedUnits = EnabledUnits(listingParams);

                foreach (var unitId in forcedUnits)
                {
                    var right = unitProperty.PropertyType == typeof(int) ?
                        Expression.Constant(unitId) : Expression.Constant(new int?(unitId), typeof(int?));
                    temp = Expression.Equal(left.Body, right);
                    if (boolExp == null)
                        boolExp = temp;
                    else boolExp = Expression.Or(boolExp, temp);
                }
                if (boolExp != null)
                {
                    var exp = Expression.Lambda<Func<E, bool>>(boolExp, param);
                    query = query.Where(exp);
                }
            }
            else
                if (PermissionMode == EntityPermissonMode.User && userProperty != null)
                {
                    ParameterExpression param = Expression.Parameter(typeof(E), "u");
                    var memExp = Expression.MakeMemberAccess(param, userProperty);
                    LambdaExpression left = Expression.Lambda(memExp, param);
                    var userId = AuthenticationManager.CurrentUserID;
                    var right = Expression.Constant(userId);
                    var boolExp = Expression.Equal(left.Body, right);
                    var exp = Expression.Lambda<Func<E, bool>>(boolExp, param);
                    query = query.Where(exp);
                }

            return query;
        }

        protected virtual IQueryable<T> PermissionQuery(IQueryable<T> query, ListingParameters listingParams = null)
        {
            return PermissionQueryFor<T>(query, listingParams);
            //if (PermissionMode == EntityPermissonMode.Unit && unitProperty != null)
            //{
            //    ParameterExpression param = Expression.Parameter(typeof(T), "w");
            //    var memExp = Expression.MakeMemberAccess(param, unitProperty);
            //    LambdaExpression left = Expression.Lambda(memExp, param);
            //    Expression temp = null, boolExp = null;
            //    HashSet<int> forcedUnits = EnabledUnits(listingParams);
                
            //    foreach (var unitId in forcedUnits)
            //    {
            //        var right = unitProperty.PropertyType == typeof(int) ?
            //            Expression.Constant(unitId) : Expression.Constant(new int?(unitId), typeof(int?));
            //        temp = Expression.Equal(left.Body, right);
            //        if (boolExp == null)
            //            boolExp = temp;
            //        else boolExp = Expression.Or(boolExp, temp);
            //    }
            //    if (boolExp != null)
            //    {
            //        var exp = Expression.Lambda<Func<T, bool>>(boolExp, param);
            //        query = query.Where(exp);
            //    }
            //}
            //else
            //    if (PermissionMode == EntityPermissonMode.User && userProperty != null)
            //    {
            //        ParameterExpression param = Expression.Parameter(typeof(T), "u");
            //        var memExp = Expression.MakeMemberAccess(param, userProperty);
            //        LambdaExpression left = Expression.Lambda(memExp, param);
            //        var userId = AuthenticationManager.CurrentUserID;
            //        var right = Expression.Constant(userId);
            //        var boolExp = Expression.Equal(left.Body, right);
            //        var exp = Expression.Lambda<Func<T, bool>>(boolExp, param);
            //        query = query.Where(exp);
            //    }

            //return query;
        }

        protected virtual IQueryable<T> CreateListingQuery(IQueryable<T> baseQuery, ListingParameters e = null)
        {
            var query = FilteredQuery(baseQuery, e);
            if (e != null)
                e.Total = query.Count();
            query = SortedQuery(query, e);
            if (e != null && e.Limit > 0)
                query = query.Skip(e.Start).Take(e.Limit).Select(p => p);
            return query;
        }



        protected virtual IQueryable<T> SortedQuery(IQueryable<T> source, ListingParameters p)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "sort");

            string sortField = p == null ? "ID" : p.SortField;
            MemberExpression propertyAccess =  GetPropertyExpression(sortField, param);

            if (propertyAccess == null)
                return source;

            var orderByExpression = Expression.Lambda(propertyAccess, param);

            string ascSortMethodName = "OrderBy";
            string descSortMethodName = "OrderByDescending";
            string sortMethod = ascSortMethodName;
            if (p == null)
            {
                sortMethod = ascSortMethodName;
            }
            else
            {
                if (p.Dir == SortDirection.Default)
                    p.Dir = SortDirection.ASC;
                if (p.Dir == SortDirection.DESC)
                    sortMethod = descSortMethodName;
            }

            MethodCallExpression resultExp = Expression.Call(
                                typeof(Queryable),
                                sortMethod,
                                new Type[] { typeof(T), propertyAccess.Type },
                                source.Expression,
                                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        protected internal virtual IQueryable<T> GetQueryable(ListingParameters listingParams = null)
        {
            ObjectSet<T> set = DataContext.CreateObjectSet<T>();
            return PermissionQuery(set.AsQueryable<T>(), listingParams);
        }

        public virtual void Update(T entity)
        {
            UpdatingEntity(entity);
        }

        public virtual void UpdateSingle(T entity)
        {
            Update(entity);
            DataContext.SaveChanges();
        }

        public virtual void DeletetingEntity(T entity)
        {

        }

        public virtual void Delete(T entity)
        {
            DeletetingEntity(entity);
            DataContext.DeleteObject(entity);
        }

        public virtual void DeleteSingle(T entity)
        {
            Delete(entity);
            DataContext.SaveChanges();
        }

        public virtual void DeleteCollection(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Delete(item);
            }
            DataContext.SaveChanges();
        }

        protected virtual void UpdatingEntity(T entity)
        {
            SetEntityProperty(entity, "GuncelleyenKullaniciID", AuthenticationManager.CurrentUserID);
            SetEntityProperty(entity, "GuncellemeTarih", DateTime.Now);
        }

        protected virtual void InsertingEntity(T entity)
        {
            SetEntityProperty(entity, "KullaniciID", AuthenticationManager.CurrentUserID);
            SetEntityProperty(entity, "KayitTarih", DateTime.Now);
            SetEntityProperty(entity, "BirimID", AuthenticationManager.CurrentUserBirimID);
        }

        private bool SetEntityProperty(T entity, string property, object value)
        {
            var propInfo = typeof(T).GetProperty(property);
            if (propInfo != null)
            {
                propInfo.SetValue(entity, value, null);
                return true;
            }
            else return false;
        }


        public virtual void InsertSingle(T entity)
        {
            Insert(entity);
            DataContext.SaveChanges();
        }

        public virtual void Insert(T entity)
        {
            DataContext.AddObject(typeof(T).Name, entity);
            InsertingEntity(entity);
        }

        public override Type EntityType
        {
            get { return typeof(T); }
        }

        //public bool SurecIliskiControlonDelete(int surecid)
        //{ 
        //    //return GetQueryable().Where(p => p ılgaz
        //}
    }



}
