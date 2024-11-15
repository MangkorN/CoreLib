using UnityEngine;

namespace CoreLib.Utilities
{
    /// <summary>
    /// Read Only Conditional attribute.
    /// Attribute is used to grey out properties if condition is met. Input a FieldName as a string e.g. "_myBool".
    /// </summary>
    public class ReadOnlyConditionalAttribute : PropertyAttribute
    {
        public readonly string[] ConditionFieldNames;

        public ReadOnlyConditionalAttribute(params string[] conditionFieldNames)
        {
            ConditionFieldNames = conditionFieldNames;
        }
    }
}