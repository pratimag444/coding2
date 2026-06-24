using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityEventManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddMaximumParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumParticipants",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumParticipants",
                table: "Events");
        }
    }
}
