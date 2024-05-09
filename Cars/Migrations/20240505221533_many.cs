using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars.Migrations
{
    /// <inheritdoc />
    public partial class many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Company_CompanyId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CompanyId",
                table: "products");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCompany",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCompany", x => new { x.CompaniesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductCompany_Company_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCompany_products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCompany_ProductsId",
                table: "ProductCompany",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCompany");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_products_CompanyId",
                table: "products",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Company_CompanyId",
                table: "products",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
