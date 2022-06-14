using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class PaypalPaymentConfig : IEntityTypeConfiguration<PaypalPayment>
    {
        public void Configure(EntityTypeBuilder<PaypalPayment> builder)
        {
            builder.Property(p => p.EmailAddress).IsRequired();

            var payment1 = new PaypalPayment()
            {
                Id = 1,
                PaymentType = PaymentType.Paypal,
                PaymentDate = new DateTime(2022, 2, 3),
                EmailAddress = "felipe@gmail.com",
                Amount = 125
            };

            var payment2 = new PaypalPayment()
            {
                Id = 2,
                PaymentType = PaymentType.Paypal,
                PaymentDate = new DateTime(2022, 4, 10),
                EmailAddress = "claudia@gmail.com",
                Amount = 256
            };

            builder.HasData(payment1, payment2);
        }
    }
}
