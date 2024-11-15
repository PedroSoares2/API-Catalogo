using APICatalogo.Context;
using APICatalogo.DTOs;
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
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _repository.All().ToList();

        if (categorias is null) return NotFound();
        
        var categoriasDto = new List<CategoriaDTO>();

        categorias.ForEach(c =>
        {
            categoriasDto.Add(new CategoriaDTO()
            {
                Id = c.Id,
                Nome = c.Nome,
                ImagemUrl = c.ImagemUrl
            });
        });

        return Ok(categoriasDto);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<IEnumerable<CategoriaDTO>> Get(int id)
    {
        var categoria = _repository.Get(c=> c.Id == id);

        //Mapeamento manual
        var categoriaDto = new CategoriaDTO()
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post([FromBody] CategoriaDTO categoria)
    {
        _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoria)
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
