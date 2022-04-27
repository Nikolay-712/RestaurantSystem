namespace RestaurantSystem.Data.Models.Contacts
{
    using System;

    public class ApplicationMessage
    {
        public ApplicationMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public MessageType MessageType { get; set; }

        public string Sender { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }
    }
}
