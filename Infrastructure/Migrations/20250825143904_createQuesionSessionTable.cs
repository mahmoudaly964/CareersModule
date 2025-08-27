using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createQuesionSessionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSessions_AssessmentSessions_AssessmentSessionId",
                        column: x => x.AssessmentSessionId,
                        principalTable: "AssessmentSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSessions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSessions_AssessmentSessionId",
                table: "QuestionSessions",
                column: "AssessmentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSessions_QuestionId",
                table: "QuestionSessions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionSessions");
        }
    }
}
