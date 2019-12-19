using System;
using System.Collections.Generic;
using web.api.todo.BLL;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.DAL {

    public class TodoService :ITodoService {

        private TODOContext context;

        public TodoService(TODOContext context) {
            this.context = context;
        }

        public ResponseModel<List<Todo>> Get() {
            ResponseModel<List<Todo>> response = new ResponseModel<List<Todo>>();
            List<Todo> model = new BLLTodo(context).Get();
            if(model == null) {
                response.status = ResponseStatus.FAIL;
                response.message = "Bir sorun oluştu.";
            } else if(model != null && model.Count == 0) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kayıt bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = model;
            }
            return response;
        }

        public ResponseModel<Todo> GetById(Guid todoId) {
            ResponseModel<Todo> response = new ResponseModel<Todo>();
            if(new BLLTodo(context).CheckDataExist(todoId)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Todo bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLTodo(context).GetById(todoId);
            }
            return response;
        }

        public ResponseModel<Todo> Insert(Todo model) {
            ResponseModel<Todo> response = new ResponseModel<Todo>();
            if(!new BLLTodo(context).CheckDataExist(model.Id)) {
                response.status = ResponseStatus.DUPLICATE;
                response.message = "Zaten böyle bir Todo bulunmaktadır.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLTodo(context).Insert(model);
            }
            return response;
        }

        public ResponseModel<Todo> Update(Todo model) {
            ResponseModel<Todo> response = new ResponseModel<Todo>();
            if(new BLLTodo(context).CheckDataExist(model.Id)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Todo bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLTodo(context).Update(model);
            }
            return response;
        }

        public ResponseModel<bool> Delete(Guid todoId) {
            ResponseModel<bool> response = new ResponseModel<bool>();
            if(new BLLTodo(context).CheckDataExist(todoId)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Todo bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLTodo(context).Delete(todoId);
            }
            return response;
        }
    }
}
