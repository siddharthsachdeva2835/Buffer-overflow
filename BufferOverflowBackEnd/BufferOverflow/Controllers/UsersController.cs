using BufferOverflow.Models;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared;
using BufferOverflow.Config;
using System.Web.Http.Description;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace BufferOverflow.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [Route("current")]
        [HttpGet]
        public IHttpActionResult CurrentUser()
        {
            IEnumerable<string> headerValues;
            var nameFilter = string.Empty;
            if (Request.Headers.TryGetValues("token", out headerValues))
            {
                nameFilter = headerValues.FirstOrDefault();
            }

            try
            {
                UserBDC userBDC = new UserBDC();
                UserDTO userDTO = userBDC.getUserByToken(nameFilter);

                if (userDTO != null)
                {
                    return Ok(MapConfig.mapper.Map<UserDTO, User>(userDTO));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }



        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginUser user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("invalid data");
            }

            try
            {
        
                UserDTO loginUserDTO = MapConfig.mapper.Map<LoginUser, UserDTO>(user);

                UserBDC userBDC = new UserBDC();
                UserDTO userDTO= userBDC.LoginUser(loginUserDTO);

                return Ok(MapConfig.mapper.Map<UserDTO, User>(userDTO));

            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> PostUser()
        {
            if (Request.Content.IsMimeMultipartContent())
            {

                try
                {
                    string fullPath = HttpContext.Current.Server.MapPath("~/images");
                    var streamProvider = new CustomMultipartFormDataStreamProvider(fullPath);
                    var result = await Request.Content.ReadAsMultipartAsync(streamProvider);
                    var newUser = JsonConvert.DeserializeObject<UserDTO>(streamProvider.FormData.Get("NewUser"));
                    var fname = result.FileData[0].LocalFileName;
                    FileInfo fi = new FileInfo(fname.ToString());

                    UserBDC userBDC = new UserBDC();
                    UserDTO userDTO = userBDC.RegisterUser(newUser, "images/" + fi.Name);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Ok();
            //try
            //{
            //    //user.ImageURL = "images/" + user.FirstName;
            //    //UserDTO UserDTO = MapConfig.mapper.Map<RegisterUser, UserDTO>(user);

            //    //UserBDC userBDC = new UserBDC();
            //    //UserDTO userDTO = userBDC.RegisterUser(UserDTO);

            //    //return Ok(MapConfig.mapper.Map<UserDTO, User>(userDTO));
            //    return Ok();

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string fileName;
            if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
            {
                var ext = Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", string.Empty));
                fileName = Guid.NewGuid() + ext;
            }
            else
            {
                fileName = Guid.NewGuid() + ".data";
            }
            return fileName;
        }
    }

}
