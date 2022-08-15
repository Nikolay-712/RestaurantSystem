namespace RestaurantSystem.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "RestaurantSystem";

        public const string AdministratorRoleName = "Administrator";

        public const string OwnerRoleName = "Owner";

        public const string RequiredFieldMessage = "Полето е задалжително.";

        public const string LenghtErrorMessage = "{0} трябва да бъде между {1} - {2} символа.";

        public const string PriceErrorMessage = "{0} трябва да бъде между {1}лв. - {2}лв.";

        public const string CategoryErrorMesage = "Категорията {0} не се подържа.";

        public const string WeightErrorMessage = "{0} трябва да бъде между {1} - {2} грама.";

        public const string CountErrorMessage = "{0} трябва да бъде между {1} - {2} души.";

        public static class Message
        {
            public const string UserSender = "User";
            public const string AdminSender = "Administration";

            public const string SuccessfullySentMessage = "Благодаря за вашето саобщение,ще се свържем с вас";
            public const string SuccessfullySentReservation = "Вашата резервация бевше изпратена успешно.След като бъде обработена ше получите потварждение";
            public const string CloseDiscussionMessage = "Дискусията е затворена,надявам се да сме били полезни";
            public const string АpproveOwnerMessage = "Изпращам инструкци за използване";
            public const string RefuseOwnerMessage = "Вие не сте одобрен";

            public const string ReservationNotificationMessage = "Статус - {0}";
            public const string ReservationPending = "Обработва се";
            public const string ReservationApproved = "Очакваме ви на {0}";
            public const string ReservationCanceled = "Отказана";

            public const string SentOrder = "Поръчка номер - #{0} е изпратена";
            public const string NewMessage = "Ново саобщение";
        }
    }
}
