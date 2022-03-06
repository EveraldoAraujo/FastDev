using FastDev.Notifications;

namespace FastDev.Notifications.Interfaces;
public interface INotification
{
    string? Property { get; set; }
    string? Message { get; set; }
    ENotificationLevel Level { get; set; }
}
