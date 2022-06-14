using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMoviesWebApi.Entities.Conversions
{
    public class CurrencyToSymbolConverter: ValueConverter<Currency, string>
    {
        public CurrencyToSymbolConverter()
            :base(value => MapCurrencyToString(value),
                 value => MapStringToCurrency(value))
        {

        }
        private static string MapCurrencyToString(Currency value)
        {
            return value switch
            {
                Currency.DominicaPeso => "RD$",
                Currency.USDollar => "$",
                Currency.euro => "€",
                _ => ""
            };
        }

        private static Currency MapStringToCurrency(string value)
        {
            return value switch
            {
                "RD$" => Currency.DominicaPeso,
                "$" => Currency.USDollar,
                "€" => Currency.euro,
                _ => Currency.Unknown
            };
        }
    }
}
