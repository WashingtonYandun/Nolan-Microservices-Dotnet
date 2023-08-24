using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nolan.Services.CouponAPI.Data;
using Nolan.Services.CouponAPI.Models;
using Nolan.Services.CouponAPI.Models.Dto;
using System.Reflection.Metadata.Ecma335;

namespace Nolan.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResonseDto _response;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
            _response = new ResonseDto();
        }

        [HttpGet]
        public ResonseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _response.Result = coupons;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResonseDto Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
