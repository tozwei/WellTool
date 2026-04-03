using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class BillTest
{
    [Fact]
    public void ConstructorTest()
    {
        var bill = new Bill("流水号", 100.50m);
        Assert.Equal("流水号", bill.SerialNumber);
        Assert.Equal(100.50m, bill.Amount);
    }

    [Fact]
    public void StatusTest()
    {
        var bill = new Bill("流水号", 100);
        Assert.Equal(BillStatus.Pending, bill.Status);
        bill.Status = BillStatus.Paid;
        Assert.Equal(BillStatus.Paid, bill.Status);
    }

    [Fact]
    public void PayTest()
    {
        var bill = new Bill("流水号", 100);
        bill.Pay();
        Assert.Equal(BillStatus.Paid, bill.Status);
        Assert.NotNull(bill.PaidTime);
    }

    [Fact]
    public void CancelTest()
    {
        var bill = new Bill("流水号", 100);
        bill.Cancel();
        Assert.Equal(BillStatus.Cancelled, bill.Status);
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
