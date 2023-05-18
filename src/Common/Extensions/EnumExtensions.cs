using System;
using System.ComponentModel;

namespace DocRouter.Common.Extensions
{
    /// <summary>
    /// Class that contains extension methods for Enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Attempts to retrieve the description of an Enum from the Description attribute.
        /// </summary>
        /// <param name="enumValue">An enumeration value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if the enum value or description cannot be found.</exception>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }
        /// <summary>
        /// Attempts to retrieve the Enum value from an Enum description.
        /// </summary>
        /// <typeparam name="T">An enumeration.</typeparam>
        /// <param name="description">A string enum description.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if no enum is found with the provided descrption.</exception>
        public static T GetEnumValueByDescription<T>(this string description) where T : Enum
        {
            foreach (Enum enumItem in Enum.GetValues(typeof(T)))
            {
                if (enumItem.GetEnumDescription() == description)
                {
                    return (T)enumItem;
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
    }    
}
