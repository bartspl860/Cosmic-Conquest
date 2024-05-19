using System;
using UnityEngine;

namespace Http
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(fixJson(json));
            return wrapper.Items;
        }
        
        private static string fixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
        

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}