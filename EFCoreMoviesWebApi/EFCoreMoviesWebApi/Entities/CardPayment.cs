﻿namespace EFCoreMoviesWebApi.Entities
{
    public class CardPayment: Payment
    {
        public string Last4Digits { get; set; }
    }
}
