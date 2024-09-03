using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    // A FINS DE EXEMPLO NÃO REFATOREI PARA USAR PRODUTOREPOSITORY
    private readonly AppDbContext _context;
    private readonly IMeuServico _meuServico;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(AppDbContext context, IMeuServico meuServico)
    {
        _context = context;
        _meuServico = meuServico;
    }

    [HttpGet("welcome/{nome}")]
    public ActionResult<string> GetSaudacao(string nome)
    {
        return _meuServico.Saudacao(nome);
    }

    [HttpGet("categorias")]
    public ActionResult<IEnumerable<Produto>> GetProdutosECategorias()
    {
        // Sempre buscar aplicar um filtro seja com o Where ou com o Take por exemplo para nao sobrecarregar a aplicação,
        //o AsNOtracking é util para quando nao vamos alterar ou seja somente leitura os objetos retornados pois ele nao rastreia em memoria
        //var produtos = _context.Produtos.Include(p => p.Categoria).AsNoTracking().ToList();
        var produtos = _context.Produtos.Include(p => p.Categoria).Where(p => p.Id <= 5).AsNoTracking().ToList();

        if (produtos is null) return NotFound();
        return produtos;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    { 
        var produtos = await _context.Produtos.ToListAsync();

        if (produtos is null) return NotFound();

        return produtos;
        //return StatusCode(StatusCodes.Status100Continue);
    }

    //BindRequired = O nome passa a ser obrigatorio
    //[FromForm] - Utilize somento os dados recebidos do formulario enviado
    //[FromRoute] - Vincula apenas os dados que são oriundos da rota de dados [HttpPut("{id:int}")]
    //[FromQuery] - Recebe apenas os dados da cadeia de consulta queryString ?name=XPTO
    //[FromHeader] - Vincula os valores que vem no cabeçalho da requisiçao HTTP
    //[FromBody] - Vincula os dados a partir do Body do request
    //[FromServices] - Vincula o valor especificado a implementaçao que foi configurada no seu container de injeçao de dependencia

    [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
    public async Task<ActionResult<IEnumerable<Produto>>> Get(int id, [BindRequired] string nome)
    {
        var nomeProduto = nome;
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id); 
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Produto produto)
    {
        _context.Produtos.Add(produto); _context.SaveChanges();
        
        return new CreatedAtRouteResult("ObterProduto",
            new {id = produto.Id}, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if(id != produto.Id) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("id")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p=> p.Id == id);

        if (produto is null) return NotFound();

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok("Produto excluido com sucesso!");
    }
}
