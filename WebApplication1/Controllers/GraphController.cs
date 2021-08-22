using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        [HttpGet("GetFirstGraph")]
        [Authorize]
        public async Task<IEnumerable<GraphResponse>> GetFirstGraph()
        {
            try
            {
                return await Task.FromResult(new List<GraphResponse>
                {

                    new GraphResponse
                    {
                        id=1,
                        month = 1,
                        numberOfEmployees = 0
                    },
                    new GraphResponse
                    {
                        id=2,
                        month = 2,
                        numberOfEmployees = 5
                    },
                    new GraphResponse
                    {
                        id=3,
                        month = 3,
                        numberOfEmployees = 8
                    },
                    new GraphResponse
                    {
                        id=4,
                        month = 4,
                        numberOfEmployees = 5
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 5,
                        numberOfEmployees = 5
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 6,
                        numberOfEmployees = 12
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetSecondGraph")]
        [Authorize]
        public async Task<IEnumerable<GraphResponse>> GetSecondGraph()
        {
            try
            {
                return await Task.FromResult(new List<GraphResponse>
                {
                    new GraphResponse
                    {
                        id=1,
                        month = 1,
                        numberOfEmployees = 2
                    },
                    new GraphResponse
                    {
                        id=2,
                        month = 2,
                        numberOfEmployees = 4
                    },
                    new GraphResponse
                    {
                        id=3,
                        month = 3,
                        numberOfEmployees = 6
                    },
                    new GraphResponse
                    {
                        id=4,
                        month = 4,
                        numberOfEmployees = 2
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 5,
                        numberOfEmployees = 5
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 6,
                        numberOfEmployees = 9
                    }
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetThirdGraph")]
        [Authorize]
        public async Task<IEnumerable<GraphResponse>> GetThirdGraph()
        {
            try
            {
                return await Task.FromResult(new List<GraphResponse>
                {
                    new GraphResponse
                    {
                        id=1,
                        month = 1,
                        numberOfEmployees = 1
                    },
                    new GraphResponse
                    {
                        id=2,
                        month = 2,
                        numberOfEmployees = 7
                    },
                    new GraphResponse
                    {
                        id=3,
                        month = 3,
                        numberOfEmployees = 2
                    },
                    new GraphResponse
                    {
                        id=4,
                        month = 4,
                        numberOfEmployees = 4
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 5,
                        numberOfEmployees = 9
                    },
                    new GraphResponse
                    {
                        id=5,
                        month = 6,
                        numberOfEmployees = 11
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetToken")]
        public IActionResult Login()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:4200",
                audience: "http://localhost:4200",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }

    }
}
