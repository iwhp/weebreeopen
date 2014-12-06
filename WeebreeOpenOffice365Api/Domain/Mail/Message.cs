namespace WeebreeOpen.Office365Api.Domain.Mail
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject("Value")]
    public class Message
    {
        public string odataid { get; set; }
        public string odataetag { get; set; }
        public string Id { get; set; }
        public string ChangeKey { get; set; }
        public object[] Categories { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeLastModified { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public MessageBody Body { get; set; }
        public string Importance { get; set; }
        public bool HasAttachments { get; set; }
        public string ParentFolderId { get; set; }
        public MessageFrom From { get; set; }
        public MessageSender Sender { get; set; }
        public MessageToRecipient[] ToRecipients { get; set; }
        public MessageCcRecipient[] CcRecipients { get; set; }
        public object[] BccRecipients { get; set; }
        [JsonProperty("ReplyTo")]
        public MessageReplyTo[] ReplyTos { get; set; }
        public string ConversationId { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public DateTime DateTimeSent { get; set; }
        public object IsDeliveryReceiptRequested { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public bool IsDraft { get; set; }
        public bool IsRead { get; set; }
    }
}
