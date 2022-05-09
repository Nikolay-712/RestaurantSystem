namespace RestaurantSystem.Data.Models.Contacts
{
    using System;
    using System.Collections.Generic;

    public class AppMessage
    {
        public AppMessage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Replies = new List<MessageReply>();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public MessageStatus Status { get; set; }

        public IEnumerable<MessageReply> Replies { get; set; }

        public bool IsOpen { get; set; }
    }
}
