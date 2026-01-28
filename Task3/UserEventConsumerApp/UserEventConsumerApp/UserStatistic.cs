namespace UserEventConsumerApp;

public class UserStatistic
{
    public string UserId { get; set; }

    public int Partition { get; set; }

    public int SrollingTimes { get; set; }

    public int BuyButtonPressedTimes { get; set; }

    public int ClearCartButtonPressedTimes { get; set; }

    public int InformationButtonPressedTimes { get; set; }

    public int ClickOnImagesTimes { get; set; }
}
