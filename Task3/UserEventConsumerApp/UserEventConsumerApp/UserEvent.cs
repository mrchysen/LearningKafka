namespace UserEventConsumerApp;

public class UserEvent
{
    public string Message { get; set; } = null!;

    public EventType EventType { get; set; }
}

public enum EventType
{
    Scrolling,
    PressButton,
    PressGood
}