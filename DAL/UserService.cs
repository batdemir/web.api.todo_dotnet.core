﻿using System;
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

        public ResponseModel<List<Person>> Get() {
            ResponseModel<List<Person>> response = new ResponseModel<List<Person>>();
            List<Person> model = new BLLUser(context).Get();
            if (model == null) {
                response.status = ResponseModel<List<Person>>.Status.fail;
                response.message = "Bir sorun oluştu.";
            } else if (model != null && model.Count == 0) {
                response.status = ResponseModel<List<Person>>.Status.notFound;
                response.message = "Kayıt bulunamadı.";
            } else {
                response.status = ResponseModel<List<Person>>.Status.success;
                response.message = "Başarılı";
                response.model = model;
            }
            return response;
        }

        public ResponseModel<Person> GetById(Guid userId) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (new BLLUser(context).CheckDataExist(userId)) {
                response.status = ResponseModel<Person>.Status.notFound;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseModel<Person>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).GetById(userId);
            }
            return response;
        }

        public ResponseModel<Person> Insert(Person model) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (!new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseModel<Person>.Status.duplicate;
                response.message = "Zaten böyle bir kullanıcı bulunmaktadır.";
            } else {
                response.status = ResponseModel<Person>.Status.success;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Insert(model);
            }
            return response;
        }

        public ResponseModel<Person> Update(Person model) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseModel<Person>.Status.notFound;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseModel<Person>.Status.success;
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
