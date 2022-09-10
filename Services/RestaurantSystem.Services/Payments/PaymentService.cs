namespace RestaurantSystem.Services.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Web.ViewModels.Payments;
    using Stripe;

    using static RestaurantSystem.Common.GlobalConstants;

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;

        public PaymentService(
            ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }

        public async Task<string> MakePaymentAsync(
            string orderId, PaymentsInputModel paymentsInput, decimal amount)
        {
            var payment = new Payment
            {
                CreatedOn = DateTime.Now,
                PaymentType = paymentsInput.PaymentType,
                OrderId = orderId,
            };

            switch (paymentsInput.PaymentType)
            {
                case PaymentType.Cash:
                    payment.IsSuccessful = true;
                    break;
                case PaymentType.DebitCard:
                    try
                    {
                        var customers = new CustomerService();
                        paymentsInput.ProcessPaymentResult = new HashSet<string>();

                        var options = new RequestOptions
                        {
                            ApiKey = "",
                        };

                        var optionToken = new TokenCreateOptions
                        {
                            Card = new TokenCardOptions
                            {
                                Number = paymentsInput.CardNumber,
                                ExpMonth = paymentsInput.Expiration.Value.Month.ToString(),
                                ExpYear = paymentsInput.Expiration.Value.Day.ToString(),
                                Cvc = paymentsInput.CVV,
                                Name = paymentsInput.CardName,
                                Currency = BGN,
                            },
                        };

                        var tokenService = new TokenService();
                        Token paymentToken = await tokenService.CreateAsync(optionToken, options);

                        var customer = new Customer();
                        var customerEmail = paymentsInput.CustomerEmail;

                        var stripeCustomer = await customers.ListAsync(
                            new CustomerListOptions
                            {
                                Email = customerEmail,
                                Limit = 1,
                            },
                            options);

                        if (stripeCustomer.Data.Count == 0)
                        {
                            // create new customer
                            customer = await customers.CreateAsync(
                                new CustomerCreateOptions
                                {
                                    Source = paymentToken.Id,
                                    Name = paymentsInput.CardName,
                                    Email = customerEmail,
                                    Description = $"New stripe customer - {customerEmail}",
                                },
                                options);

                            paymentToken = await tokenService.CreateAsync(optionToken, options);
                        }
                        else
                        {
                            // use existing customer
                            customer = stripeCustomer
                                .FirstOrDefault(x => x.Email.ToString() == customerEmail);
                        }

                        var charges = new ChargeService();
                        var charge = await charges.CreateAsync(
                            new ChargeCreateOptions
                            {
                                Source = paymentToken.Id,
                                Amount = (int)(Math.Round(amount, 2) * 100),
                                Currency = BGN,
                                ReceiptEmail = "test@abv.bg",
                                Description = $"New payment - {orderId}",
                            },
                            options);

                        if (charge.Status.ToLower().Equals("succeeded"))
                        {
                            payment.IsSuccessful = true;
                        }
                        else
                        {
                            paymentsInput.ProcessPaymentResult.Add("Error processing payment." + charge.FailureMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        paymentsInput.ProcessPaymentResult.Add(ex.Message);
                    }

                    break;
                default:
                    break;
            }

            if (!payment.IsSuccessful)
            {
                return null;
            }

            await this.applicationDbContext.Payments.AddAsync(payment);
            await this.applicationDbContext.SaveChangesAsync();

            return payment.Id;
        }
    }
}
