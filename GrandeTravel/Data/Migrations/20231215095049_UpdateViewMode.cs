using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrandeTravel.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateViewMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "tb_CustomerProfile",
                columns: table => new
                {
                    CustomerProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerProfile", x => x.CustomerProfileId);
                });

            migrationBuilder.CreateTable(
                name: "tb_MyUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_MyUser_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_TravelProviderProfile",
                columns: table => new
                {
                    TravelProviderProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_TravelProviderProfile", x => x.TravelProviderProfileId);
                });

            migrationBuilder.CreateTable(
                name: "tb_TravelPackage",
                columns: table => new
                {
                    TravelPackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagePrice = table.Column<int>(type: "int", nullable: false),
                    Discontinued = table.Column<bool>(type: "bit", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_TravelPackage", x => x.TravelPackageId);
                    table.ForeignKey(
                        name: "FK_tb_TravelPackage_tb_MyUser_MyUserId",
                        column: x => x.MyUserId,
                        principalTable: "tb_MyUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_Booking",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelPackageId = table.Column<int>(type: "int", nullable: false),
                    MyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    People = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<int>(type: "int", nullable: false),
                    TravelPackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftFeedback = table.Column<bool>(type: "bit", nullable: false),
                    PaymentReceived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_tb_Booking_tb_MyUser_MyUserId",
                        column: x => x.MyUserId,
                        principalTable: "tb_MyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_Booking_tb_TravelPackage_TravelPackageId",
                        column: x => x.TravelPackageId,
                        principalTable: "tb_TravelPackage",
                        principalColumn: "TravelPackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    TravelPackageId = table.Column<int>(type: "int", nullable: false),
                    MyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_tb_Feedback_tb_MyUser_MyUserId",
                        column: x => x.MyUserId,
                        principalTable: "tb_MyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_Feedback_tb_TravelPackage_TravelPackageId",
                        column: x => x.TravelPackageId,
                        principalTable: "tb_TravelPackage",
                        principalColumn: "TravelPackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Photo",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Photo", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_tb_Photo_tb_TravelPackage_TravelPackageId",
                        column: x => x.TravelPackageId,
                        principalTable: "tb_TravelPackage",
                        principalColumn: "TravelPackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Booking_MyUserId",
                table: "tb_Booking",
                column: "MyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Booking_TravelPackageId",
                table: "tb_Booking",
                column: "TravelPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Feedback_MyUserId",
                table: "tb_Feedback",
                column: "MyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Feedback_TravelPackageId",
                table: "tb_Feedback",
                column: "TravelPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Photo_TravelPackageId",
                table: "tb_Photo",
                column: "TravelPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_TravelPackage_MyUserId",
                table: "tb_TravelPackage",
                column: "MyUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Booking");

            migrationBuilder.DropTable(
                name: "tb_CustomerProfile");

            migrationBuilder.DropTable(
                name: "tb_Feedback");

            migrationBuilder.DropTable(
                name: "tb_Photo");

            migrationBuilder.DropTable(
                name: "tb_TravelProviderProfile");

            migrationBuilder.DropTable(
                name: "tb_TravelPackage");

            migrationBuilder.DropTable(
                name: "tb_MyUser");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
