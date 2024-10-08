﻿namespace MovieCard_API.Models;

public class ContactInformation
{
    public int Id { get; set; }
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
}