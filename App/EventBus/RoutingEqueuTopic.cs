using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    public class RoutingEqueuTopic
    {
        public readonly string NameExchange;
        public string QueryEnqueu => $"{NameExchange}";
        public string ResultEnqueu => $"{NameExchange}";

        public string QueryRoutingKey => "query";
        public string ResultRoutingKey => "result";
        public string CancelRoutingKey => "cancel";
        public RoutingEqueuTopic(string nameExchange)
        {
            NameExchange = nameExchange;
        }
    }
}
