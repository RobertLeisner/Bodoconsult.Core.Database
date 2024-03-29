﻿// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using System;
using System.Collections.Generic;
using System.Text;
using Bodoconsult.Core.Database.Interfaces;

namespace Bodoconsult.Core.Database.EntityBackup
{

    /// <summary>
    /// Base class for <see cref="IEntityBackupDataService&lt;T&gt;"/> implementations
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class BaseEntityBackupDataService<T> : IEntityBackupDataService<T> where T : class
    {
        /// <summary>
        /// Get the data for an entity by date
        /// </summary>
        /// <param name="from">Date from inclusive</param>
        /// <param name="to">Date until exclusive</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Current page index</param>
        /// <returns>List with entities</returns>
        public virtual IList<T> GetData(DateTime from, DateTime to, int pageSize, int pageIndex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Format the entity as a line with semicolon separated fields
        /// </summary>
        /// <param name="entity">Entity to serialize</param>
        /// <param name="stringBuilder">StringBuilder to append the data</param>
        public virtual void FormatAsString(T entity, StringBuilder stringBuilder)
        {
            throw new NotSupportedException();
        }


        /// <summary>
        /// Remove the entities backuped already
        /// </summary>
        /// <param name="entities">Entities to remove</param>
        public virtual void RemoveData(IList<T> entities)
        {
            throw new NotSupportedException();
        }
    }
}