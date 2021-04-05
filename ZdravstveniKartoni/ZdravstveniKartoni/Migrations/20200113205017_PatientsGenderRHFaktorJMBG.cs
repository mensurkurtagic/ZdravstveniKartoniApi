using Microsoft.EntityFrameworkCore.Migrations;

namespace ZdravstveniKartoni.Migrations
{
    public partial class PatientsGenderRHFaktorJMBG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JMBG",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RHFaktor",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "JMBG",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RHFaktor",
                table: "Patients");
        }
    }
}
