using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OGBattleSim
{
    public static class ObjectCopier
    {
        public static  Tuple<T,T> DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                var o1 = (T)formatter.Deserialize(ms);
                ms.Position = 0;
                var o2 = (T)formatter.Deserialize(ms);

                return new Tuple<T, T>(o1, o2);
            }
        }
    }
}