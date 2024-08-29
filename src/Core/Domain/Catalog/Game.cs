using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Catalog;
public class Game :AuditableEntity, IAggregateRoot, ICheckOwner
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime DateTime { get; private set; }
    public bool IsArchive { get; private set; }

    public Game(string name,string? description, DateTime dateTime,bool isArchive =false )
    {
        Name = name;
        Description = description;
        DateTime = dateTime;
        IsArchive = isArchive;

    }

    public Game Update(string? name,DateTime dateTime, string? description,bool isArchive=false)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (dateTime!=DateTime) DateTime = dateTime;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isArchive!=IsArchive) IsArchive = isArchive;
        return this;
    }
}
