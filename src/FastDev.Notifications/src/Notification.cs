using FastDev.Notifications.Interfaces;

namespace FastDev.Notifications;

public class Notification : INotification
{
    public string? Property { get; set; }
    public string? Message { get; set; }
    public ENotificationLevel Level { get; set; }

    public Notification(string message, string property)
    {
        Property = property;
        Message = message;
        Level = ENotificationLevel.Entity;
    }

    public Notification(string message)
    {
        Message = message;
        Level = ENotificationLevel.Process;
    }

}