﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace PSRule.Rules.Azure.Configuration
{
    public sealed class ConfigurationOption : IEquatable<ConfigurationOption>
    {
        internal static readonly ConfigurationOption Default = new ConfigurationOption
        {
            Subscription = SubscriptionOption.Default,
            ResourceGroup = ResourceGroupOption.Default,
            Tenant = TenantOption.Default,
            ManagementGroup = ManagementGroupOption.Default,
        };

        public ConfigurationOption()
        {
            Subscription = null;
            ResourceGroup = null;
            Tenant = null;
            ManagementGroup = null;
        }

        internal ConfigurationOption(ConfigurationOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            Subscription = option.Subscription;
            ResourceGroup = option.ResourceGroup;
            Tenant = option.Tenant;
            ManagementGroup = option.ManagementGroup;
        }

        public override bool Equals(object obj)
        {
            return obj is ConfigurationOption option && Equals(option);
        }

        public bool Equals(ConfigurationOption other)
        {
            return other != null &&
                Subscription == other.Subscription &&
                ResourceGroup == other.ResourceGroup &&
                Tenant == other.Tenant &&
                ManagementGroup == other.ManagementGroup;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine
            {
                int hash = 17;
                hash = hash * 23 + (Subscription != null ? Subscription.GetHashCode() : 0);
                hash = hash * 23 + (ResourceGroup != null ? ResourceGroup.GetHashCode() : 0);
                hash = hash * 23 + (Tenant != null ? Tenant.GetHashCode() : 0);
                hash = hash * 23 + (ManagementGroup != null ? ManagementGroup.GetHashCode() : 0);
                return hash;
            }
        }

        internal static ConfigurationOption Combine(ConfigurationOption o1, ConfigurationOption o2)
        {
            var result = new ConfigurationOption
            {
                ResourceGroup = ResourceGroupOption.Combine(o1?.ResourceGroup, o2?.ResourceGroup),
                Subscription = SubscriptionOption.Combine(o1?.Subscription, o2?.Subscription),
                Tenant = TenantOption.Combine(o1?.Tenant, o2?.Tenant),
                ManagementGroup = ManagementGroupOption.Combine(o1?.ManagementGroup, o2?.ManagementGroup),
            };
            return result;
        }

        /// <summary>
        /// The file path location to save results.
        /// </summary>
        [DefaultValue(null)]
        [YamlMember(Alias = "AZURE_SUBSCRIPTION", ApplyNamingConventions = false)]
        public SubscriptionOption Subscription { get; set; }

        [DefaultValue(null)]
        [YamlMember(Alias = "AZURE_RESOURCE_GROUP", ApplyNamingConventions = false)]
        public ResourceGroupOption ResourceGroup { get; set; }

        [DefaultValue(null)]
        [YamlMember(Alias = "AZURE_TENANT", ApplyNamingConventions = false)]
        public TenantOption Tenant { get; set; }

        [DefaultValue(null)]
        [YamlMember(Alias = "AZURE_MANAGEMENT_GROUP", ApplyNamingConventions = false)]
        public ManagementGroupOption ManagementGroup { get; set; }
    }
}
