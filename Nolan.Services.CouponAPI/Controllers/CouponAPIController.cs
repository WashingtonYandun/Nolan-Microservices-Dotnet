using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nolan.Services.CouponAPI.Data;
using Nolan.Services.CouponAPI.Models;
using Nolan.Services.CouponAPI.Models.Dto;

namespace Nolan.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        #region Private Fields

        private readonly AppDbContext _db;
        private ResonseDto _response;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResonseDto();
        }

        #endregion

        #region GET Methods

        [HttpGet]
        public async Task<ResonseDto> Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = await _db.Coupons.ToListAsync();
                if (coupons == null || coupons.Count() == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupons not found";
                }
                else
                {
                    _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
                    _response.Message = "Success";
                }
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
        public async Task<ResonseDto> GetById(int id)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<ResonseDto> GetByCode(string code)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower().Equals(code.ToLower()));

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        #endregion

        #region POST Methods

        [HttpPost]
        public async Task<ResonseDto> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                await _db.SaveChangesAsync();

                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        #endregion

        #region PUT Methods

        [HttpPut]
        public async Task<ResonseDto> Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                await _db.SaveChangesAsync();

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        #endregion

        #region DELETE Methods

        [HttpDelete]
        [Route("[action]/{id:int}")]
        public async Task<ResonseDto> DeleteById(int id)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                _db.Coupons.Remove(coupon);
                await _db.SaveChangesAsync();

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("[action]/{code}")]
        public async Task<ResonseDto> DeleteByCode(string code)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower().Equals(code.ToLower()));
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                }
                else
                {
                    _db.Coupons.Remove(coupon);
                    await _db.SaveChangesAsync();

                    _response.Result = _mapper.Map<CouponDto>(coupon);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
    }
}