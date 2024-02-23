using eTickets.Models.BaseRepository;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Cinema : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Logo")]
    public string Logo { get; set; }
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    public List<Movie> Movies { get; set; }
}
