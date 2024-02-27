﻿using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    public int Ammount { get; set; }
    public double Price { get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie{ get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}
