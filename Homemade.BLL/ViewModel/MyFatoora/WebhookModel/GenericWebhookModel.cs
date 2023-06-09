﻿using Homemade.Enums;
using System;
namespace Homemade.BLL.ViewModel.MyFatoora.WebhookModel
{
    public class GenericWebhookModel<T>
    {
        public WebhookEvents EventType { get; set; }
        public string Event { get; set; }
        public string DateTime { get; set; }
        public string CountryIsoCode { get; set; }
        public T Data { get; set; }
    }
}
