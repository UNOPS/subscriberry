using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subscriberry.EntityFramework.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SubscriptionRole",
				schema: "dbo");

			migrationBuilder.DropTable(
				name: "UserSubscription",
				schema: "dbo");

			migrationBuilder.DropTable(
				name: "Subscription",
				schema: "dbo");

			migrationBuilder.DropTable(
				name: "Group",
				schema: "dbo");
		}

		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "dbo");

			migrationBuilder.CreateTable(
				name: "Group",
				schema: "dbo",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_Group", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "Subscription",
				schema: "dbo",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					GroupId = table.Column<int>(nullable: true),
					Name = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Subscription", x => x.Id);
					table.ForeignKey(
						name: "FK_Subscription_Group_GroupId",
						column: x => x.GroupId,
						principalSchema: "dbo",
						principalTable: "Group",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "SubscriptionRole",
				schema: "dbo",
				columns: table => new
				{
					RoleId = table.Column<int>(nullable: false),
					SubscriptionId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SubscriptionRole", x => new {x.RoleId, x.SubscriptionId});
					table.ForeignKey(
						name: "FK_SubscriptionRole_Subscription_SubscriptionId",
						column: x => x.SubscriptionId,
						principalSchema: "dbo",
						principalTable: "Subscription",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserSubscription",
				schema: "dbo",
				columns: table => new
				{
					UserId = table.Column<string>(nullable: false),
					SubscriptionId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserSubscription", x => new {x.UserId, x.SubscriptionId});
					table.ForeignKey(
						name: "FK_UserSubscription_Subscription_SubscriptionId",
						column: x => x.SubscriptionId,
						principalSchema: "dbo",
						principalTable: "Subscription",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Subscription_GroupId",
				schema: "dbo",
				table: "Subscription",
				column: "GroupId");

			migrationBuilder.CreateIndex(
				name: "IX_SubscriptionRole_SubscriptionId",
				schema: "dbo",
				table: "SubscriptionRole",
				column: "SubscriptionId");

			migrationBuilder.CreateIndex(
				name: "IX_UserSubscription_SubscriptionId",
				schema: "dbo",
				table: "UserSubscription",
				column: "SubscriptionId");
		}
	}
}