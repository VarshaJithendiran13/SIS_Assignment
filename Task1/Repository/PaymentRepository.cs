using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInfoSys.Utilities;
using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;

namespace StudentInfoSys.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SqlConnection _connection;

        public PaymentRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
        }

        public Payment GetPaymentById(int paymentID)
        {
            Payment payment = null;
            string query = "SELECT * FROM Payments WHERE PaymentID = @PaymentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    payment = new Payment
                    {
                        PaymentID = (int)reader["PaymentID"],
                        StudentID = (int)reader["StudentID"],
                        Amount = (decimal)reader["Amount"],
                        PaymentDate = (DateTime)reader["PaymentDate"]
                    };
                }

                _connection.Close();
            }

            return payment;
        }

        public void AddPayment(Payment payment)
        {
            string query = "INSERT INTO Payments (StudentID, Amount, PaymentDate) VALUES (@StudentID, @Amount, @PaymentDate)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", payment.StudentID);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Payment> GetPaymentsByStudent(int studentID)
        {
            List<Payment> payments = new List<Payment>();
            string query = "SELECT * FROM Payments WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    payments.Add(new Payment
                    {
                        PaymentID = (int)reader["PaymentID"],
                        StudentID = (int)reader["StudentID"],
                        Amount = (decimal)reader["Amount"],
                        PaymentDate = (DateTime)reader["PaymentDate"]
                    });
                }

                _connection.Close();
            }

            return payments;
        }

        public decimal GetPaymentAmount(int paymentID)
        {
            decimal amount = 0;
            string query = "SELECT Amount FROM Payments WHERE PaymentID = @PaymentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                _connection.Open();
                amount = (decimal)cmd.ExecuteScalar();
                _connection.Close();
            }

            return amount;
        }

        public DateTime GetPaymentDate(int paymentID)
        {
            DateTime paymentDate = DateTime.MinValue;
            string query = "SELECT PaymentDate FROM Payments WHERE PaymentID = @PaymentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                _connection.Open();
                paymentDate = (DateTime)cmd.ExecuteScalar();
                _connection.Close();
            }

            return paymentDate;
        }
        public List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();
            string query = "SELECT * FROM Payments";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payment payment = new Payment
                        {
                            PaymentID = (int)reader["PaymentID"],
                            StudentID = (int)reader["StudentID"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        };
                        payments.Add(payment);
                    }
                }
                _connection.Close();
            }

            return payments;
        }
    }
}
