using Microsoft.Extensions.Configuration;

namespace web.api.todo.Models {

    public class Global {

        private static Global instance;

        private Global() {
        }

        public static Global getInstance() {
            if(instance == null) {
                instance = new Global();
            }
            return instance;
        }

        public IConfiguration Configration { get; set; }
    }
}
