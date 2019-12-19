using System;
using System.Collections.Generic;
using web.api.todo.BLL;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.DAL {

    public class TodoListService :ITodoListService {

        private TODOContext context;

        public TodoListService(TODOContext context) {
            this.context = context;
        }

        public ResponseModel<List<CustomTodoList>> Get() {
            ResponseModel<List<CustomTodoList>> response = new ResponseModel<List<CustomTodoList>>();
            List<CustomTodoList> model = new BLLTodoList(context).Get();
            if(model == null) {
                response.status = ResponseStatus.FAIL;
                response.message = "Bir sorun oluştu.";
            } else if((model != null && model.Count == 0)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kayıt bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
            }
            response.model = model;
            return response;
        }

        public ResponseModel<List<CustomTodoList>> GetByTodo(Guid todoId) {
            ResponseModel<List<CustomTodoList>> response = new ResponseModel<List<CustomTodoList>>();
            List<CustomTodoList> model = new BLLTodoList(context).GetByTodo(todoId);
            if(model == null) {
                response.status = ResponseStatus.FAIL;
                response.message = "Bir sorun oluştu.";
            } else if((model != null && model.Count == 0)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Bu Todo'ya ait kayıt bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
            }
            response.model = model;
            return response;
        }

        public ResponseModel<List<CustomTodoList>> GetByUser(Guid userId) {
            ResponseModel<List<CustomTodoList>> response = new ResponseModel<List<CustomTodoList>>();
            List<CustomTodoList> model = new BLLTodoList(context).GetByUser(userId);
            if(model == null) {
                response.status = ResponseStatus.FAIL;
                response.message = "Bir sorun oluştu.";
            } else if((model != null && model.Count == 0)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Bu User'a ait kayıt bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
            }
            response.model = model;
            return response;
        }
    }
}
