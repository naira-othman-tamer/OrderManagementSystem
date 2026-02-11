using OrderManagementSystem.Api.ViewModels.AuthVMs;
using OrderManagementSystem.Api.ViewModels.CustomerVMs;
using OrderManagementSystem.Api.ViewModels.InvoiceVMs;
using OrderManagementSystem.Api.ViewModels.OrderVMs;
using OrderManagementSystem.Api.ViewModels.ProductVMs;
using OrderManagementSystem.Application.Services.DTOs.AuthDTOs;
using OrderManagementSystem.Application.Services.DTOs.CustomerDTOs;
using OrderManagementSystem.Application.Services.DTOs.EmailDTOs;
using OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs;
using OrderManagementSystem.Application.Services.DTOs.OrderDTOs;
using OrderManagementSystem.Application.Services.DTOs.ProductDTOs;
using OrderManagementSystem.Application.Services.Implementations;
using OrderManagementSystem.Core.Abstractions.UOW;
using OrderManagementSystem.Infrastructure.UOW;

namespace OrderManagementSystem.Api.Configurations
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Register context & UOW

            builder.RegisterType<OrderManagementDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            #endregion

            #region Register Services

            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InvoiceService>()
                .As<IInvoiceService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>()
                .As<ICustomerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthService>()
                .As<IAuthService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>()
                .As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NotificationService>()
                .As<INotificationService>()
                .InstancePerLifetimeScope();

            #endregion

            #region Register AutoMapper

            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<OrderDTOsProfile>();
                    cfg.AddProfile<ProductDtosProfile>();
                    cfg.AddProfile<CustomerDTOsProfile>();
                    cfg.AddProfile<InvoiceDTOsProfile>();
                    cfg.AddProfile<EmailDTOsProfile>();
                    cfg.AddProfile<AuthDTOsProfile>();

                    cfg.AddProfile<OrderViewModelProfile>();
                    cfg.AddProfile<ProductViewModelProfile>();
                    cfg.AddProfile<CustomerViewModelProfile>();
                    cfg.AddProfile<InvoiceViewModelProfile>();
                    cfg.AddProfile<AuthViewModelProfile>();

                });
                return config;
            }).SingleInstance().AutoActivate().AsSelf();

            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();
                return config.CreateMapper(ctx.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            #endregion

        }
    }
}
