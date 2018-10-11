﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF
{
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }
        public IntegrationEventLogEntry(IntegrationEvent @event)
        {
            EventId = @event.Id;            
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
        }
        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }

        public void DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
        }
    }
}
