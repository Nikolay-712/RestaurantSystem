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

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;

        public PaymentService(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }

        public async Task<string> MakePaymentAsync(string orderId, PaymentsInputModel paymentsInput)
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

                    var processPaymentResult = new ProcessPaymentResult();
                    try
                    {
                        var customers = new CustomerService();


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
                                Currency = "bgn",
                            },
                        };

                        var tokenService = new TokenService();
                        Token paymentToken = await tokenService.CreateAsync(optionToken, options);

                        var customer = new Customer();
                        var customerEmail = "testcustumer@abv.bg";

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
                                    Phone = "",
                                    Name = "custumerTestName".ToString(),
                                    Email = customerEmail,
                                    Description = $"test description",
                                },
                                options);
                        }
                        else
                        {
                            // use existing customer
                            customer = stripeCustomer.FirstOrDefault();
                        }

                        var charges = new ChargeService();
                        var charge = await charges.CreateAsync(
                            new ChargeCreateOptions
                            {
                                Source = paymentToken.Id,
                                Amount = 1000,
                                Currency = "bgn",
                                ReceiptEmail = "test@abv.bg",
                                Description = "new test description",
                            },
                            options);

                        if (charge.Status.ToLower().Equals("succeeded"))
                        {
                            payment.IsSuccessful = true;
                        }
                        else
                        {
                            processPaymentResult.Errors.Add("Error processing payment." + charge.FailureMessage);
                        }

                    }
                    catch (Exception ex)
                    {
                        processPaymentResult.Errors.Add(ex.Message);
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

    public class ProcessPaymentResult
    {
        public List<string> Errors { get; set; }
    }

}
