using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class CRUD_operations_Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Contract",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Group",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_ParentInfo",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_HeadId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_EducatorId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Contract",
                table: "Children",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Group",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_ParentInfo",
                table: "Children",
                column: "ParentInfoId",
                principalTable: "ParentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_HeadId",
                table: "Contracts",
                column: "HeadId",
                principalTable: "Heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_EducatorId",
                table: "Groups",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Contract",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Group",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_ParentInfo",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_HeadId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_EducatorId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Contract",
                table: "Children",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Group",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_ParentInfo",
                table: "Children",
                column: "ParentInfoId",
                principalTable: "ParentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_HeadId",
                table: "Contracts",
                column: "HeadId",
                principalTable: "Heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_EducatorId",
                table: "Groups",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
