using Confluent.Kafka;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UserEventProducerApp;

namespace UserEventConsumerApp;

public class Program
{
    public static ObservableCollection<UserStatistic> statistics = new();

    public static DataGrid dataGrid;

    [STAThread]
    public static void Main(string[] args)
    {
        var topic = "logs";

        var consumerConfig = new ConsumerConfig
        {
            GroupId = "1",
            BootstrapServers = "localhost:9092",
        };

        using var consumer =
            new ConsumerBuilder<string, UserEvent>(consumerConfig)
            .SetValueDeserializer(new KafkaDeserializer<UserEvent>())
            .Build();

        consumer.Subscribe(topic);

        var app = new Application();

        app.Startup += (s, e) =>
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            dataGrid = mainWindow.DataGrid;

            var dispatcher = mainWindow.Dispatcher;

            dataGrid.ItemsSource = statistics;

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var cr = consumer.Consume();

                        var userId = cr.Message.Key;
                        var userEvent = cr.Message.Value;

                        dispatcher.Invoke(() =>
                        {
                            UpdateStatistic(userId, userEvent, cr.Partition);
                        });

                        Debug.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Debug.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            });
        };

        app.Run();
    }

    public static void UpdateStatistic(string userId, UserEvent userEvent, int partition)
    {
        var statisticItem = statistics.FirstOrDefault(s => s.UserId == userId);

        if (statisticItem is not null)
        {
            // Обновляем существующую запись
            UpdateItem(statisticItem, userEvent);

            statistics.Remove(statisticItem);
            statistics.Add(statisticItem);
        }
        else
        {
            // Создаем новую запись
            statisticItem = new UserStatistic()
            {
                UserId = userId,
                Partition = partition
            };

            UpdateItem(statisticItem, userEvent);
            statistics.Add(statisticItem);
        }
    }

    public static void UpdateItem(UserStatistic statisticItem, UserEvent userEvent)
    {
        switch (userEvent.EventType)
        {
            case EventType.Scrolling:
                statisticItem.SrollingTimes++;
                break;
            case EventType.PressButton:
                statisticItem.BuyButtonPressedTimes++;
                break;
            case EventType.PressGood:
                statisticItem.ClickOnImagesTimes++;
                break;
        }
    }
}
