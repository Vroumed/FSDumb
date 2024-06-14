using System;
using System.Collections;
using System.Reflection;
using Vroumed.FSDumb.Extensions;

namespace Vroumed.FSDumb.Dependencies
{
    
    [AttributeUsage(AttributeTargets.Property)]
    public class Resolved : Attribute
    {
    }
    
    public interface IDependencyCandidate
    {
    }

    public struct Dependency
    {
        public object DependencyObject { get; set; }
        public Type Type { get; set; }
    }
    public sealed class DependencyInjector
    {
        private ArrayList Dependencies { get; } = new ArrayList();

        /// <summary>
        /// Cache a dependency topmost type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependency"></param>
        /// <exception cref="Exception"></exception>
        public void Cache<T>(T dependency)
        {
            foreach (Dependency dep in Dependencies)
            {
                if (dep.Type == dependency.GetType())
                {
                    throw new Exception("Dependency already exists");
                }
            }

            Dependencies.Add(new Dependency
            {
                DependencyObject = dependency,
                Type = dependency.GetType()
            });
        }

        /// <summary>
        /// Caches a dependency as a specific type
        /// </summary>
        /// <typeparam name="T">Type to be registered in</typeparam>
        /// <param name="dependency">The dependency object to be cached</param>
        /// <exception cref="InvalidOperationException">When caching two same dependency types</exception>
        public void CacheAs<T>(T dependency)
        {
            foreach (Dependency dep in Dependencies)
            {
                if (dep.Type == typeof(T))
                {
                    throw new InvalidOperationException("Dependency already exists");
                }
            }

            Dependencies.Add(new Dependency
            {
                DependencyObject = dependency,
                Type = typeof(T)
            });
        }

        /// <summary>
        /// Resolve a <see cref="IDependencyCandidate"/>'s <see cref="Resolved"/> attributes
        /// </summary>
        /// <param name="candidate"></param>
        public void Resolve(IDependencyCandidate candidate)
        {
            //Get all the properties with the Resolved attribute
            FieldInfo[] properties = candidate.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (FieldInfo property in properties)
            {
                if (property.GetCustomAttributes(false).Any(s => s is Resolved))
                {
                    property.SetValue(candidate, Retrieve(property.FieldType));
                }
            }
        }

        private object Retrieve(Type type)
        {
            foreach (Dependency dep in Dependencies)
            {
                if (dep.Type == type)
                {
                    return dep;
                }
            }

            throw new Exception("Dependency not found");
        }

        public T Retrieve<T>()
        {
            foreach (Dependency dep in Dependencies)
            {
                if (dep.Type == typeof(T))
                {
                    return (T)dep.DependencyObject;
                }
            }

            throw new Exception($"dependency of type {typeof(T)} not found");
        }
        
        /// <summary>
        /// Create a <see cref="T"/>, inject it's constructors the necessary types, and fill all Resolved Attribtes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Resolve<T>() where T : IDependencyCandidate
        {
            //Get Constructor
            var constructors = typeof(T).GetConstructors();
            if (constructors.Length == 0)
            {
                throw new Exception("No constructors found");
            }
            if (constructors.Length > 1)
            {
                throw new Exception("Dependecies injection by creation not supported for multiple constructors");
            }

            ConstructorInfo constructor = constructors[0];

            //Get Parameters
            ParameterInfo[] parameters = constructor.GetParameters();

            //Create instances of parameters

            object[] instances = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var instance = Retrieve(parameter.ParameterType);
                instances[i] = instance;
            }

            T obj = (T)constructor.Invoke(instances);

            //get all the properties with the Resolved attribute
            FieldInfo[] properties = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (FieldInfo property in properties)
            {
                if (property.GetCustomAttributes(false).Any(s => s is Resolved))
                {
                    property.SetValue(obj, Retrieve(property.FieldType));
                }
            }

            return obj;
        }
    }
}
