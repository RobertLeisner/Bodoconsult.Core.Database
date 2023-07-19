// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bodoconsult.Core.Database.EntityBackup;
using Bodoconsult.Core.Database.Interfaces;

namespace Bodoconsult.Core.Database.Test.EntityBackup;

/// <summary>
/// Example for a implementation of <see cref="IEntityBackupDataService&lt;T&gt;"/>  based on <see cref="DemoEntity"/>
/// </summary>
public class DemoEntityEntityBackupDataService : BaseEntityBackupDataService<DemoEntity>
{

    /// <summary>
    /// Demo entities for testing
    /// </summary>
    public List<DemoEntity> DemoEntities { get; } = new();

    /// <summary>
    /// Get the data for an entity by date
    /// </summary>
    /// <param name="from">Date from inclusive</param>
    /// <param name="to">Date until exclusive</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="pageIndex">Current page index</param>
    /// <returns>List with entities</returns>
    public override IList<DemoEntity> GetData(DateTime from, DateTime to, int pageSize, int pageIndex)
    {
        return DemoEntities.Where(x => x.Date >= from && x.Date < to).ToList();
    }

    /// <summary>
    /// Format the entity as a line with semicolon separated fields
    /// </summary>
    /// <param name="entity">Entity to serialize</param>
    /// <param name="stringBuilder">StringBuilder to append the data</param>
    public override void FormatAsString(DemoEntity entity, StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine($"{entity.Id};{entity.Name};{entity.Date}");
    }

    /// <summary>
    /// Remove the entities backuped already
    /// </summary>
    /// <param name="entities">Entities to remove</param>
    public override void RemoveData(IList<DemoEntity> entities)
    {

        foreach (var entity in entities)
        {
            DemoEntities.Remove(entity);
        }

    }

}