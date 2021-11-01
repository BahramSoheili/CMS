﻿using Core.Queries;
using System;
namespace QueryManagement.Queries
{
    public class SearchUserById: IQuery<User>
    {
        public Guid Id { get; }

        public SearchUserById(Guid id)
        {
            Id = id;
        }
    }
}

