﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using MigrationTools.Configuration;
using MigrationTools.DataContracts;
using MigrationTools.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationTools.Clients.AzureDevops.Rest
{
    public static class AzureDevOpsExtensions
    {

        public static WorkItemData ToWorkItemData(this WorkItem workItem)
        {
            var internalObject = new WorkItemData();
            internalObject.Id = workItem.Id.ToString();
            internalObject.Type = "unknown";
            internalObject.Title = "unknown";
            internalObject.internalObject = workItem;
            return internalObject;
        }

        public static WorkItem ToWorkItem(this WorkItemData workItemData)
        {
            if (!(workItemData.internalObject is WorkItem))
            {
                throw new InvalidCastException($"The Work Item stored in the inner field must be of type {(typeof(WorkItem)).FullName}");
            }
            return (WorkItem)workItemData.internalObject;
        }


    }
}
