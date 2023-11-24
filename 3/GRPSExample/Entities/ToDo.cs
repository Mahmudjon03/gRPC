using System.ComponentModel.DataAnnotations;

namespace GRPSExample.Entities;

public class ToDo
{     [Key]
    public int Id { get; set; }

    [MaxLength(100)] 
    public string Title { get; set; } = null!;

    [MaxLength(800)]
    public string Description { get; set; } = null!;
}