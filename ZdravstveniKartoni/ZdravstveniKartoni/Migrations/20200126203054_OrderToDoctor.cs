using Microsoft.EntityFrameworkCore.Migrations;

namespace ZdravstveniKartoni.Migrations
{
    public partial class OrderToDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "DiseaseRecords",
                newName: "OrderToDoctor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderToDoctor",
                table: "DiseaseRecords",
                newName: "Order");
        }
    }
}
