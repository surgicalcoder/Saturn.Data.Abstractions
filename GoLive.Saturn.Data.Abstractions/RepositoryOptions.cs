using System;
using System.Collections.Generic;

namespace GoLive.Saturn.Data.Abstractions
{
    public class RepositoryOptions
    {
        public RepositoryOptions()
        {
            GenericSerializers = new Dictionary<Type, Type>();
            DiscriminatorConventions = new Dictionary<Type, object>();
            Serializers = new Dictionary<Type, object>();
        }

        public string ConnectionStringName { get; set; }
        public Action<string, string> CommandStartedCallback { get; set; }
        public TimeSpan InitDuration { get; set; }

        public Action<IRepository> InitCallback { get; set; }

        public Func<string, string> CollectionNameOverride { get; set; }

        public Dictionary<Type, Type> GenericSerializers { get; set; }
        public Dictionary<Type, Object> DiscriminatorConventions { get; set; }
        public Dictionary<Type, Object> Serializers { get; set; }

    }
}