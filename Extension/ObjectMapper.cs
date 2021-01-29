using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CloudDataManagement.Extension
{
    public static class ObjectMapper
    {
        public static IEnumerable<TTarget> MapList<TSource, TTarget>(this IEnumerable<TSource> sources, IEnumerable<TTarget> targets)
        {
            if (!sources.Any())
                return targets;

            var targetDic = new Dictionary<string, TTarget>();
            foreach (var t in targets)
            {
                var tProp = t.GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                   .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                if (tProp != null)
                {
                    var id = $"{tProp.GetValue(t, null)}";
                    targetDic.Add(id, t);
                }
            }

            foreach (var source in sources)
            {
                var sourceProp = source.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                    .Where(p => p.CanWrite && p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (sourceProp != null)
                {
                    var id = $"{sourceProp.GetValue(source, null)}";
                    if (targetDic.ContainsKey(id))
                    {
                        targetDic[id] = source.Map(targetDic[id]);
                    }
                    else
                    {
                        //logger.Debug($"The target object is not exist.");
                    }
                }
            }
            return targetDic.Values;
        }

        public static TTarget Map<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (source is null)
                return target;

            var targetProps = target.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);

            var sourceProps = source.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            foreach (var sourceProp in sourceProps)
            {
                try
                {
                    var attributes = sourceProp.GetCustomAttributes(typeof(IngorePropertyAttribute), true);
                    if (attributes.FirstOrDefault() != null) continue;

                    var targetProp = targetProps.FirstOrDefault(p => p.Name.Equals(sourceProp.Name) && p.PropertyType.Equals(sourceProp.PropertyType));
                    if (targetProp != null)
                    {
                        targetProp.SetValue(target, sourceProp.GetValue(source, null), null);
                    }
                    else
                    {
                        //_logger.Debug($"The property is not exist in tenant yaml file, property name: {sourceProp.Name}");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return target;
        }

        public static TTarget Map<TSource, TTarget>(this TSource source)
        {
            Type targetType = typeof(TTarget);
            var newTarget = Activator.CreateInstance(targetType);
            if (source is null)
                return default(TTarget);

            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);

            var sourceProps = source.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            foreach (var sourceProp in sourceProps)
            {
                try
                {
                    var attributes = sourceProp.GetCustomAttributes(typeof(IngorePropertyAttribute), true);
                    if (attributes.FirstOrDefault() != null) continue;

                    var targetProp = targetProps.FirstOrDefault(p => p.Name.Equals(sourceProp.Name) && p.PropertyType.Equals(sourceProp.PropertyType));
                    if (targetProp != null)
                    {
                        targetProp.SetValue(newTarget, sourceProp.GetValue(source, null), null);
                    }
                    else
                    {
                        //_logger.Debug($"The property is not exist in tenant yaml file, property name: {sourceProp.Name}");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return (TTarget)newTarget;
        }

        public static IEnumerable<TTarget> MapList<TSource, TTarget>(this IEnumerable<TSource> sources)
        {
            var targets = new List<TTarget>();
            Type targetType = typeof(TTarget);
            var targetPsrops = targetType.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);

            foreach (var source in sources)
            {
                var newTarget = Activator.CreateInstance(targetType);
                var sourceProps = source.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                    .Where(p => p.CanWrite);
                foreach (var tp in targetPsrops)
                {
                    var sourceProp = sourceProps.FirstOrDefault(sp => sp.Name.Equals(tp.Name) && sp.PropertyType.Equals(tp.PropertyType));
                    if (sourceProp != null)
                    {
                        tp.SetValue(newTarget, sourceProp.GetValue(source, null), null);
                    }
                }
                targets.Add((TTarget)newTarget);
            }
            return targets;
        }

        public static IEnumerable<TTarget> MapListV2<TSource, TTarget>(this IEnumerable<TSource> sources, IEnumerable<TTarget> targets)
        {
            if (!sources.Any())
                return targets;

            var targetDic = new Dictionary<string, TTarget>();
            foreach (var target in targets)
            {
                var targetProp = target.GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                   .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                if (targetProp != null)
                {
                    var id = $"{targetProp.GetValue(target, null)}";
                    targetDic.Add(id, target);
                }
            }

            var targetInstance = Activator.CreateInstance(typeof(TTarget));
            var defaultTarget = targetDic[targetDic.First().Key].Map(targetInstance);
            var sourceIds = new List<string>();

            foreach (var source in sources)
            {
                var sourceProp = source.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                    .FirstOrDefault(p => p.CanWrite && p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                if (sourceProp != null)
                {
                    var id = $"{sourceProp.GetValue(source, null)}";
                    sourceIds.Add(id);

                    if (targetDic.ContainsKey(id))
                    {
                        targetDic[id] = source.Map(targetDic[id]);
                    }
                    else
                    {
                        targetDic.Add(id, source.Map((TTarget)defaultTarget));
                    }
                }
            }
            foreach (var id in targetDic.Keys.Except(sourceIds))
            {
                targetDic.Remove(id);
            }
            return targetDic.Values;
        }

        public static IEnumerable<TTarget> MapListV2<TSource, TTarget>(this IEnumerable<TSource> sources)
        {
            var targets = new List<TTarget>();
            Type targetType = typeof(TTarget);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            foreach (var source in sources)
            {
                var newTarget = Activator.CreateInstance(targetType);
                var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance).Where(p => p.CanWrite);
                foreach (var tp in targetProps)
                {
                    var sourceProp = sourceProps.FirstOrDefault(sp => sp.Name.Equals(tp.Name));
                    if (sourceProp.PropertyType.Equals(tp.PropertyType))
                    {
                        tp.SetValue(newTarget, sourceProp.GetValue(source, null), null);
                    }
                    else 
                    {
                        if (tp.PropertyType.BaseType.Name.Equals("Object", StringComparison.OrdinalIgnoreCase))
                        {
                            var targetObj = Activator.CreateInstance(tp.PropertyType);
                            sourceProp.GetValue(source, null).MapV2(targetObj);
                            tp.SetValue(newTarget, targetObj, null);
                        }
                        else 
                        {
                            //Not Support the case property with same name but different type
                        }
                    }
                }
                targets.Add((TTarget)newTarget);
            }
            return targets;
        }

        public static TTarget MapV2<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (source is null)
                return target;

            var targetProps = target.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);

            var sourceProps = source.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            foreach (var targetProp in targetProps)
            {
                try
                {
                    var sourceProp = sourceProps.FirstOrDefault(sp => sp.Name.Equals(targetProp.Name));
                    if (sourceProp != null)
                    {
                        if (targetProp.PropertyType.Equals(sourceProp.PropertyType))
                        {
                            targetProp.SetValue(target, sourceProp.GetValue(source, null), null);
                        }
                        else
                        {
                            if (targetProp.PropertyType.BaseType.Name.Equals("Object", StringComparison.OrdinalIgnoreCase))
                            {
                                var targetObj = Activator.CreateInstance(targetProp.PropertyType);
                                targetObj = sourceProp.GetValue(source, null).MapV2(targetObj);
                                targetProp.SetValue(target, targetObj, null);
                            }
                            else 
                            {
                                //Not Support the case property with same name but different type
                            }
                        }
                    }
                    else
                    {
                        //_logger.Debug($"The property is not exist in tenant yaml file, property name: {sourceProp.Name}");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return target;
        }
    }

    public class IngorePropertyAttribute : Attribute
    {
    }
}
