﻿using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using IMDBDegrees.DAL.Titles.Models;

namespace IMDBDegrees.DAL.Titles
{
    public class TitleContext : DbContext
    {
        public TitleContext(DbContextOptions<TitleContext> options): base(options){}

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Models.Type> Types { get; set; }
    }
}
