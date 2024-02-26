using eTickets;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MovieVM 
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Movie Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Movie Description")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Start date")]
    public DateTime StartDate { get; set; }

    [Required]
    [Display(Name = "End date")]
    public DateTime EndDate { get; set; }

    [Required]
    [Display(Name = "Movie Price")]
    public double Price { get; set; }

    [Required]
    [Display(Name = "Movie poster")]
    public string ImageURL { get; set; }

    [Required]
    [Display(Name = "Select category")]
    public MovieCategory MovieCategory { get; set; }

    [Required]
    [Display(Name = "Select a cinema")]
    public int CinemaId { get; set; }

    [Required]
    [Display(Name = "Select a producer")]
    public int ProducerId { get; set; }

    [Required]
    [Display(Name = "Select actor/s")]
    public List<int> ActorIds { get; set; }
}
