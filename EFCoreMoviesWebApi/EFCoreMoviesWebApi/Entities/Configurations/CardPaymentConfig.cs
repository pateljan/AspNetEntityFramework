using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class CardPaymentConfig : IEntityTypeConfiguration<CardPayment>
    {
        public void Configure(EntityTypeBuilder<CardPayment> builder)
        {
            builder.Property(p => p.Last4Digits).HasColumnType("char(4)").IsRequired();

            var payment1 = new CardPayment()
            {
                Id = 3,
                PaymentType = PaymentType.Card,
                PaymentDate = new DateTime(2022, 4, 9),
                Amount = 16.25m,
                Last4Digits = "1234"
            };

            var payment2 = new CardPayment()
            {
                Id = 4,
                PaymentType = PaymentType.Card,
                PaymentDate = new DateTime(2022, 3, 15),
                Amount = 22,
                Last4Digits = "8555"
            };

            builder.HasData(payment1, payment2);
        }
    }
}
