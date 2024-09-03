using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class populaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) 
                VALUES
                ('Coca Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocola.jpg', 50, NOW(), 1),
                ('Coca Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocola.jpg', 50, NOW(), 2),
                ('Coca Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocola.jpg', 50, NOW(), 3);
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
