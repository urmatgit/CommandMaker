﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Catalog;
public class Player : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Phone { get;private set; }
    [Range(14,80)]
    public byte Age { get; private set; }
    [Range(0,5)]
    public byte Level { get; private set; } = 0;
    public DefaultIdType UserId { get;set; }
    public DefaultIdType TeamId { get; private set; }
    public virtual Team Team { get; private set; }
    public Player(string name,string? phone,  byte age, byte level, DefaultIdType teamId)
    {
        Name = name;
        Age = age;
        Level = level;
        TeamId = teamId;
        Phone = phone;

    }

    public Player Update(string? name,string? phone, byte age, byte level, DefaultIdType teamId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (age != Age) Age = age;
        if (level != Level) Level = level;
        if (teamId != TeamId) TeamId = teamId;
        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        return this;
    }
}