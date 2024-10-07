using System;
using System.Collections.Generic;
using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Exceptions; 

namespace StudentInfoSys.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            // Check if the student exists
            if (student == null)
            {
                throw new StudentNotFoundException("The specified student does not exist.");
            }

            // Validate payment amount
            if (amount <= 0)
            {
                throw new InsufficientFundsException("Payment amount must be greater than zero.");
            }

            var payment = new Payment
            {
                StudentID = student.StudentID,
                Amount = amount,
                PaymentDate = paymentDate
            };
            _paymentRepository.AddPayment(payment);
        }

        public List<Payment> GetPaymentHistory(Student student)
        {
            if (student == null)
            {
                throw new StudentNotFoundException("The specified student does not exist.");
            }

            return _paymentRepository.GetPaymentsByStudent(student.StudentID);
        }

        public decimal GetPaymentAmount(Payment payment)
        {
            if (payment == null)
            {
                throw new PaymentValidationException("The specified payment does not exist.");
            }

            return _paymentRepository.GetPaymentAmount(payment.PaymentID);
        }
        public List<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAllPayments();
        }
        public DateTime GetPaymentDate(Payment payment)
        {
            if (payment == null)
            {
                throw new PaymentValidationException("The specified payment does not exist.");
            }

            return _paymentRepository.GetPaymentDate(payment.PaymentID);
        }
    }
}
