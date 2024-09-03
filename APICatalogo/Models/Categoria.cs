using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria : IValidatableObject
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80)]
    [JsonProperty("Nome Fantasia")]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var primeiraLetra = this.Nome[0].ToString();
        if (primeiraLetra != primeiraLetra.ToUpper())
        {
            yield return new
                ValidationResult("A primeira letra da categoria deve ser maiscula",
                new[]
                { nameof(this.Nome)}
                );
        }
    }
}
