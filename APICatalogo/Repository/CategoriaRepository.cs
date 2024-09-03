using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

}
