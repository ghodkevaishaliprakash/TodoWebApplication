namespace WebApplication1.Errors
{
    public class NotFound : Exception
    {
        public NotFound(string Messege) : base(Messege) { }
    }
}
