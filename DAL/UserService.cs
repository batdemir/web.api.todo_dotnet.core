using System;
using System.Collections.Generic;
using web.api.todo.BLL;
using web.api.todo.Models;
using web.api.todo.Models.DB;

namespace web.api.todo.DAL {

    public class UserService : IUserService {

        private TODOContext context;

        public UserService(TODOContext context) {
            this.context = context;
        }

        public ResponseModel<List<User>> Get() {
            ResponseModel<List<User>> response = new ResponseModel<List<User>>();
            List<User> model = new BLLUser(context).Get();
            if (model == null) {
                response.status = ResponseModel<List<User>>.Status.fail;
                response.message = "Bir sorun oluştu.";
            } else if (model != null && model.Count == 0) {
                response.status = ResponseModel<List<User>>.Status.notFound;
                response.message = "Kayıt bulunamadı.";
            } else {
                response.status = ResponseModel<List<User>>.Status.success;
                response.message = "Başarılı";
                response.model = model;
            }
            return response;
        }

        public ResponseModel<User> GetById(Guid userId) {
            ResponseModel<User> response = new ResponseModel<User>();
            if (new BLLUser(context).CheckDataExist(userId)) {
                response.status = ResponseModel<User>.Status.notFound;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseModel<User>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).GetById(userId);
            }
            return response;
        }

        public ResponseModel<User> Insert(User model) {
            ResponseModel<User> response = new ResponseModel<User>();
            if (!new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseModel<User>.Status.duplicate;
                response.message = "Zaten böyle bir kullanıcı bulunmaktadır.";
            } else {
                response.status = ResponseModel<User>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Insert(model);
            }
            return response;
        }

        public ResponseModel<User> Update(User model) {
            ResponseModel<User> response = new ResponseModel<User>();
            if (new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseModel<User>.Status.notFound;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseModel<User>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Update(model);
            }
            return response;
        }

        public ResponseModel<bool> Delete(Guid userId) {
            ResponseModel<bool> response = new ResponseModel<bool>();
            if (new BLLUser(context).CheckDataExist(userId)) {
                response.status = ResponseModel<bool>.Status.notFound;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseModel<bool>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Delete(userId);
            }
            return response;
        }
    }
}
