using System;
using System.Reflection;

namespace CasinoTests.Infrastructure
{
    internal static class ReflectionUtility
    {
        private const BindingFlags InstanceMembers =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void SetField<T>(object target, string fieldName, T value)
        {
            FieldInfo field = target.GetType().GetField(fieldName, InstanceMembers);
            if (field == null)
                throw new MissingFieldException(target.GetType().FullName, fieldName);

            field.SetValue(target, value);
        }

        public static T GetField<T>(object target, string fieldName)
        {
            FieldInfo field = target.GetType().GetField(fieldName, InstanceMembers);
            if (field == null)
                throw new MissingFieldException(target.GetType().FullName, fieldName);

            return (T)field.GetValue(target);
        }

        public static object Invoke(object target, string methodName, params object[] arguments)
        {
            MethodInfo method = target.GetType().GetMethod(methodName, InstanceMembers);
            if (method == null)
                throw new MissingMethodException(target.GetType().FullName, methodName);

            try
            {
                return method.Invoke(target, arguments);
            }
            catch (TargetInvocationException exception) when (exception.InnerException != null)
            {
                throw exception.InnerException;
            }
        }
    }
}
