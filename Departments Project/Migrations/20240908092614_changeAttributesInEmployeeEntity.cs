using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Departments_Project.Migrations
{
    /// <inheritdoc />
    public partial class changeAttributesInEmployeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "FirstName");
        }
    }
}
