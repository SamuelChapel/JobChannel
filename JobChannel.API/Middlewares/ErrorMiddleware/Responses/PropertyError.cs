namespace JobChannel.API.Middlewares.ErrorMiddleware.Responses
{
    public class PropertyError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }

        public PropertyError(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }
}

