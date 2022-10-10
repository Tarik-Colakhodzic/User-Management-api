namespace Application.Models.Responses
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Items = data;
        }

        public T Items { get; set; }
    }
}