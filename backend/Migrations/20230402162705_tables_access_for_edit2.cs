using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class tables_access_for_edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Contracts_ContractId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_ParentInfos_ParentInfoId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Heads_HeadOfKindergartenId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Educators_EducatorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Children_ChildId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ChildId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_HeadOfKindergartenId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ChildId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "HeadOfKindergartenId",
                table: "Contracts");

            migrationBuilder.AlterColumn<long>(
                name: "ChildId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ChildId",
                table: "Payments",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_HeadId",
                table: "Contracts",
                column: "HeadId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Children_ChildId",
                table: "Payments",
                column: "ChildId",
                principalTable: "Children",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Children_ChildId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ChildId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_HeadId",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "Payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ChildId1",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HeadOfKindergartenId",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ChildId1",
                table: "Payments",
                column: "ChildId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_HeadOfKindergartenId",
                table: "Contracts",
                column: "HeadOfKindergartenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Contracts_ContractId",
                table: "Children",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_ParentInfos_ParentInfoId",
                table: "Children",
                column: "ParentInfoId",
                principalTable: "ParentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Heads_HeadOfKindergartenId",
                table: "Contracts",
                column: "HeadOfKindergartenId",
                principalTable: "Heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Educators_EducatorId",
                table: "Groups",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Children_ChildId1",
                table: "Payments",
                column: "ChildId1",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
