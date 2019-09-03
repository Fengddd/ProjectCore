using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conference.EntityFrameworkCore.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConferenceInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    ConferenceName = table.Column<string>(nullable: true),
                    ConferenceContent = table.Column<string>(nullable: true),
                    ConferenceAddress = table.Column<string>(nullable: true),
                    ConferenceParticipantNum = table.Column<int>(nullable: false),
                    ConferencePublishStatus = table.Column<bool>(nullable: false),
                    ConferenceStartTime = table.Column<DateTime>(nullable: false),
                    ConferenceEndTime = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAge = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    CustomerAddress_Province = table.Column<string>(nullable: true),
                    CustomerAddress_City = table.Column<string>(nullable: true),
                    CustomerAddress_County = table.Column<string>(nullable: true),
                    CustomerAddress_AddressDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStorageInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateRootId = table.Column<Guid>(nullable: false),
                    AggregateRootType = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    EventData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStorageInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitorLogInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MonitorLogId = table.Column<Guid>(nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    RequestParameters = table.Column<string>(nullable: true),
                    ExecuteStartTime = table.Column<DateTime>(nullable: false),
                    ExecuteEndTime = table.Column<DateTime>(nullable: false),
                    ExecutionTime = table.Column<long>(nullable: false),
                    ErrorMsg = table.Column<string>(nullable: true),
                    LogType = table.Column<int>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    AddressIp = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorLogInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatTypeInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeatTypeName = table.Column<string>(nullable: true),
                    SeatTypePrice = table.Column<decimal>(nullable: false),
                    SeatTypeNum = table.Column<int>(nullable: false),
                    ConferenceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTypeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatTypeInfo_ConferenceInfo_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "ConferenceInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    SeatTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatInfo_SeatTypeInfo_SeatTypeId",
                        column: x => x.SeatTypeId,
                        principalTable: "SeatTypeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventStorageInfo_AggregateRootId_AggregateRootType",
                table: "EventStorageInfo",
                columns: new[] { "AggregateRootId", "AggregateRootType" });

            migrationBuilder.CreateIndex(
                name: "IX_MonitorLogInfo_ControllerName_ActionName_ExecuteStartTime_ExecuteEndTime",
                table: "MonitorLogInfo",
                columns: new[] { "ControllerName", "ActionName", "ExecuteStartTime", "ExecuteEndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_SeatInfo_SeatTypeId",
                table: "SeatInfo",
                column: "SeatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatTypeInfo_ConferenceId",
                table: "SeatTypeInfo",
                column: "ConferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "EventStorageInfo");

            migrationBuilder.DropTable(
                name: "MonitorLogInfo");

            migrationBuilder.DropTable(
                name: "SeatInfo");

            migrationBuilder.DropTable(
                name: "SeatTypeInfo");

            migrationBuilder.DropTable(
                name: "ConferenceInfo");
        }
    }
}
