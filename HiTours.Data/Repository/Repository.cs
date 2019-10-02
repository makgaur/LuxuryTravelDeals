// <copyright file="Repository.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository Type Of Entity
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="HiTours.Data.Repository.ToPaggedList" />
    /// <seealso cref="HiTours.Core.IRepository{TEntity}" />
    public partial class Repository<TEntity> : ToPaggedList, IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly DataBaseContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private DbSet<TEntity> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DataBaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (this.entities == null)
                {
                    this.entities = this.context.Set<TEntity>();
                }

                return this.entities;
            }
        }

#pragma warning disable SA1202 // Elements must be ordered by access

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public virtual IQueryable<TEntity> Table
#pragma warning restore SA1202 // Elements must be ordered by access
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets the table no tracking.
        /// </summary>
        /// <value>
        /// The table no tracking.
        /// </value>
        public virtual IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Adds to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddToContext(TEntity entity)
        {
            this.Entities.Add(entity);
        }

        /// <summary>
        /// Adds to context.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddToContext(IEnumerable<TEntity> entities)
        {
            this.Entities.AddRange(entities);
        }

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void RemoveToContext(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Insert Entity
        /// </returns>
        public int Insert(TEntity entity)
        {
            this.Entities.Add(entity);
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Delete Entity
        /// </returns>
        public int Delete(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Deleted;
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Update Entity
        /// </returns>
        public int Update(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            return this.context.SaveChanges();
        }

        /////// <summary>
        /////// Updates the specified entity.
        /////// </summary>
        /////// <param name="entity">The entity.</param>
        ////public void UpdateCompleteGraph(TEntity entity, object id)
        ////{
        ////    var tbl = this.context.TourPackage;
        ////    this.context.Entry(entity).CurrentValues.SetValues = entity;
        ////    ////return this.context.SaveChanges();
        ////}

        /// <summary>
        /// Updates the complete graph.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="key">The key.</param>
        /// <returns>type of entity</returns>
        public virtual TEntity UpdateCompleteGraph(TEntity t, object key)
        {
            ////TEntity exist = this.entities.Find(key);
            //////this.context.Entry(exist).CurrentValues.SetValues(t);
            ////this.context.Entry(exist).State = EntityState.Modified;
            ////return exist;

            if (t == null)
            {
                return null;
            }

            TEntity exist = this.entities.Find(key);
            if (exist != null)
            {
                ////this.context.Entry(exist).OriginalValues["RowVersion"] = rowVersion;
                ////this.context.Entry(exist).CurrentValues.SetValues(t);
                this.context.Entry(t);
                ////this.context.SaveChanges();
            }

            return exist;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Async Insert Entity
        /// </returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            await this.Entities.AddAsync(entity);
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Async Update Entity
        /// </returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Async Delete Entity
        /// </returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Deleted;
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>
        /// Save Changes
        /// </returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// To the paged list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Get Pagging Entites List By Querable Data with search parameters
        /// </returns>
        public async Task<DataTableResult> ToPagedListAsync(IQueryable<TEntity> query, DataTableParameter model)
        {
            return await this.ToPaggedListAsync(query, model);
        }

        /// <summary>
        /// To the option list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// Get pagging Data for Dropdown Select Item List
        /// </returns>
        public async Task<IList<Dropdown>> ToOptionListAsync(IQueryable<Dropdown> query, int page)
        {
            page = page == 0 ? 1 : page;
            if (page > 0)
            {
                query = query.Skip((page - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// To the option list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// Get pagging Data for Dropdown Select Item List
        /// </returns>
        public async Task<IList<Dropdown>> ToListAsync(IQueryable<Dropdown> query)
        {
            return await query.ToListAsync();
        }

        /// <summary>
        /// To the option list.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <returns>list</returns>
        public IList<Dropdown> ToOptionList(IQueryable<Dropdown> query, int page)
        {
            page = page == 0 ? 1 : page;
            if (page > 0)
            {
                query = query.Skip((page - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            return query.ToList();
        }

        /// <summary>
        /// Errors the free object.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="t">The t.</param>
        private void ErrorFreeObject<TClass>(TClass t)
            where TClass : class
        {
            try
            {
                foreach (PropertyDescriptor propertyInfo in TypeDescriptor.GetProperties(typeof(TClass)))
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (propertyInfo.GetValue(t) == null)
                        {
                            propertyInfo.SetValue(t, string.Empty);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        if (propertyInfo.GetValue(t) == null)
                        {
                            propertyInfo.SetValue(t, 0);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        if (propertyInfo.GetValue(t) == null || (DateTime)propertyInfo.GetValue(t) == DateTime.MinValue)
                        {
                            propertyInfo.SetValue(t, DateTime.Now);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}