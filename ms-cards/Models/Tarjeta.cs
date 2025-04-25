using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("cards_emitidas")]
public class Tarjeta
{
    [Key]
    [Column("card_id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("NombreCompleto")]
    public string? NombreCompleto { get; set; }

    [Column("card_number")]
    public string? CardNumber { get; set; }

    [Column("cvv")]
    public int? CVV { get; set; }

    [Column("mes_exp")]
    public int? MesExp { get; set; }

    [Column("año_exp")]
    public int? AñoExp { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
}
