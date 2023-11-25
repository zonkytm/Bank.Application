using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Application.DataAccess.Clients;

[Table("Client")]
public class ClientEntity

{
    [Key]
    public int id { get; set; }
    
    [Required]
    [Column("login")]
    public string Login { get; set; }
    
    [Required]
    [Column("password")]
    public string Password { get; set; } 

    [Required]
    [MaxLength(20)]
    [Column("first_name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("last_name")]
    public string LastName { get; set; }

    [MaxLength(20)]
    [Column("middle_name")]
    public string? MiddleName { get; set; }

    [Required]
    [Column("salary")]
    public decimal Salary { get; set; }

    [Required]
    [Column("birthdate")]
    public DateTime BirthDate { get; set; }
}