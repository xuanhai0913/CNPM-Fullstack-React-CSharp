using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationshipConflicts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Projects_ProjectId1",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Projects_ProjectId1",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Projects_ProjectId1",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Projects_ProjectId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ProjectId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_ProjectId1",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProjectId1",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Milestones_ProjectId1",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Milestones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Transactions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "TaskItems",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Proposals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Milestones",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProjectId1",
                table: "Transactions",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ProjectId1",
                table: "TaskItems",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProjectId1",
                table: "Proposals",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectId1",
                table: "Milestones",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Projects_ProjectId1",
                table: "Milestones",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Projects_ProjectId1",
                table: "Proposals",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Projects_ProjectId1",
                table: "TaskItems",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Projects_ProjectId1",
                table: "Transactions",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
