using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using web.api.todo.BLL;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

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
                response.status = ResponseStatus.FAIL;
                response.message = "Bir sorun oluştu.";
            } else if (model != null && model.Count == 0) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kayıt bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = model;
            }
            return response;
        }

        public ResponseModel<Person> GetById(Guid userId) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (new BLLUser(context).CheckDataExist(userId)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLUser(context).GetById(userId);
            }
            return response;
        }

        public ResponseModel<Person> Insert(Person model) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (!new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseStatus.DUPLICATE;
                response.message = "Zaten böyle bir kullanıcı bulunmaktadır.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Insert(model);
            }
            return response;
        }

        public ResponseModel<Person> Update(Person model) {
            ResponseModel<Person> response = new ResponseModel<Person>();
            if (new BLLUser(context).CheckDataExist(model.Id)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Update(model);
            }
            return response;
        }

        public ResponseModel<bool> Delete(Guid userId) {
            ResponseModel<bool> response = new ResponseModel<bool>();
            if (new BLLUser(context).CheckDataExist(userId)) {
                response.status = ResponseStatus.NOT_FOUND;
                response.message = "Kullanıcı bulunamadı.";
            } else {
                response.status = ResponseStatus.SUCCESS;
                response.message = "Başarılı";
                response.model = new BLLUser(context).Delete(userId);
            }
            return response;
        }

        public string GetToken(string userName, string password) {
            var user = new BLLUser(context).ValidUser(userName, password);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Global.getInstance().Configration["Issuer"],
                audience: Global.getInstance().Configration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Global.getInstance().Configration["SigningKey"])), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
