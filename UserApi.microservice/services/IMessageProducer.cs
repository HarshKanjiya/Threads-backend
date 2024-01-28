namespace UserApi.microservice.services
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(T message);
    }
}
