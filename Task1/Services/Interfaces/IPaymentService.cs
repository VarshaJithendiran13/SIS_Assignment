using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;

namespace StudentInfoSys.Services.Interfaces
{
    public interface IPaymentService
    {
        void RecordPayment(Student student, decimal amount, DateTime paymentDate);
        List<Payment> GetPaymentHistory(Student student);
        decimal GetPaymentAmount(Payment payment);
        DateTime GetPaymentDate(Payment payment);
        List<Payment> GetAllPayments();
    }
}

