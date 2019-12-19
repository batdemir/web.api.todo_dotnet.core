namespace web.api.todo.Models.Response {

    public class ResponseModel<T> {

        public T model { get; set; }

        public string message { get; set; }

        public ResponseStatus status { get; set; }

    }
}
