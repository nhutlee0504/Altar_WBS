using ALtar_WBS.Dto;

namespace ALtar_WBS.Interface
{
	public interface InterfacePayment
	{
		Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
		Task<PaymentDto> GetPaymentByIdAsync(int paymentId);
		Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto);
		Task<PaymentDto> UpdatePaymentAsync(int paymentId, PaymentDto paymentDto);
		Task<bool> DeletePaymentAsync(int paymentId);
	}
}
