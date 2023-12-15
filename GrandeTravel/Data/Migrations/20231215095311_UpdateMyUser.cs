using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrandeTravel.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Booking_tb_MyUser_MyUserId",
                table: "tb_Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_Feedback_tb_MyUser_MyUserId",
                table: "tb_Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_TravelPackage_tb_MyUser_MyUserId",
                table: "tb_TravelPackage");

            migrationBuilder.DropTable(
                name: "tb_MyUser");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Booking_AspNetUsers_MyUserId",
                table: "tb_Booking",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Feedback_AspNetUsers_MyUserId",
                table: "tb_Feedback",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_TravelPackage_AspNetUsers_MyUserId",
                table: "tb_TravelPackage",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Booking_AspNetUsers_MyUserId",
                table: "tb_Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_Feedback_AspNetUsers_MyUserId",
                table: "tb_Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_TravelPackage_AspNetUsers_MyUserId",
                table: "tb_TravelPackage");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Booking_tb_MyUser_MyUserId",
                table: "tb_Booking",
                column: "MyUserId",
                principalTable: "tb_MyUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Feedback_tb_MyUser_MyUserId",
                table: "tb_Feedback",
                column: "MyUserId",
                principalTable: "tb_MyUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_TravelPackage_tb_MyUser_MyUserId",
                table: "tb_TravelPackage",
                column: "MyUserId",
                principalTable: "tb_MyUser",
                principalColumn: "Id");
        }
    }
}
