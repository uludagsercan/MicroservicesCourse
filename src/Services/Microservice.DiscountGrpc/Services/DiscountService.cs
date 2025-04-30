



using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microservice.DiscountGrpc.Data;
using Microservice.DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DiscountGrpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount"));
            }

            await dbContext.Coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName : {productName}", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            }
            dbContext.Coupons.Remove(coupon);
            var result = await dbContext.SaveChangesAsync() > 0;
            return new DeleteDiscountResponse
            {
                Success = result
            };
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (discount == null)
            {
                discount = new Coupon
                {
                    ProductName = request.ProductName,
                    Description = "No discount available",
                    Amount = 0
                };
            }
            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", discount.ProductName, discount.Amount);
            return discount.Adapt<CouponModel>();
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount"));
            }
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
    }
}