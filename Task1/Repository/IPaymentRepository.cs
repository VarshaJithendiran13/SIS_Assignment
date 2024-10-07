using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Repository
{
    public interface IPaymentRepository
    {
        Payment GetPaymentById(int paymentID);
        void AddPayment(Payment payment);
        List<Payment> GetPaymentsByStudent(int studentID);
        decimal GetPaymentAmount(int paymentID);
        List<Payment> GetAllPayments();
        DateTime GetPaymentDate(int paymentID);
    }
}


