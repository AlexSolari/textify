﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Data.Models;

namespace Textify.Core.Dependencies
{
    public static class Resolver
    {
        static Dictionary<Type, object> instanceDependencies = new Dictionary<Type, object>();
        static Dictionary<Type, Type> typeDependencies = new Dictionary<Type, Type>();

        #region Types
        public static Type Resolve<T>()
        {
            if (typeDependencies.ContainsKey(typeof(T)))
                return typeDependencies[typeof(T)];
            else
                return typeof(T);
        }

        public static Type Resolve(Type type)
        {
            if (typeDependencies.ContainsKey(type))
                return typeDependencies[type];
            else
                return type;
        }

        /// <summary>
        ///     Register type and realisation for DI.
        /// </summary>
        /// <param name="registerCollections">If true, also registers IList, List, IEnumerable and ICollection</param>
        /// <typeparam name="T">What will be requested</typeparam>
        /// <typeparam name="I">What will be returned</typeparam>
        public static void RegisterType<T, I>(bool registerCollections = true)
            where I : T
        {
            typeDependencies.Add(typeof(T), typeof(I));
            if (registerCollections)
            {
                typeDependencies.Add(typeof(IList<T>), typeof(List<I>));
                typeDependencies.Add(typeof(List<T>), typeof(List<I>));
                typeDependencies.Add(typeof(IEnumerable<T>), typeof(List<I>));
                typeDependencies.Add(typeof(ICollection<T>), typeof(List<I>));
            }
        }

        /// <summary>
        ///     Resolve dependency and return new instance of registered type.
        /// </summary>
        /// <typeparam name="T">What will be returned</typeparam>
        public static T GetInstance<T>(params object[] args)
        {
            var resolved = Resolve<T>();
            object result;

            if (instanceDependencies.ContainsKey(typeof(T)))
            {
                result = instanceDependencies[typeof(T)];
            }
            else
            {
                result = Activator.CreateInstance(resolved, args);
            }

            if (result is Entity)
            {
                var entity = result as Entity;
                entity.Id = Guid.NewGuid();
            }

            return (T)result;
        }

        /// <summary>
        ///     Resolve dependency and return new instance of registered type.
        /// </summary>
        /// <param name="reflectedType">Type that will be returned. Notice, that result will be boxed.</param>
        public static object GetInstance(Type type, params object[] args)
        {
            var resolved = Resolve(type);
            object result;

            if (resolved == null && instanceDependencies.ContainsKey(type))
            {
                result = instanceDependencies[type];
            }
            else
            {
                result = Activator.CreateInstance(resolved, args);
            }

            if (result is Entity)
            {
                var entity = result as Entity;
                entity.Id = Guid.NewGuid();
            }

            return result;
        }

        /// <summary>
        ///     Register instance of type to be used as singleton.
        /// </summary>
        /// <typeparam name="T">What will be returned</typeparam>
        public static void RegisterInstance<T>(T instance)
        {
            instanceDependencies[typeof(T)] = instance;
        }

        /// <summary>
        ///     Resolve dependency and return new instance of registered type.
        /// </summary>
        /// <remarks>If requested type is not registered, forces creation of instance anyway</remarks>
        /// <param name="reflectedType">Type that will be returned. Notice, that result will be boxed.</param>
        public static object GetInstanceForced(Type reflectedType, params object[] args)
        {
            var resolved = Resolve(reflectedType);
            object result = null;

            if (resolved == null)
            {
                if (instanceDependencies.ContainsKey(reflectedType))
                    result = instanceDependencies[reflectedType];
                else
                    result = Activator.CreateInstance(reflectedType, args);
            }
            else
            {
                result = Activator.CreateInstance(resolved, args);
            }

            if (result is Entity)
            {
                var entity = result as Entity;
                entity.Id = Guid.NewGuid();
            }

            return result;
        }
        #endregion
    }
}
