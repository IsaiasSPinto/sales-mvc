namespace sales_mvc.Services.Exeptions {
    public class NotFoundExeption : ApplicationException {
        public NotFoundExeption(string message) : base(message) { } 
    }
}
