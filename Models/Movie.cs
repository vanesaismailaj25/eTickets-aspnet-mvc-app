using eTickets;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Movie
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    [Display(Name = "Start date")]
    public DateTime StartDate { get; set; }
    [Display(Name = "End date")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Price")]
    public double Price { get; set; }
    [Display(Name = "Image")]
    public string ImageURL { get; set; }
    public MovieCategory MovieCategory { get; set; }
    public int CinemaId { get; set; }
    [ForeignKey("CinemaId")]
    public Cinema Cinema { get; set; }
    public int ProducerId { get; set; }
    [ForeignKey("ProducerId")]
    public Producer Producer { get; set; }
    public List<ActorMovie> ActorsMovies { get; set; }
}
