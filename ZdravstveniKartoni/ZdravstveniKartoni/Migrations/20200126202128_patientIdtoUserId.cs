using Microsoft.EntityFrameworkCore.Migrations;

namespace ZdravstveniKartoni.Migrations
{
    public partial class patientIdtoUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "DiseaseRecords",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DiseaseRecords",
                newName: "PatientId");
        }
    }
}
