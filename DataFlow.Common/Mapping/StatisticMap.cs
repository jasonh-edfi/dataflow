﻿using System.Data.Entity.ModelConfiguration;
using DataFlow.Models;

namespace DataFlow.Common.Mapping
{
    public class StatisticMap : EntityTypeConfiguration<Statistic>
    {
        public StatisticMap()
        {
            this.ToTable("Statistic");
            this.HasKey(x => x.Id);

            this.Property(e => e.EntityType)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            this.Property(e => e.Measure)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}