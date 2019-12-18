namespace web.api.todo.Models {

    public class ResponseModel<T> {

        public enum Status {
            fail = 0,
            success = 1,
            duplicate = 2,
            notFound = 3
        }

        public T model { get; set; }

        public string message { get; set; }

        public Status status { get; set; }

    }
}
