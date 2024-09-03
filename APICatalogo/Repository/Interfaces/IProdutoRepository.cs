using APICatalogo.Models;

namespace APICatalogo.Repository.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> AllByCategoria(int id);
}
