using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Product.API.Entities;

public class Product : EntityAuditBase<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(150)")]
    public required string No { get; init; }

    [Required]
    [Column(TypeName = "nvarchar(250)")]
    public required string Name { get; init; }
    
    [Column(TypeName = "nvarchar(255)")]
    public required string Summary { get; init; }
    
    [Column(TypeName = "nvarchar(4000)")]
    public required string Description { get; init; }
    
    [Column(TypeName = "decimal(12,2)")]
    public decimal Price { get; init; }
}