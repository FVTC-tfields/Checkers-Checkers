using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Checkers.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("042aec28-94e5-4862-9d5f-f2bb27fb290e"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("4171f106-c9b6-4ad0-883e-168c50222e55"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("64590857-c346-45f1-8f8e-7b486a1378ef"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("801c7e5e-1a33-4bc7-afaa-cca8ac1590b0"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("da191dc2-8ad3-41c4-a182-12a1523e7ef2"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("3152a920-04a7-4189-94ef-9c2065c04a36"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("78adffd4-d9b6-497b-ba5d-8ea4d75cb1e5"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("7c1c1ec6-3eae-45ab-90a5-e329e1359798"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("b0c26673-6df2-4271-85d3-f298714ae8df"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("7d2f3a5c-70c3-4677-9ff7-f846520dde0e"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("92dfaa2f-bc16-44ef-97aa-914fb2c57de2"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("da7d1f88-73b5-4ea4-b265-092b3914cb91"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("076a44d7-9578-42cf-bc81-f22664f2a1ff"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("582b65b0-d22b-4826-a1f2-cc0c94288d5f"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("b6934c9a-745a-4bff-b3b0-4435a6295a8a"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("0a20964c-b349-49ee-97dd-8eb8de62ad7d"));

            migrationBuilder.InsertData(
                table: "tblGameState",
                columns: new[] { "Id", "Column", "IsKing", "Row" },
                values: new object[,]
                {
                    { new Guid("1b2016ce-b4da-4f0e-8314-24aeca258e10"), "8", false, "2" },
                    { new Guid("1f744961-4ab6-48e5-962d-e468ab75618f"), "1", true, "7" },
                    { new Guid("585d0c90-fddd-4ba5-9f37-23185ae7644f"), "10", false, "1" },
                    { new Guid("947dead7-573d-4fbb-82fe-aa86b0134215"), "4", true, "1" },
                    { new Guid("edce4d88-e5c4-4bf5-8d5b-32e7acf52dd0"), "4", false, "5" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Nickname", "Password", "UserName", "tblUserGameId" },
                values: new object[,]
                {
                    { new Guid("5f2cb212-0cc9-4643-bf7c-f7fbbb55994c"), "Other", "Other", "Nickname", "X1BEO/529yeajg8vCpiXXNv/OOk=", "sophie", null },
                    { new Guid("62a497d0-9c0f-498b-b729-8d0bfada05f8"), "Brian", "Foote", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "bfoote", null },
                    { new Guid("9a857333-5ad2-4bba-88cb-8a0474c5e2da"), "John", "Doro", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "jdoro", null },
                    { new Guid("e4fa4897-677f-4826-8a32-78aaf2c5de2a"), "Barney", "Smith", "Nickname", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "MetalWhee3l", null }
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "GameDate", "GameStateId", "Name", "Winner", "tblUserGameId" },
                values: new object[,]
                {
                    { new Guid("2ff78b93-aad9-4033-8752-032b5130ebb8"), new DateTime(2024, 4, 26, 9, 4, 15, 897, DateTimeKind.Local).AddTicks(7576), new Guid("947dead7-573d-4fbb-82fe-aa86b0134215"), "Example", "", null },
                    { new Guid("31637e01-7048-4de7-81f7-94d1744f702c"), new DateTime(2024, 4, 26, 9, 4, 15, 897, DateTimeKind.Local).AddTicks(7618), new Guid("edce4d88-e5c4-4bf5-8d5b-32e7acf52dd0"), "George", "", null },
                    { new Guid("eb1201de-4a8d-4935-a370-978e89cdc055"), new DateTime(2024, 4, 26, 9, 4, 15, 897, DateTimeKind.Local).AddTicks(7623), new Guid("1f744961-4ab6-48e5-962d-e468ab75618f"), "World War 42", "MetalWhee3l", null },
                    { new Guid("f59423ae-53ee-43d6-8990-0ee3d42cb4bd"), new DateTime(2024, 4, 26, 9, 4, 15, 897, DateTimeKind.Local).AddTicks(7621), new Guid("1b2016ce-b4da-4f0e-8314-24aeca258e10"), "Hanna", "", null }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "Color", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c99b329-2a4c-4cb2-a721-d4112b237bd8"), "Red", new Guid("2ff78b93-aad9-4033-8752-032b5130ebb8"), new Guid("62a497d0-9c0f-498b-b729-8d0bfada05f8") },
                    { new Guid("bb71b11e-a60d-4057-94c8-cd9cae3115db"), "Red", new Guid("2ff78b93-aad9-4033-8752-032b5130ebb8"), new Guid("e4fa4897-677f-4826-8a32-78aaf2c5de2a") },
                    { new Guid("fbb9e6c9-43a8-4d85-baa4-2376beb19525"), "Black", new Guid("2ff78b93-aad9-4033-8752-032b5130ebb8"), new Guid("9a857333-5ad2-4bba-88cb-8a0474c5e2da") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("31637e01-7048-4de7-81f7-94d1744f702c"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("eb1201de-4a8d-4935-a370-978e89cdc055"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("f59423ae-53ee-43d6-8990-0ee3d42cb4bd"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("585d0c90-fddd-4ba5-9f37-23185ae7644f"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("5f2cb212-0cc9-4643-bf7c-f7fbbb55994c"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("0c99b329-2a4c-4cb2-a721-d4112b237bd8"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("bb71b11e-a60d-4057-94c8-cd9cae3115db"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("fbb9e6c9-43a8-4d85-baa4-2376beb19525"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("2ff78b93-aad9-4033-8752-032b5130ebb8"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("1b2016ce-b4da-4f0e-8314-24aeca258e10"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("1f744961-4ab6-48e5-962d-e468ab75618f"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("edce4d88-e5c4-4bf5-8d5b-32e7acf52dd0"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("62a497d0-9c0f-498b-b729-8d0bfada05f8"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("9a857333-5ad2-4bba-88cb-8a0474c5e2da"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("e4fa4897-677f-4826-8a32-78aaf2c5de2a"));

            migrationBuilder.DeleteData(
                table: "tblGameState",
                keyColumn: "Id",
                keyValue: new Guid("947dead7-573d-4fbb-82fe-aa86b0134215"));

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
        }
    }
}
