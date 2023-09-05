using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    /// <inheritdoc />
    public partial class PopularLanches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tortas(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) VALUES(2, 'Biscoito de maisena, leite condensado, creme de leite, suco e raspa de limão, açucar, claras de ovo.', 'Deliciosa torta de limão preparada com biscoito de maizena, raspas de limão e leite condesado de qualidade.', 1, 'https://folhadomate.com/wp-content/uploads/2022/11/Torta-de-Lima%CC%83o.jpg', 'https://folhadomate.com/wp-content/uploads/2022/11/Torta-de-Lima%CC%83o.jpg', 0, 'Torta de limão', 49.90)");
            migrationBuilder.Sql("INSERT INTO Tortas(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) VALUES(2, 'Leite condensado, leite, ovos, açucar.', 'Um pudim super cremoso, com ingredientes de alta qualidade.', 1, 'https://receitinhas.com.br/wp-content/uploads/2022/09/maxresdefault-61-730x365.jpg', 'https://receitinhas.com.br/wp-content/uploads/2022/09/maxresdefault-61-730x365.jpg', 0, 'Pudim', 39.90)");
            migrationBuilder.Sql("INSERT INTO Tortas(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) VALUES(2, 'Ovos, achocolatado, farinha de trigo e açucar.', 'Delicioso brownie preparado com achocolatado de qualidade, ovos coloniais pensado na qualidade do produto.', 1, 'https://assets.unileversolutions.com/recipes-v2/236951.jpg', 'https://assets.unileversolutions.com/recipes-v2/236951.jpg', 0, 'Brownie em fatia', 8.90)");
            migrationBuilder.Sql("INSERT INTO Tortas(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) VALUES(3, 'Biscoito maria, leite condensado, creme de leite, nata, leite, chocolate e chocolate em pó.', 'Torta de bolacha preparada com chocolate de alta qualidade, leite condensado, nata, creme de leite e bolacha maria de chocolate', 0, 'https://i.ytimg.com/vi/66Ufd8TPPfU/maxresdefault.jpg', 'https://i.ytimg.com/vi/66Ufd8TPPfU/maxresdefault.jpg', 0, 'Torta de bolacha', 59.90)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Tortas");
        }
    }
}
