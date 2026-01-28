using Confluent.Kafka;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace UserEventProducerApp;

public partial class MainWindow : Window
{
    private string _userId;
    private IProducer<string, UserEvent> _producer;
    private string _topic;

    public MainWindow(
        string userId,
        IProducer<string, UserEvent> producer,
        string topic)
    {
        _userId = userId;
        _producer = producer;
        _topic = topic;

        InitializeComponent();

        UserIdTextBlock.Text = userId;
    }

    private async void ScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
    {
        var message = $"{_userId}: Scrolling";
        Debug.WriteLine(message);

        await SendEventToKafka(message, EventType.Scrolling);
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var message = $"{_userId}: Pressed the \"Buy\" button";
        Debug.WriteLine(message);

        await SendEventToKafka(message, EventType.PressButton);
    }

    private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var message = $"{_userId}: Pressed the \"Clear Cart\" button";
        Debug.WriteLine(message);

        await SendEventToKafka(message, EventType.PressButton);
    }

    private async void Button_Click_2(object sender, RoutedEventArgs e)
    {
        var message = $"{_userId}: Pressed the \"Information\" button";
        Debug.WriteLine(message);

        await SendEventToKafka(message, EventType.PressButton);
    }

    private async void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Image img = (Image)sender;
        string imageNum = img.Tag as string ?? string.Empty;

        var message = $"{_userId}: Clicked on image with number {imageNum}";
        Debug.WriteLine(message);

        await SendEventToKafka(message, EventType.PressGood);
    }

    private async Task SendEventToKafka(string message, EventType eventType)
    {
        try
        {
            var userEvent = new UserEvent
            {
                Message = message,
                EventType = eventType
            };

            var kafkaMessage = new Message<string, UserEvent>
            {
                Key = _userId,
                Value = userEvent
            };

            await _producer.ProduceAsync(_topic, kafkaMessage);

            Debug.WriteLine($"{_userId}: Event sent to Kafka - {eventType}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{_userId}: Error sending to Kafka - {ex.Message}");
        }
    }
}