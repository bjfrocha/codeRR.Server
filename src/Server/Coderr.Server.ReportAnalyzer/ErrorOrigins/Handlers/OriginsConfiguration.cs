﻿using System.Collections.Generic;
using Coderr.Server.Abstractions.Config;

namespace Coderr.Server.ReportAnalyzer.ErrorOrigins.Handlers
{
    public class OriginsConfiguration : IConfigurationSection
    {
        /// <summary>
        ///     API key for ipstack
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     API key for LocationIQ.com
        /// </summary>
        public string LocationIqApiKey { get; set; }

        /// <summary>
        ///     API key for mapquest.com
        /// </summary>
        public string MapQuestApiKey { get; set; }

        string IConfigurationSection.SectionName { get; } = "Origins";

        IDictionary<string, string> IConfigurationSection.ToDictionary()
        {
            return this.ToConfigDictionary();
        }


        void IConfigurationSection.Load(IDictionary<string, string> settings)
        {
            this.AssignProperties(settings);
        }
    }
}