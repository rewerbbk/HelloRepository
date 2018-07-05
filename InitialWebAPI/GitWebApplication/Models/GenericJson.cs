using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;

namespace Models
{
    public class GenericJson
    {
        public Dictionary<string, string> Questions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, GenericJson> Members = new Dictionary<string, GenericJson>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, List<GenericJson>> MemberArrays = new Dictionary<string, List<GenericJson>>(StringComparer.OrdinalIgnoreCase);

        public GenericJson()
        {
            Questions = new Dictionary<string, string>();
            Members = new Dictionary<string, GenericJson>();
            MemberArrays = new Dictionary<string, List<GenericJson>>();
        }

        public GenericJson(string jsonString)
        {
            dynamic jsonObj = Json.Decode(jsonString);
            Initialize(jsonObj);
        }

        public GenericJson(dynamic jsonObj)
        {
            Initialize(jsonObj);
        }

        private void Initialize(dynamic jsonObj)
        {
            foreach (KeyValuePair<string, object> child in jsonObj)
            {
                if (child.Value is string)
                {
                    string value = child.Value.ToString();

                    if (value.Equals("True", StringComparison.CurrentCultureIgnoreCase))
                        Questions.Add(child.Key, "true");
                    else if (value.Equals("False", StringComparison.CurrentCultureIgnoreCase))
                        Questions.Add(child.Key, "false");
                    else
                        Questions.Add(child.Key, child.Value.ToString());
                }
                else if (child.Value is int || child.Value is double || child.Value is float)
                {
                    Questions.Add(child.Key, child.Value.ToString());
                }
                else if (child.Value is bool)
                {
                    Questions.Add(child.Key, ((bool)child.Value).ToString().ToLower());
                }
                else if (child.Value is DynamicJsonObject)
                {
                    Members.Add(child.Key, new GenericJson(child.Value));
                }
                else if (child.Value is DynamicJsonArray)
                {
                    List<GenericJson> childArray = new List<GenericJson>();
                    foreach (DynamicJsonObject arrayValue in (DynamicJsonArray)child.Value)
                    {
                        childArray.Add(new GenericJson(arrayValue));
                    }
                    MemberArrays.Add(child.Key, childArray);
                }
            }
        }

        public dynamic this[string key]
        {
            get
            {
                if (Questions.ContainsKey(key))
                    return Questions[key];
                else if (Members.ContainsKey(key))
                    return Members[key];
                else if (MemberArrays.ContainsKey(key))
                    return MemberArrays[key];

                return null;
            }
            set
            {
                if (Questions.ContainsKey(key))
                    if (value is string)
                    {
                        if (value.Equals("True", StringComparison.CurrentCultureIgnoreCase))
                            Questions[key] = "true";
                        else if (value.Equals("False", StringComparison.CurrentCultureIgnoreCase))
                            Questions[key] = "false";
                        else
                            Questions[key] = value;
                    }
                    else if (value is bool)
                        Questions[key] = ((bool)value).ToString().ToLower();
                    else
                        Questions[key] = value;
                else if (Members.ContainsKey(key))
                    Members[key] = value;
                else if (MemberArrays.ContainsKey(key))
                    MemberArrays[key] = value;
                else if (value is string)
                {
                    if (value.Equals("True", StringComparison.CurrentCultureIgnoreCase))
                        Questions.Add(key, "true");
                    else if (value.Equals("False", StringComparison.CurrentCultureIgnoreCase))
                        Questions.Add(key, "false");
                    else
                        Questions.Add(key, value);
                }
                else if (value is int)
                    Questions.Add(key, ((int)value).ToString());
                else if (value is double)
                    Questions.Add(key, ((double)value).ToString());
                else if (value is float)
                    Questions.Add(key, ((float)value).ToString());
                else if (value is bool)
                    Questions.Add(key, ((bool)value).ToString().ToLower());
                else if (value is GenericJson)
                    Members.Add(key, value);
                else if (value is List<GenericJson>)
                    MemberArrays.Add(key, value);
            }
        }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            response.Append("{");

            bool writeComma = false;
            foreach (KeyValuePair<string, string> question in Questions)
            {
                if (writeComma)
                    response.Append(",");

                response.Append("\"" + question.Key + "\":\"" + question.Value + "\"");
                writeComma = true;
            }

            if (Members.Count > 0)
            {
                foreach (KeyValuePair<string, GenericJson> child in Members)
                {
                    if (writeComma)
                        response.Append(",");

                    response.Append("\"" + child.Key + "\":");
                    response.Append(child.Value.ToString());
                    writeComma = true;
                }
            }

            if (MemberArrays.Count > 0)
            {
                foreach (KeyValuePair<string, List<GenericJson>> child in MemberArrays)
                {
                    if (writeComma)
                        response.Append(",");

                    response.Append("\"" + child.Key + "\":[");

                    writeComma = false;
                    foreach (GenericJson gChild in child.Value)
                    {
                        if (writeComma)
                            response.Append(",");
                        response.Append(gChild.ToString());
                        writeComma = true;
                    }

                    response.Append("]");
                    writeComma = true;
                }
            }

            response.Append("}");

            return response.ToString();
        }
    }

    public static class GenericJsonExtensions
    {
        public static bool Any<GenericJson>(this IEnumerable<GenericJson> source, Func<GenericJson, bool> predicate)
        {
            foreach (GenericJson item in source)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }
        public static bool All<GenericJson>(this IEnumerable<GenericJson> source, Func<GenericJson, bool> predicate)
        {
            foreach (GenericJson item in source)
            {
                if (!predicate(item))
                    return false;
            }
            return true;
        }
    }
}
