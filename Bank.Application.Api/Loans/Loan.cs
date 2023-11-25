namespace Bank.Application.Api.Loans;

/// <summary>
/// Кредит.
/// </summary>
public class Loan
{
    /// <summary>
    /// Проценты по займу.
    /// </summary>
    public decimal InterestRate;
    
    /// <summary>
    /// Сумма кредита.
    /// </summary>
    public decimal LoanAmount;
    
    /// <summary>
    /// Ежемесяный платеж.
    /// </summary>
    public decimal MonthlyPayment;
    
    /// <summary>
    /// Статус кредита.
    /// </summary>
    public string Status;
}