using UberSystem.Domain.Interfaces.Services;
using MimeKit;
using MailKit.Net.Smtp;
using KoishopBusinessObjects;
using System.Text;

namespace UberSystem.Service
{
  public class EmailService : IEmailService
  {
    public async Task SendEmailVerificationAsync(string? email, List<KoiFish> list, Order order)
    {
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("KoiShop", "noreply@ubersystem.com"));
      message.To.Add(new MailboxAddress("", email));
      message.Subject = "Confirm fish purchase information";

       // Build the fish list HTML
    

    message.Body = new TextPart("html")
    {
        Text = GetMessageBody(list, order)
    };

      using (var client = new SmtpClient())
      {
        // Configure your SMTP settings here
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("ngocnbse171101@fpt.edu.vn", "ozhh ebpw qpov nter");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
      }
    }

    private string GetMessageBody (List<KoiFish> list, Order order) 
    {
      var fishListHtml = new StringBuilder();
      foreach (var koi in list)
      {
        fishListHtml.Append($@"
          <tr>
              <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{koi.Name}</td>
              <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{koi.Age} months</td>
              <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{koi.Gender}</td>
              <td style='padding: 8px; border-bottom: 1px solid #ddd;'>${koi.Price}</td>
          </tr>");
      }

      var body = $@"
        <html>
        <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
            <h2 style='color: #2c3e50;'>Fish Purchase Information</h2>
            
            <div style='margin: 20px 0; padding: 15px; background-color: #fafafa; border-radius: 5px;'>
                <p><strong>Order Date:</strong> {order.OrderDate}</p>
                <p><strong>Total Items:</strong> {order.OrderItems?.Count} pcs</p>
                
                <table style='width: 100%; border-collapse: collapse; margin-top: 15px;'>
                    <thead>
                        <tr style='background-color: #f1f1f1;'>
                            <th style='padding: 8px; text-align: left; border-bottom: 2px solid #ddd;'>Name</th>
                            <th style='padding: 8px; text-align: left; border-bottom: 2px solid #ddd;'>Age</th>
                            <th style='padding: 8px; text-align: left; border-bottom: 2px solid #ddd;'>Gender</th>
                            <th style='padding: 8px; text-align: left; border-bottom: 2px solid #ddd;'>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {fishListHtml}
                    </tbody>
                </table>

                <p style='margin-top: 15px;'><strong>Total Amount:</strong> ${order.TotalAmount}</p>
            </div>

            <p>Please confirm and reply to this email if there is any problem with your order.</p>
            <p style='margin-top: 20px;'><em>Thanks for relying on our shop!</em></p>
        </body>
        </html>";
      
      return body;
    } 
  }
}