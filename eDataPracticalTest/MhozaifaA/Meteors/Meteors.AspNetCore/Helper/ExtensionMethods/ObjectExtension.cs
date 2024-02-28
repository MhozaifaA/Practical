using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meteors
{
    public static class ObjectExtension
    {
        public static T CastTo<T>(this object value)
            => (T)value;

        public static T AsTo<T>(this object value) where T : class
          => value as T;


        public static TGetterResult GetNValueNormalReflection<TObject, TGetterResult>(string propertyName)
        => typeof(TObject).GetProperty(propertyName).GetValue(typeof(TObject), null).CastTo<TGetterResult>();

        public static TCast GetValueNormalReflection<TObject, TCast>(this TObject obj, string propertyName)
        => obj.GetType().GetProperty(propertyName).GetValue(obj, null).CastTo<TCast>();

        public static bool TryGetValueNormalReflection<TObject>(this TObject obj, string propertyName, out object value)
        {
            value = default;
            PropertyInfo prop = obj.GetType().GetProperty(propertyName);
            if (prop is null) return false;
            value = prop.GetValue(obj);
            return true;
        }

        public static bool TryGetValueNormalReflection<TObject, TCast>(this TObject obj, string propertyName, out TCast value)
        {
            value = default;
            PropertyInfo prop = obj.GetType().GetProperty(propertyName);
            if (prop is null) return false;
            value = prop.GetValue(obj).CastTo<TCast>();
            return true;
        }


        public static TCast GetValueReflection<TObject, TCast>(this TObject obj, string propertyName)
          => ((Func<TObject, TCast>)Delegate.CreateDelegate(typeof(Func<TObject, TCast>), null, typeof(TObject).GetProperty(propertyName).GetGetMethod()))(obj).CastTo<TCast>();


        public static TGetterResult GetValueReflection<TObject, TGetterResult>(string propertyName)
        => ((Func<TGetterResult>)Delegate.CreateDelegate(typeof(Func<TGetterResult>), null, typeof(TObject).GetProperty(propertyName).GetGetMethod()))().CastTo<TGetterResult>();


        public static bool HasProperty<TObject>(this TObject value, string propertyName)
        => value.GetType().GetProperty(propertyName) is not null;

        public static bool HasProperty<TObject>(string propertyName)
        => typeof(TObject).GetProperty(propertyName) is not null;


        public static List<Type> AbleNameSpaceToType<T>(this T[] any)
         => any.Aggregate(new List<Type>(), (all, next) => { all.AddRange(getOverWriteNext(next)); return all; });


        /// <summary>
        /// Choice cast wise to implement <see langword="namespace"/> s
        /// </summary>
        private static Func<object, Type[]> getOverWriteNext = (next) => next switch
        {
            string s => Assembly.Load(s).GetTypes(),
            Type t => t.Assembly.GetTypes(),
            Assembly a => a.GetTypes(),
            _ => default
        };
    }
}

