using HotelGaremo.Domain.Common;
using HotelGaremo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; }
    public int VerificationCode { get; private set; }
    public bool IsVerified { get; private set; }
    public int? PasswordRecoveryCode { get; private set; }
    public UserRoles Role { get; private set; }


    public List<Booking> Bookings { get; private set; } = new List<Booking>();
    public List<Review> Reviews { get; private set; } = new List<Review>();

    private User() { }

    public User(
        string name,
        string lastName,
        string email,
        DateTime dateOfBirth,
        string password,
        string phoneNumber,
        int verificationCode)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Password = password;
        PhoneNumber = phoneNumber;
        VerificationCode = verificationCode;

        Role = UserRoles.User;
        IsVerified = false;
    }
}