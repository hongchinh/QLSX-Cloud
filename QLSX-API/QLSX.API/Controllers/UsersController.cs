using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using Serilog;
using QLSX.Shared.Models;
using User = QLSX.Shared.Entities.User;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using SaleAPI.Extensions;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly Models.JWTSettings _jwtsettings;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        public UsersController(CRMDBContext context, IOptions<Models.JWTSettings> jwtsettings,
            ITenantProvider tenantProvider, INhatKyService nhatKyService)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
        }

        // GET: api/Users
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetUsers()
        {
            var query =
               from ue in _context.UserRepository
               where ue.DeletedDate == null
               //join rol in _context.Roles on ue.RoleId equals rol.RoleId into ue_rol
               //from rol in ue_rol.DefaultIfEmpty()
               select new { ue/*, rol*/ };

            var data = await query.Select(x => new UserVM()
            {
                Id = x.ue .Id,
                UserId = x.ue.UserId ?? 0,
                FirstName = x.ue.FirstName,
                EmailAddress = x.ue.EmailAddress,
                RoleId = x.ue.RoleId ?? 0,
                HoTen = x.ue.HoTen,
                //RoleName = x.rol.RoleDesc,
                IsActive = x.ue.IsActive ?? false
            }).ToListAsync();

            return data;
        }

        // GET: api/Users/5
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await _context.UserRepository.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var result = new UserModel(user);
            return result;
        }

        // GET: api/Users/5
        [HttpGet("GetUserDetails/{id}")]
        public async Task<ActionResult<UserModel>> GetUserDetails(int id)
        {
            var user = await _context.UserRepository
                .Where(x => x.DeletedDate == null)
                //.Include(u => u.Role)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserModel(user);
            return result;
        }

        // GET: api/Users/5
        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserModel>> GetUserDetails([FromQuery] string email)
        {
            var userEntity = await _context.UserRepository.FirstOrDefaultAsync(u => u.EmailAddress == email && u.DeletedDate == null);

            if (userEntity == null)
            {
                return NotFound();
            }
            UserModel user = new UserModel(userEntity);
            return user;
        }

        // POST: api/Users
        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var userEntity = await _context.UserRepository
                                       .Where(x => x.DeletedDate == null)
                                       //.Include(u => u.Role)
                                       .FirstOrDefaultAsync(u => u.EmailAddress == request.EmailAddress
                                                                 && u.MatKhau == request.Password);

                Log.Information("Email: " + request.EmailAddress);
                Log.Information("Password: " + request.Password);
                UserWithToken userWithToken = null;

                UserModel user = new UserModel(userEntity);
                if (user != null)
                {
                    if ((bool)!user.IsActive)
                    {
                        return NotFound();
                    }
                    RefreshToken refreshToken = GenerateRefreshToken();
                    user.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync();
                    //user.RoleName = user.Role.RoleDesc;
                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user);
                return userWithToken;
            }
            catch (Exception ex)
            {
                Log.Information("Exception: " + ex.Message);
                Log.Information("Exception: " + ex.StackTrace);
                throw ex;
            }

        }

        // POST: api/Users
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserWithToken>> RegisterUser([FromBody] UserModel model)
        {
            try
            {
                User user = ConvertUserModelToEntity(model);

                _context.UserRepository.Add(user);
                await _context.SaveChangesAsync();

                //load role for registered user
                user = await _context.UserRepository
                     .Where(x => x.DeletedDate == null)
                     //.Include(u => u.Role)
                     .FirstOrDefaultAsync(u => u.Id == model.Id);

                UserWithToken userWithToken = null;

                if (model != null)
                {
                    RefreshToken refreshToken = GenerateRefreshToken();
                    model.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync();

                    userWithToken = new UserWithToken(model);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(model);
                return userWithToken;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        private User ConvertUserModelToEntity(UserModel model)
        {
            var user = new User()
            {
                Id = model.Id,
                HoTen = model.HoTen,
                MatKhau = model.MatKhau,
                QuyenSuDung = model.QuyenSuDung,
                Quyen = model.Quyen,
                TrangThai = model.TrangThai,
                GhiChu = model.GhiChu,
                UserId = model.Id,
                EmailAddress = model.EmailAddress,
                Source = model.Source,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                HireDate = model.HireDate,
                IsActive = model.IsActive,
                IdKt = model.IdKt,
                DMPhongBanId = model.DMPhongBanId,
                MaNhanVienId = model.MaNhanVienId,
                DMDonViSuDungId = model.DMDonViSuDungId,
                CreatedDate = (DateTime)model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                RoleId = model.RoleId,
                DeletedDate = model.DeletedDate,
                CreateBy = model.CreateBy,
            };
            return user;
        }

        // GET: api/Users
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            UserModel user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateAccessToken(user);

                return userWithToken;
            }

            return null;
        }

        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<UserModel>> GetUserByAccessToken([FromBody] string accessToken)
        {
            UserModel user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        private bool ValidateRefreshToken(UserModel user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.ExpiryDate)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        private async Task<UserModel> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userEmail = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var userEntity = await _context.UserRepository/*.Include(u => u.Role) */
                                           .Where(u => u.EmailAddress == userEmail).FirstOrDefaultAsync();
                    //user.RoleName = user.Role.RoleDesc;
                    UserModel user = new UserModel(userEntity);
                    return user;
                }
            }
            catch (Exception ex)
            {
                return new UserModel();
            }

            return new UserModel();
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(1);

            return refreshToken;
        }

        private string GenerateAccessToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.Role, user.Role?.RoleDesc??string.Empty),
                    //new Claim("IsUserEmployedBefore1990", IsUserEmployedBefore1990(user)),
                    //new Claim("IsAdmin", IsAdmin(user)),
                    //new Claim("IsTP", IsTP(user)),
                    //new Claim("IsNV", IsNV(user)),
                    new Claim("UserId", user.Id.ToString() ),
                    new Claim("TenantId", (user.DMDonViSuDungId ?? 0).ToString() ),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateUser/{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> PutUser(int id, UserModel model)
        {
            User entity = await _context.UserRepository.FirstOrDefaultAsync(item => item.DeletedDate == null && item.Id == model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.HoTen = model.HoTen;
            entity.MatKhau = model.MatKhau;
            entity.QuyenSuDung = model.QuyenSuDung;
            entity.Quyen = model.Quyen;
            entity.TrangThai = model.TrangThai;
            entity.GhiChu = model.GhiChu;
            entity.EmailAddress = model.EmailAddress;
            entity.Source = model.Source;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.HireDate = model.HireDate;
            entity.IsActive = model.IsActive;
            entity.IdKt = model.IdKt;
            entity.DMPhongBanId = model.DMPhongBanId;
            entity.MaNhanVienId = model.MaNhanVienId;
            entity.DMDonViSuDungId = model.DMDonViSuDungId;
            entity.RoleId = model.RoleId;
            entity.UpdatedDate = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log Nhat ky
                await _nhatKyService.LogError("Update_User", "id : " + id + ";\nuser : " + model.ToString());
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return model;
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel model)
        {
            User entity = new();
            entity.Id = 0;
            entity.HoTen = model.HoTen;
            entity.MatKhau = model.MatKhau;
            entity.QuyenSuDung = model.QuyenSuDung;
            entity.Quyen = model.Quyen;
            entity.TrangThai = model.TrangThai;
            entity.GhiChu = model.GhiChu;
            entity.EmailAddress = model.EmailAddress;
            entity.Source = model.Source;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.HireDate = model.HireDate;
            entity.IsActive = model.IsActive;
            entity.IdKt = model.IdKt;
            entity.DMPhongBanId = model.DMPhongBanId;
            entity.MaNhanVienId = model.MaNhanVienId;
            entity.DMDonViSuDungId = model.DMDonViSuDungId;
            entity.RoleId = model.RoleId;
            entity.CreateBy = model.CreateBy;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            _context.UserRepository.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new UserModel(entity);
            }
            catch (Exception ex)
            {
                return new UserModel();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            var user = await _context.UserRepository.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.DeletedDate = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
                return new UserModel(user);
            }
            catch (Exception ex)
            {
                return new UserModel();
            }
        }

        private bool UserExists(int id)
        {
            return _context.UserRepository.Any(e => e.Id == id);
        }

        // GET: api/Permissions
        [HttpGet("DMDonViSuDung/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetDMDonViSuDung(int id)
        {
            return await _context.UserRepository/*.Include(x => x.DMPhongBans).Where(p => p.DMDonViSuDungId == id)*/.ToListAsync();
        }

        [HttpGet("ExportToExcel")]
        public async Task<ActionResult<GetAllResponse<UserModel>>> ExportToExcel([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<UserModel> outputs = await GetData(request, false);
            return outputs;
        }

        [HttpGet("GetAllPaged")]
        public async Task<ActionResult<GetAllResponse<UserModel>>> GetAllPaged([FromBody] BaseSearchRequest request)
        {
            GetAllResponse<UserModel> outputs = await GetData(request, true);
            return outputs;
        }

        private async Task<GetAllResponse<UserModel>> GetData(BaseSearchRequest request, bool isPaging)
        {
            GetAllResponse<UserModel> outputs = new GetAllResponse<UserModel>();
            Expression<Func<User, bool>> filter = m => (1 == 1);
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                filter = filter.And(x => x.EmailAddress.Contains(request.Keywords) || x.FirstName.Contains(request.Keywords));
            }
            Func<IQueryable<User>, IOrderedQueryable<User>> order = null;
            if (request.SortDirection != QLSX.Shared.Enums.SortDirection.None)
            {
                order = await OrderBy(request.SortLable, request.SortDirection == QLSX.Shared.Enums.SortDirection.Ascending);
            }
            IQueryable<User> query = _context.UserRepository.Where(item => item.DeletedDate == null);

            if (filter != null) query = query.Where(filter);
            if (order != null) query = order(query);
            outputs.TotalRecords = await query.CountAsync();
            outputs.TotalPages = (int)Math.Ceiling(outputs.TotalRecords / (double)request.PageSize);
            outputs.Page = request.Page;
            outputs.PageSize = request.PageSize;
            if (isPaging)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }
            outputs.Items = await query.Select(item => new UserModel(item)).ToListAsync();
            return outputs;
        }

        private async Task<Func<IQueryable<User>, IOrderedQueryable<User>>> OrderBy(string sortBy, bool sortType)
        {
            Func<IQueryable<User>, IOrderedQueryable<User>> myFunc;
            if (sortBy == "EmailAddress")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.EmailAddress);
                else myFunc = source => source.OrderByDescending(x => x.EmailAddress);
                return myFunc;
            }
            if (sortBy == "FirstName")
            {
                if (sortType) myFunc = source => source.OrderBy(x => x.FirstName);
                else myFunc = source => source.OrderByDescending(x => x.FirstName);
                return myFunc;
            }
            return null;

        }
    }
}
