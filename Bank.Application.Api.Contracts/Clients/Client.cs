﻿namespace Bank.Application.Api.Contracts.Clients;

public class Client
{
    public int Id { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; } 
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public decimal Salary { get; set; }
    
    public DateTime BirthDate { get; set; }
}