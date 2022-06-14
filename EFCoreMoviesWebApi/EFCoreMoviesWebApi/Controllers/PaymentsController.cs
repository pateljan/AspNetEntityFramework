﻿using EFCoreMoviesWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GET()
        {
            return await _context.Payments.ToListAsync();
        }

        [HttpGet("cards")]
        public async Task<ActionResult<IEnumerable<CardPayment>>> GetCardPayments()
        {
            return await _context.Payments.OfType<CardPayment>().ToListAsync();
        }

        [HttpGet("paypal")]
        public async Task<ActionResult<IEnumerable<PaypalPayment>>> GetPaypalPayments()
        {
            return await _context.Payments.OfType<PaypalPayment>().ToListAsync();
        }
    }
}
