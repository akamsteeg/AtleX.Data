﻿using AtleX.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity.Mocks
{
    public class TestEntity : IHasCreated, IHasLastModified
    {
        [Key]
        public string Name { get; set; }

        public DateTimeOffset? Created
        {
            get;
            set;
        }

        public DateTimeOffset? LastModified
        {
            get;
            set;
        }
    }
}
