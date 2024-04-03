using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Checkers.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGameState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Column = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsKing = table.Column<bool>(type: "bit", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGameState_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameStateId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Winner = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GameDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    tblUserGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGame_tblGameState_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "tblGameState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    tblUserGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUserGame_tblGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserGame_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblGameState",
                columns: new[] { "Id", "Column", "IsKing", "Row" },
                values: new object[,]
                {
                    { new Guid("0a20964c-b349-49ee-97dd-8eb8de62ad7d"), "4", true, "1" },
                    { new Guid("7d2f3a5c-70c3-4677-9ff7-f846520dde0e"), "8", false, "2" },
                    { new Guid("801c7e5e-1a33-4bc7-afaa-cca8ac1590b0"), "10", false, "1" },
                    { new Guid("92dfaa2f-bc16-44ef-97aa-914fb2c57de2"), "4", false, "5" },
                    { new Guid("da7d1f88-73b5-4ea4-b265-092b3914cb91"), "1", true, "7" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Nickname", "Password", "UserName", "tblUserGameId" },
                values: new object[,]
                {
                    { new Guid("076a44d7-9578-42cf-bc81-f22664f2a1ff"), "Brian", "Foote", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "bfoote", null },
                    { new Guid("582b65b0-d22b-4826-a1f2-cc0c94288d5f"), "Barney", "Smith", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "MetalWhee3l", null },
                    { new Guid("b6934c9a-745a-4bff-b3b0-4435a6295a8a"), "John", "Doro", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "jdoro", null },
                    { new Guid("da191dc2-8ad3-41c4-a182-12a1523e7ef2"), "Other", "Other", "Nickname", "X1BEO/529yeajg8vCpiXXNv/OOk=", "sophie", null }
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "GameDate", "GameStateId", "Name", "Winner", "tblUserGameId" },
                values: new object[,]
                {
                    { new Guid("042aec28-94e5-4862-9d5f-f2bb27fb290e"), new DateTime(2024, 4, 3, 13, 32, 7, 454, DateTimeKind.Local).AddTicks(8150), new Guid("7d2f3a5c-70c3-4677-9ff7-f846520dde0e"), "Hanna", "", null },
                    { new Guid("4171f106-c9b6-4ad0-883e-168c50222e55"), new DateTime(2024, 4, 3, 13, 32, 7, 454, DateTimeKind.Local).AddTicks(8152), new Guid("da7d1f88-73b5-4ea4-b265-092b3914cb91"), "World War 42", "MetalWhee3l", null },
                    { new Guid("64590857-c346-45f1-8f8e-7b486a1378ef"), new DateTime(2024, 4, 3, 13, 32, 7, 454, DateTimeKind.Local).AddTicks(8148), new Guid("92dfaa2f-bc16-44ef-97aa-914fb2c57de2"), "George", "", null },
                    { new Guid("b0c26673-6df2-4271-85d3-f298714ae8df"), new DateTime(2024, 4, 3, 13, 32, 7, 454, DateTimeKind.Local).AddTicks(8108), new Guid("0a20964c-b349-49ee-97dd-8eb8de62ad7d"), "Example", "", null }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "Color", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3152a920-04a7-4189-94ef-9c2065c04a36"), "Red", new Guid("b0c26673-6df2-4271-85d3-f298714ae8df"), new Guid("582b65b0-d22b-4826-a1f2-cc0c94288d5f") },
                    { new Guid("78adffd4-d9b6-497b-ba5d-8ea4d75cb1e5"), "Red", new Guid("b0c26673-6df2-4271-85d3-f298714ae8df"), new Guid("076a44d7-9578-42cf-bc81-f22664f2a1ff") },
                    { new Guid("7c1c1ec6-3eae-45ab-90a5-e329e1359798"), "Black", new Guid("b0c26673-6df2-4271-85d3-f298714ae8df"), new Guid("b6934c9a-745a-4bff-b3b0-4435a6295a8a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblGame_GameStateId",
                table: "tblGame",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGame_tblUserGameId",
                table: "tblGame",
                column: "tblUserGameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_tblUserGameId",
                table: "tblUser",
                column: "tblUserGameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_GameId",
                table: "tblUserGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_UserId",
                table: "tblUserGame",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblGame_tblUserGame_tblUserGameId",
                table: "tblGame",
                column: "tblUserGameId",
                principalTable: "tblUserGame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_tblUserGame_tblUserGameId",
                table: "tblUser",
                column: "tblUserGameId",
                principalTable: "tblUserGame",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGame_tblGameState_GameStateId",
                table: "tblGame");

            migrationBuilder.DropForeignKey(
                name: "FK_tblGame_tblUserGame_tblUserGameId",
                table: "tblGame");

            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_tblUserGame_tblUserGameId",
                table: "tblUser");

            migrationBuilder.DropTable(
                name: "tblGameState");

            migrationBuilder.DropTable(
                name: "tblUserGame");

            migrationBuilder.DropTable(
                name: "tblGame");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
