using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Application.DataAccess.Deposits;

[Table("Deposit")]
public class DepositEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Required]
    [Column("client_id")]
    public long ClientId { get; set; }
    
    [Required]
    [Column("deposit_amount")]
    public decimal DepositAmount { get; set; }
    
    [Required]
    [Column("interest_rate")]
    public decimal InterestRate { get; set; }
    
    [Required]
    [Column("period_in_month")]
    public int PeriodInMonth { get; set; }
}