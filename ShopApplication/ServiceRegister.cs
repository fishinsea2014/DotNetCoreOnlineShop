//using Microsoft.Extensions.DependencyInjection;
using Shop.Application.OrdersAdmin;
using Shop.Application.UsersAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<CreateUser>();
            return @this;
        }
    }
}
