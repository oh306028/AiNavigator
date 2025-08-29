using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiNavigator.Api.Migrations
{
    public partial class conclusionTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestSummaryId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestSummaryId",
                table: "Requests",
                column: "RequestSummaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Summaries_RequestSummaryId",
                table: "Requests",
                column: "RequestSummaryId",
                principalTable: "Summaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Summaries_RequestSummaryId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestSummaryId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestSummaryId",
                table: "Requests");
        }
    }
}
