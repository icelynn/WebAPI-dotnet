using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Models;

namespace WebAPI.Utility
{
    /// <summary>
    /// Contains some methods which serialize/deserialize the common data types between <c>System.String</c>.
    /// </summary>
    public class JsonParser
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonParser()
        {

        }

        /// <summary>
        /// Deserialize a string in to <c>AccountInfo</c>.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>Get a deserialized <code>AccountInfo</code>.</returns>
        public AccountInfo DeserializeAccountInfo(string json)
        {
            AccountInfo info = JsonConvert.DeserializeObject<AccountInfo>(json);
            return info;
        }

        /// <summary>
        /// Deserialize <code>System.Collections.Generic.Dictionary<string, string></code>
        /// </summary>
        /// <param name="inputJson"></param>
        /// <returns>Get a deserialized <code>Dictionary<string, string></code>.</returns>
        public Dictionary<string, string> DeserializeStrDict(string inputJson)
        {
            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputJson);
            return dic;
        }

        /// <summary>
        /// Deserialize <code>System.Collections.Generic.Dictionary<string, object></code>
        /// </summary>
        /// <param name="inputJson"></param>
        /// <returns>Get a deserialized <code>Dictionary<string, object></code>.</returns>
        public Dictionary<string, object> DeserializeDict(string inputJson)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputJson);
            return dic;
        }

        /// <summary>
        /// Serialize a <code>System.Collections.Generic.Dictionary<string, object>into string.</code>
        /// </summary>
        /// <param name="dic"></param>
        /// <returns>Get a string object of a <code>System.Collections.Generic.Dictionary<string, object>.</returns>
        public string SerializeDictionary(Dictionary<string, object> dic)
        {
            string output = JsonConvert.SerializeObject(dic, Formatting.Indented);
            return output;
        }

        /// <summary>
        /// Serialize a <code>System.Collections.Generic.Dictionary<string, string>into string.</code>
        /// </summary>
        /// <param name="dic"></param>
        /// <returns>Get a string object of a <code>System.Collections.Generic.Dictionary<string, string>.</returns>
        public string SerializeStrDictionary(Dictionary<string, string> dic)
        {
            string output = JsonConvert.SerializeObject(dic, Formatting.Indented);
            return output;
        }
    }
}