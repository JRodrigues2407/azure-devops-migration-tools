﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Diagnostics;
using MigrationTools.Configuration.FieldMap;
using MigrationTools.Configuration;
using Microsoft.Extensions.Logging;

namespace MigrationTools.Clients.AzureDevops.ObjectModel.FieldMaps
{
    public class FieldToFieldMap : FieldMapBase
    {

        public FieldToFieldMap(ILogger<FieldToFieldMap> logger) : base(logger)
        {

        }
        private FieldtoFieldMapConfig Config { get { return (FieldtoFieldMapConfig)_Config; } }

        public override void Configure(IFieldMapConfig config)
        {
            base.Configure(config);

        }

        public override string MappingDisplayName => $"{Config.sourceField} {Config.targetField}";

        internal override void InternalExecute(WorkItem source, WorkItem target)
        {
            if (source.Fields.Contains(Config.sourceField) && target.Fields.Contains(Config.targetField))
            {
                var value = source.Fields[Config.sourceField].Value;
                if ((value as string is null || value as string == "") && Config.defaultValue != null)
                {
                    value = Config.defaultValue;
                }
                target.Fields[Config.targetField].Value = value;
                Trace.WriteLine(string.Format("  [UPDATE] field mapped {0}:{1} to {2}:{3}", source.Id, Config.sourceField, target.Id, Config.targetField));
            }
        }
    }
}
