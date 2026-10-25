using Company.Finance;

namespace Company.Business_HR
{
    public class User(string fullName, string position, string department)
    {
        public string FullName { get; set; } = fullName;
        public string Position { get; set; } = position;
        public string Department { get; set; } = department;

        public void DisplayInfo() => Console.WriteLine($"""
                [HR USER] {FullName}
                Position: {Position}
                Department: {Department}
                ----------------------------
                """);
    }
}

namespace Company.Security
{
    public class User(string username, string role, bool isActive)
    {
        public string Username { get; set; } = username;
        public string Role { get; set; } = role;
        public bool IsActive { get; set; } = isActive;

        public void ShowSecurityInfo() => Console.WriteLine($"""
                [SECURITY USER] Username: {Username}
                Role: {Role}
                Status: {(IsActive ? "Active" : "Locked")}
                ----------------------------
                """);

        public void Login()
        {
            if (!IsActive)
            {
                Console.WriteLine($"Login denied for {Username}: account is locked.");
                return;
            }

            Console.WriteLine($"User {Username} has logged into the system.");
        }
    }
}

namespace Company.Finance
{
    public class Payroll(string paymentId, decimal amount)
    {
        public string PaymentId { get; set; } = paymentId;
        public decimal Amount { get; set; } = amount;

        public void ProcessPayment() => Console.WriteLine($"""
                [Finance] Processing payment {PaymentId}
                Amount: {Amount:N0}
                Payment processed successfully
                ----------------------------
                """);
    }

    public class FinancialReport
    {
        public void GenerateReport() => Console.WriteLine("[Finance] Financial report has been created.");
    }
}

namespace Main
{
    public static class Run
    {
        public static void Main()
        {
            var hrUser = new Company.Business_HR.User("Adam Smith", "HR Manager", "HR");
            hrUser.DisplayInfo();

            var payroll = new Payroll("PAY202501", 45_000_000m);
            payroll.ProcessPayment();

            var securityUser = new Company.Security.User("Adin Ross", "Security Administrator", true);
            securityUser.ShowSecurityInfo();
            securityUser.Login();

            var report = new FinancialReport();
            report.GenerateReport();
        }
    }
}