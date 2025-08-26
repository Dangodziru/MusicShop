using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicShop.DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSexToArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Artist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sex",
                table: "Artist",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
