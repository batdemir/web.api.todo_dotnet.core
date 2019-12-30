using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using web.api.todo.BLL;
using web.api.todo.DAL;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using web.api.todo.Models.Mapper;
using web.api.todo.Models.Response;

namespace web.api.todo.Controllers {

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {

        private TODOContext context;
        private readonly IMapper mapper;

        public UserController(TODOContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<Person>>> Get() {
            return new UserService(context).Get();
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Person>> Get(Guid id) {
            return new UserService(context).GetById(id);
        }

        [HttpPost]
        public ActionResult<ResponseModel<Person>> Post([FromBody]Person value) {
            return new UserService(context).Insert(value);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<Person>> Put([FromBody]Person value) {
            return new UserService(context).Update(value);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<bool>> Delete(Guid id) {
            return new UserService(context).Delete(id);
        }

        //Testing auth login
        [AllowAnonymous]
        [HttpGet("login")]
        public ActionResult<string> Login(string userName, string password) {
            return Ok(new UserService(context).GetToken(userName, password));
        }

        //Testing mapper
        [HttpGet("mapper/{id}")]
        public ActionResult<ViewUser> GetMapper(Guid id) {
            var data = context.Person.Where(person => person.Id.Equals(id)).FirstOrDefault();
            var result = mapper.Map<ViewUser>(data);
            return result;
        }

        [HttpPut("mapper")]
        public void UpdatetMapper([FromBody] ViewUser model) {
            Person desData = context.Person.Where(q => q.Id.Equals(model.Id)).FirstOrDefault();
            Person data = mapper.Map<Person>(model);

            data.IsActive = desData.IsActive;

            new BLLUser(context).Update(data);
        }
    }
}
