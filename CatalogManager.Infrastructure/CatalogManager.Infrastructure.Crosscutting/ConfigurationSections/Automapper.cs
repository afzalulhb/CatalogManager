using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting
{
    public class Automapper : ConfigurationSection
    {
        [ConfigurationProperty("assemblies", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AssemblyCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public AssemblyCollection Assemblies
        {
            get { return (AssemblyCollection)base["assemblies"]; }
        }
    }

    public class AssemblyCollection : ConfigurationElementCollection
    {
        public AssemblyElement this[int index]
        {
            get { return (AssemblyElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(AssemblyElement assemblyElement)
        {
            BaseAdd(assemblyElement);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AssemblyElement)element).Name;
        }

        public void Remove(AssemblyElement assemblyElement)
        {
            BaseRemove(assemblyElement.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }

    public class AssemblyElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}
