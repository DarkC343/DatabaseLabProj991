using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLabProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    NattionalCode = table.Column<string>(nullable: true),
                    Age = table.Column<uint>(nullable: false),
                    Rank = table.Column<uint>(nullable: false),
                    AverageMark = table.Column<uint>(nullable: false),
                    IsIranian = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
