namespace WebApplication1.Errors
{
    public class BadRequest : Exception
    {
        public BadRequest(string Messege) : base(Messege) { }
    }
}
