using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedFox.TemperatureApp.Migrations
{
    public partial class AddLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TemperatureHumidityData",
                nullable: true);

			migrationBuilder.Sql("update TemperatureHumidityData set Location = 'nursery'");

			migrationBuilder.AlterColumn<string>(
				name: "Location", 
				table: "TemperatureHumidityData", 
				nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "TemperatureHumidityData");
        }
    }
}
