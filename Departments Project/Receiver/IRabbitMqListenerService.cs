namespace Departments_Project.Receiver
{
    public interface IRabbitMqListenerService
    {
        String ReceiveMessage();
    }
}
