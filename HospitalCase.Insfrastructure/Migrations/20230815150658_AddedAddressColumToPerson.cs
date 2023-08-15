using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalCase.Insfrastructure.Migrations
{
    public partial class AddedAddressColumToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "People");
        }
    }
}
