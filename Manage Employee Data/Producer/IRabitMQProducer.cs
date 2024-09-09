namespace Manage_Employee_Data.Producer
{
    public interface IRabitMQProducer
    {
        public void SendEmployeeMessage<T>(T message);
    }
}
