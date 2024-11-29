using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALtar_WBS.Service
{
    public class ServicePayment : InterfacePayment
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServicePayment(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _context.payments
                    .Select(p => new PaymentDto
                    {
                        Amount = p.Amount,
                        PaymentDate = p.PaymentDate,
                        Method = p.Method,
                        StudentID = p.StudentID
                    })
                    .ToListAsync();

                if (!payments.Any())
                    throw new InvalidOperationException("No payments found.");

                return payments;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving payments.", ex);
            }
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                var payment = await _context.payments.FindAsync(paymentId);

                if (payment == null)
                    throw new InvalidOperationException($"Payment with ID {paymentId} not found.");

                return new PaymentDto
                {
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    Method = payment.Method,
                    StudentID = payment.StudentID
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving payment with ID {paymentId}.", ex);
            }
        }

        public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        {
            try
            {
                var payment = new Payment
                {
                    Amount = paymentDto.Amount,
                    PaymentDate = paymentDto.PaymentDate,
                    Method = paymentDto.Method,
                    StudentID = paymentDto.StudentID
                };

                _context.payments.Add(payment);
                await _context.SaveChangesAsync();

                return paymentDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating payment.", ex);
            }
        }

        public async Task<PaymentDto> UpdatePaymentAsync(int paymentId, PaymentDto paymentDto)
        {
            try
            {
                var payment = await _context.payments.FindAsync(paymentId);

                if (payment == null)
                    throw new InvalidOperationException($"Payment with ID {paymentId} not found.");

                payment.Amount = paymentDto.Amount;
                payment.PaymentDate = paymentDto.PaymentDate;
                payment.Method = paymentDto.Method;
                payment.StudentID = paymentDto.StudentID;

                await _context.SaveChangesAsync();

                return paymentDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating payment with ID {paymentId}.", ex);
            }
        }

        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            try
            {
                var payment = await _context.payments.FindAsync(paymentId);

                if (payment == null)
                    throw new InvalidOperationException($"Payment with ID {paymentId} not found.");

                _context.payments.Remove(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting payment with ID {paymentId}.", ex);
            }
        }
    }
}
