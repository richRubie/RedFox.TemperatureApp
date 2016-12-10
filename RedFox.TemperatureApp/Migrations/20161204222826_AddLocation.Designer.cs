using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RedFox.TemperatureApp.Business;

namespace RedFox.TemperatureApp.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20161204222826_AddLocation")]
    partial class AddLocation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RedFox.TemperatureApp.Business.TemperatureHumidityData", b =>
                {
                    b.Property<int>("TemperatureHumidityDataId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Humidity");

                    b.Property<string>("Location");

                    b.Property<DateTime>("LogDateTime");

                    b.Property<decimal>("Temperature");

                    b.HasKey("TemperatureHumidityDataId");

                    b.ToTable("TemperatureHumidityData");
                });
        }
    }
}
