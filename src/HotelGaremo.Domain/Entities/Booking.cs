using HotelGaremo.Domain.Common;
using HotelGaremo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGaremo.Domain.Entities;

public class Booking : BaseEntity
{
    public string BookingNumber { get; private set; }
    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public decimal TotalPrice { get; private set; }
    public int GuestCount { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public BookingStatus BookingStatus { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; }
    public int CottageId { get; private set; }
    public Cottage Cottage { get; private set; }

    private Booking() { }

    public Booking(string bookingNumber, DateTime checkIn, DateTime checkOut,
                   int guestCount, int userId, int cottageId,
                   decimal totalPrice, PaymentType paymentType)
    {
        BookingNumber = bookingNumber;
        CheckIn = checkIn;
        CheckOut = checkOut;
        GuestCount = guestCount;
        UserId = userId;
        CottageId = cottageId;
        TotalPrice = totalPrice;
        PaymentType = paymentType;
        BookingStatus = BookingStatus.Pending;
    }

    public void ChangeStatus(BookingStatus status)
    {
        BookingStatus = status;
    }
}