namespace RestaurantSystem.Data.Models.Contacts
{
    using System;

    public class MessageReply
    {
        public MessageReply()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Text { get; set; }

        public string MessageId { get; set; }
    }
}
