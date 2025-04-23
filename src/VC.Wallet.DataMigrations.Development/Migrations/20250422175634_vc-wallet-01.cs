using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Wallet.DataMigrations.Development.Migrations
{
    /// <inheritdoc />
    public partial class vcwallet01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vc_wallet_holder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    countryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    region = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vc_wallet_holder", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vc_wallet_holder_credential",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    credentialId = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    holderUsername = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    credentialName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    sortingCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    groupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vc_wallet_holder_credential", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vc_wallet_holder_credential_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    holderUsername = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    groupName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    sortingCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vc_wallet_holder_credential_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vc_wallet_holder_did",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    holderUsername = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    did = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sortingCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vc_wallet_holder_did", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vc_wallet_trusted_issuer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    did = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adminUsername = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    countryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    region = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    privateKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    websiteUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vc_wallet_trusted_issuer", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vc_wallet_holder");

            migrationBuilder.DropTable(
                name: "vc_wallet_holder_credential");

            migrationBuilder.DropTable(
                name: "vc_wallet_holder_credential_group");

            migrationBuilder.DropTable(
                name: "vc_wallet_holder_did");

            migrationBuilder.DropTable(
                name: "vc_wallet_trusted_issuer");
        }
    }
}
