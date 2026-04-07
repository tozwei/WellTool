using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class BillTest
{
    [Fact]
    public void ConstructorTest()
    {
        var bill = new Bill("流水号", 100.50m);
        Xunit.Assert.Equal("流水号", bill.SerialNumber);
        Xunit.Assert.Equal(100.50m, bill.Amount);
    }

    [Fact]
    public void StatusTest()
    {
        var bill = new Bill("流水号", 100);
        Xunit.Assert.Equal(BillStatus.Pending, bill.Status);
        bill.Status = BillStatus.Paid;
        Xunit.Assert.Equal(BillStatus.Paid, bill.Status);
    }

    [Fact]
    public void PayTest()
    {
        var bill = new Bill("流水号", 100);
        bill.Pay();
        Xunit.Assert.Equal(BillStatus.Paid, bill.Status);
        Xunit.Assert.NotNull(bill.PaidTime);
    }

    [Fact]
    public void CancelTest()
    {
        var bill = new Bill("流水号", 100);
        bill.Cancel();
        Xunit.Assert.Equal(BillStatus.Cancelled, bill.Status);
    }
}

public class Bill
{
    public string SerialNumber { get; set; }
    public decimal Amount { get; set; }
    public BillStatus Status { get; set; } = BillStatus.Pending;
    public DateTime? PaidTime { get; set; }

    public Bill(string serialNumber, decimal amount)
    {
        SerialNumber = serialNumber;
        Amount = amount;
    }

    public void Pay()
    {
        Status = BillStatus.Paid;
        PaidTime = DateTime.Now;
    }

    public void Cancel()
    {
        Status = BillStatus.Cancelled;
    }
}

public enum BillStatus
{
    Pending,
    Paid,
    Cancelled
}
