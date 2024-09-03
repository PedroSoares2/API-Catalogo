using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;

    public CategoriasController(IRepository<Categoria> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _repository.All();

        if (categorias is null) return NotFound();

        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<IEnumerable<Categoria>> Get(int id)
    {
        var categoria = _repository.Get(c=> c.Id == id);
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Categoria categoria)
    {
        _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.Id) return BadRequest();

        _repository.Update(categoria);

        return Ok(categoria);
    }

    [HttpDelete("id")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.Id == id);

        _repository.Delete(categoria);

        return Ok("categoria excluido com sucesso!");
    }
}
