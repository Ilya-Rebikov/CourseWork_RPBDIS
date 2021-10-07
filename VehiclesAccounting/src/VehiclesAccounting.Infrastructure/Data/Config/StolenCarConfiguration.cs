﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiclesAccounting.Core.ProjectAggregate;

namespace VehiclesAccounting.Infrastructure.Data.Config
{
    /// <summary>
    /// Configurations for class StolenCar and table StolenCars
    /// </summary>
    public class StolenCarConfiguration : IEntityTypeConfiguration<StolenCar>
    {
        public void Configure(EntityTypeBuilder<StolenCar> builder)
        {
            builder.ToTable("StolenCars");
            builder.Property(sc => sc.Circumstances).IsRequired();
            builder.Property(sc => sc.InsuranceType).IsRequired().HasMaxLength(50);
        }
    }
}
