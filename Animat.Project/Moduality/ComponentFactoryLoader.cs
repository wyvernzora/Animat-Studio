using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog.Filters;

namespace Animat.Project.Moduality
{
    /// <summary>
    /// Modular component loader.
    /// </summary>
    /// <typeparam name="TFactory">Type of the component to load.</typeparam>
    public class ComponentFactoryLoader<T>
    {
        protected const String DefaultAuthor = "<UNKNOWN>";
        protected const String DefaultVersion = "<UNKNOWN>";
        protected const String DefaultFilter = "Undefined Component (*.*)|*.*";

        #region Nested Data

        public class FactoryMetadata
        {
            public String Author { get; set; }

            public String Version { get; set; }

            public String Filter { get; set; }

            public IComponentFactory<T> Factory { get; set; }

            public String Name
            { get { return Factory.GetType().FullName; } }
        }

        #endregion

        private Dictionary<String, FactoryMetadata> loadedFactories;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="root">Root directory for all external assemblies.</param>
        /// <param name="external">Paths to external assemblies to load (relative to root).</param>
        public ComponentFactoryLoader()
        {
            loadedFactories = new Dictionary<String, FactoryMetadata>();
            LoadAssembly(Assembly.GetExecutingAssembly());
        }


        public IComponentFactory<T> this[String loaderName]
        {
            get
            {
                return loadedFactories[loaderName].Factory;
            }
        }


        public IEnumerable<FactoryMetadata> Factories
        { get { return loadedFactories.Values; } }

        protected void LoadAssembly(Assembly assembly)
        {
            try
            {
                var types = assembly.GetTypes();
                var factories = new List<FactoryMetadata>();

                foreach (var t in types)
                {
                    var factoryAttribute = t.GetCustomAttribute<ComponentFactoryAttribute>();
                    if (factoryAttribute != null && factoryAttribute.Type == typeof(T))
                    {
                        var metadata = new FactoryMetadata();

                        // Get filter
                        var filterAttribute = t.GetCustomAttribute<ComponentFileFilterAttribute>();
                        if (filterAttribute != null)
                            metadata.Filter = filterAttribute.Filter;
                        else
                            metadata.Filter = DefaultFilter;

                        // Get Info
                        var infoAttribute = t.GetCustomAttribute<ComponentInfoAttribute>();
                        if (infoAttribute != null)
                        {
                            metadata.Author = infoAttribute.Author;
                            metadata.Version = infoAttribute.Version;
                        }
                        else
                        {
                            metadata.Author = DefaultAuthor;
                            metadata.Version = DefaultVersion;
                        }

                        // Create Instance
                        metadata.Factory = (IComponentFactory<T>) Activator.CreateInstance(t);
                        factories.Add(metadata);
                    }
                }

                foreach (var f in factories)
                    loadedFactories.Add(f.Factory.GetType().FullName, f);
            }
            catch (Exception x)
            {
                throw;
            }

        }

    }
}
