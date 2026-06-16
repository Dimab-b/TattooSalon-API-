namespace WebApiArchitecture.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name , object key) : base($"Об'єкт '{name}' з ID ({key}) не знайдено.") { }
    }
}
