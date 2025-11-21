using static MudBlazor.FilterOperator;
using System.Collections.Generic;
using System;
using MudBlazor;
using SaleAPI.Extensions;
using QLSX.Shared.Models;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FoolProof.Core;
using System.Linq.Expressions;
using Guid = System.Guid;
using DateTime = System.DateTime;
using Enum = System.Enum;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;

namespace Sale.API.Extensions
{
    public class FilterBuider<T>
    {

        public FilterBuider(FilterDefinition<T> filter1)
        {
            filter = filter1 ?? throw new ArgumentNullException(nameof(filter1));
        }
        public string Operator { get; set; }
        public object Value { get; set; }
        private Type DataType { get; set; }
        public string Field { get; set; }
        public Expression<Func<T, bool>> GetFilter
        {
            get
            {
                return GenerateFilterFunction();
            }
        }
        private FilterDefinition<T> filter { get; set; }
        public Expression<Func<T, bool>> GenerateFilterFunction()
        {
            DataType = typeof(T).GetProperty(filter.Field).PropertyType;
            Operator = filter.Operator;
            Field = filter.Field;
            Value = filter.Value;
            // Handle case where we have an IDictionary.
            if (typeof(T) == typeof(IDictionary<string, object>))
            {
                //if (IsNumber(DataType))
                //{
                //    return GenerateFilterForNumericTypesInIDictionary();
                //}
                //else if (IsEnum(DataType))
                //{
                //    return GenerateFilterForEnumTypesInIDictionary();
                //}
                //else if (IsBoolean(DataType))
                //{
                //    return GenerateFilterForBooleanTypeInIDictionary();
                //}
                //else if (IsDateTime(DataType))
                //{
                //    return GenerateFilterForDateTimeTypeInIDictionary();
                //}
                //else if (IsGuid(DataType))
                //{
                //    return GenerateFilterForGuidTypeInIDictionary();
                //}
                //else
                //{
                //    return GenerateFilterForStringTypeInIDictionary();
                //}

                return x => true;
            }
            else
            {
                var expression = GenerateFilterExpression();

                try
                {
                    var item = expression;
                    return item;
                }
                catch (Exception ex)
                {

                    throw;
                }



            }
        }

        public Expression<Func<T, bool>> GenerateFilterExpression()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression expression;

            if (DataType == typeof(string))
            {
                expression = GenerateFilterExpressionForStringType(parameter);
            }
            else if (IsNumber(DataType))
            {
                expression = GenerateFilterExpressionForNumericTypes(parameter);
            }
            else if (IsEnum(DataType))
            {
                expression = GenerateFilterExpressionForEnumTypes(parameter);
            }
            else if (IsBoolean(DataType))
            {
                expression = GenerateFilterExpressionForBooleanTypes(parameter);
            }
            else if (IsDateTime(DataType))
            {
                expression = GenerateFilterExpressionForDateTimeTypes(parameter);
            }
            else if (IsGuid(DataType))
            {
                expression = GenerateFilterExpressionForGuidTypes(parameter);
            }
            else
            {
                expression = Expression.Constant(true, typeof(bool));
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }


        private Expression GenerateFilterExpressionForDateTimeTypes(ParameterExpression parameter)
        {
            var field = Expression.Convert(Expression.Property(parameter, typeof(T).GetProperty(Field)), typeof(DateTime?));
            DateTime? valueDateTime = Value == null ? null : ((DateTime)Value).Date;
            var isnotnull = Expression.NotEqual(field, Expression.Constant(null));
            var isnull = Expression.Equal(field, Expression.Constant(null));
            var notNullDateTime = Expression.Convert(field, typeof(DateTime));
            var valueDateTimeConstant = Expression.Constant(Convert.ToDateTime(Convert.ToString(valueDateTime?.ToString("yyyy/MM/dd"))), typeof(DateTime));

            return Operator switch
            {
                FilterOperator.DateTime.Is when null != Value =>
                    Expression.AndAlso(isnotnull,
                        Expression.Equal(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.IsNot when null != Value =>
                    Expression.OrElse(isnull,
                        Expression.NotEqual(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.After when null != Value =>
                    Expression.AndAlso(isnotnull,
                        Expression.GreaterThan(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.OnOrAfter when null != Value =>
                    Expression.AndAlso(isnotnull,
                        Expression.GreaterThanOrEqual(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.Before when null != Value =>
                    Expression.AndAlso(isnotnull,
                        Expression.LessThan(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.OnOrBefore when null != Value =>
                    Expression.AndAlso(isnotnull,
                        Expression.LessThanOrEqual(notNullDateTime, valueDateTimeConstant)),

                FilterOperator.DateTime.Empty => isnull,
                FilterOperator.DateTime.NotEmpty => isnotnull,

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Expression GenerateFilterExpressionForBooleanTypes(ParameterExpression parameter)
        {
            var field = Expression.Convert(Expression.Property(parameter, typeof(T).GetProperty(Field)), typeof(bool?));
            bool? valueBool = Value == null ? null : Convert.ToBoolean(Value);
            var isnotnull = Expression.NotEqual(field, Expression.Constant(null));
            var notNullBool = Expression.Convert(field, typeof(bool));

            return Operator switch
            {
                FilterOperator.Enum.Is when Value != null => Expression.AndAlso(isnotnull,
                    Expression.Equal(notNullBool, Expression.Constant(valueBool))),

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Expression GenerateFilterExpressionForGuidTypes(ParameterExpression parameter)
        {
            var field = Expression.Convert(Expression.Property(parameter, typeof(T).GetProperty(Field)), typeof(Guid?));
            Guid? valueGuid = Value == null ? null : ParseGuid((System.String)Value);
            var isnotnull = Expression.IsTrue(Expression.Property(field, typeof(Guid?), "HasValue"));
            var isnull = Expression.IsFalse(Expression.Property(field, typeof(Guid?), "HasValue"));
            var notNullGuid = Expression.Convert(field, typeof(Guid));

            return Operator switch
            {
                FilterOperator.Guid.Equal when valueGuid != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Equal(notNullGuid, Expression.Constant(valueGuid))),

                FilterOperator.Guid.NotEqual when valueGuid != null =>
                    Expression.OrElse(
                        isnull,
                        Expression.NotEqual(notNullGuid, Expression.Constant(valueGuid))),

                // filtered value is not a valid GUID
                _ when valueGuid == null && Value != null =>
                    Expression.Constant(false),

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Expression GenerateFilterExpressionForEnumTypes(ParameterExpression parameter)
        {
            var field = Expression.Convert(Expression.Property(parameter, typeof(T).GetProperty(Field)), DataType);
            var valueEnum = Value == null ? null : (Enum)Value;
            var _null = Expression.Convert(Expression.Constant(null), DataType);
            var isnull = Expression.Equal(field, _null);
            var isnotnull = Expression.NotEqual(field, _null);
            var valueEnumConstant = Expression.Convert(Expression.Constant(valueEnum), DataType);

            return Operator switch
            {
                FilterOperator.Enum.Is when Value != null =>
                    IsNullableEnum(DataType) ? Expression.AndAlso(isnotnull,
                            Expression.Equal(field, valueEnumConstant))
                        : Expression.Equal(field, valueEnumConstant),

                FilterOperator.Enum.IsNot when Value != null =>
                    IsNullableEnum(DataType) ? Expression.OrElse(isnull,
                            Expression.NotEqual(field, valueEnumConstant))
                        : Expression.NotEqual(field, valueEnumConstant),

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Expression GenerateFilterExpressionForNumericTypes(ParameterExpression parameter)
        {
            var field = Expression.Convert(Expression.Property(parameter, typeof(T).GetProperty(Field)), typeof(double?));
            double? valueNumber = Value == null ? null : Convert.ToDouble(Value);
            var isnotnull = Expression.NotEqual(field, Expression.Constant(null));
            var isnull = Expression.Equal(field, Expression.Constant(null));
            var notNullNumber = Expression.Convert(field, typeof(double));
            var valueNumberConstant = Expression.Constant(valueNumber);

            return Operator switch
            {
                FilterOperator.Number.Equal when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Equal(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.NotEqual when Value != null =>
                    Expression.OrElse(isnull,
                        Expression.NotEqual(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.GreaterThan when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.GreaterThan(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.GreaterThanOrEqual when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.GreaterThanOrEqual(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.LessThan when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.LessThan(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.LessThanOrEqual when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.LessThanOrEqual(notNullNumber, valueNumberConstant)),

                FilterOperator.Number.Empty => isnull,
                FilterOperator.Number.NotEmpty => isnotnull,

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Expression GenerateFilterExpressionForStringType(ParameterExpression parameter)
        {
            var field = Expression.Property(parameter, typeof(T).GetProperty(Field));
            var valueString = Value?.ToString();
            var trim = Expression.Call(field, DataType.GetMethod("Trim", Type.EmptyTypes));
            var isnull = Expression.Equal(field, Expression.Constant(null));
            var isnotnull = Expression.NotEqual(field, Expression.Constant(null));
            return Operator switch
            {
                FilterOperator.String.Contains when Value != null =>
                    Expression.AndAlso(
                            isnotnull,
                            Expression.Call(field, DataType.GetMethod("Contains", new[] { DataType }),
                            Expression.Constant(valueString))),

                FilterOperator.String.Contains when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("Contains", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) })),

                FilterOperator.String.NotContains when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Not(Expression.Call(field, DataType.GetMethod("Contains", new[] { DataType }), Expression.Constant(valueString)))),

                FilterOperator.String.NotContains when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Not(Expression.Call(field, DataType.GetMethod("Contains", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) }))),

                FilterOperator.String.Equal when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Equal(field, Expression.Constant(valueString))),

                FilterOperator.String.Equal when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("Equals", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) })),

                FilterOperator.String.NotEqual when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Not(Expression.Equal(field, Expression.Constant(valueString)))),

                FilterOperator.String.NotEqual when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Not(Expression.Call(field, DataType.GetMethod("Equals", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) }))),

                FilterOperator.String.StartsWith when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("StartsWith", new[] { DataType }), Expression.Constant(valueString))),

                FilterOperator.String.StartsWith when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("StartsWith", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) })),

                FilterOperator.String.EndsWith when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("EndsWith", new[] { DataType }), Expression.Constant(valueString))),

                FilterOperator.String.EndsWith when Value != null =>
                    Expression.AndAlso(isnotnull,
                        Expression.Call(field, DataType.GetMethod("EndsWith", new[] { DataType, typeof(StringComparison) }), new[] { Expression.Constant(valueString), Expression.Constant(StringComparison.OrdinalIgnoreCase) })),

                FilterOperator.String.Empty =>
                    Expression.OrElse(isnull,
                        Expression.Equal(trim, Expression.Constant(string.Empty, DataType))),

                FilterOperator.String.NotEmpty =>
                    Expression.AndAlso(isnotnull,
                        Expression.NotEqual(trim, Expression.Constant(string.Empty, DataType))),

                _ => Expression.Constant(true, typeof(bool))
            };
        }

        private Func<T, bool> GenerateFilterForStringTypeInIDictionary()
        {
            var valueString = Value?.ToString();

            var caseSensitivity = StringComparison.OrdinalIgnoreCase;

            return Operator switch
            {
                FilterOperator.String.Contains when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null && v.Contains(valueString, caseSensitivity);
                }
                ,
                FilterOperator.String.NotContains when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null && !v.Contains(valueString, caseSensitivity);
                }
                ,

                FilterOperator.String.Equal when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null && v.Equals(valueString, caseSensitivity);
                }
                ,

                FilterOperator.String.NotEqual when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return !valueString.Equals(v, caseSensitivity);
                }
                ,

                FilterOperator.String.StartsWith when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null && v.StartsWith(valueString, caseSensitivity);
                }
                ,

                FilterOperator.String.EndsWith when Value != null => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null && v.EndsWith(valueString, caseSensitivity);
                }
                ,

                FilterOperator.String.Empty => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return string.IsNullOrWhiteSpace(v);
                }
                ,

                FilterOperator.String.NotEmpty => x =>
                {
                    string v = GetStringFromObject(((IDictionary<string, object>)x)[Field]);

                    return !string.IsNullOrWhiteSpace(v);
                }
                ,

                _ => x => true
            };
        }
        private Func<T, bool> GenerateFilterForNumericTypesInIDictionary()
        {
            double? valueNumber = Value == null ? null : Convert.ToDouble(Value);

            return Operator switch
            {
                FilterOperator.Number.Equal when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v == valueNumber;
                }
                ,

                FilterOperator.Number.NotEqual when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != valueNumber;
                }
                ,

                FilterOperator.Number.GreaterThan when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v > valueNumber;
                }
                ,

                FilterOperator.Number.GreaterThanOrEqual when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v >= valueNumber;
                }
                ,

                FilterOperator.Number.LessThan when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v < valueNumber;
                }
                ,

                FilterOperator.Number.LessThanOrEqual when Value != null => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v <= valueNumber;
                }
                ,

                FilterOperator.Number.Empty => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v == null;
                }
                ,

                FilterOperator.Number.NotEmpty => x =>
                {
                    double? v = GetDoubleFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null;
                }
                ,

                _ => x => true
            };
        }
        private Func<T, bool> GenerateFilterForEnumTypesInIDictionary()
        {
            return Operator switch
            {
                FilterOperator.Enum.Is when Value != null => x =>
                {
                    var v = GetEnumFromObject(((IDictionary<string, object>)x)[Field]);

                    return object.Equals(v, Value);
                }
                ,

                FilterOperator.Enum.IsNot when Value != null => x =>
                {
                    var v = GetEnumFromObject(((IDictionary<string, object>)x)[Field]);

                    return !object.Equals(v, Value);
                }
                ,

                _ => x => true
            };
        }
        private Func<T, bool> GenerateFilterForBooleanTypeInIDictionary()
        {
            return Operator switch
            {
                FilterOperator.Enum.Is when Value != null => x =>
                {
                    var v = GetBoolFromObject(((IDictionary<string, object>)x)[Field]);

                    return object.Equals(v, Value);
                }
                ,

                _ => x => true
            };
        }
        private Func<T, bool> GenerateFilterForGuidTypeInIDictionary()
        {
            Guid? valueGuid = Value == null ? null : ParseGuid((string)Value);
            return Operator switch
            {
                FilterOperator.Guid.Equal when Value != null => x =>
                {
                    var v = GetGuidFromObject(((IDictionary<string, object>)x)[Field]);

                    return v == valueGuid;
                }
                ,
                FilterOperator.Guid.NotEqual when Value != null => x =>
                {
                    var v = GetGuidFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != valueGuid;
                }
                ,

                _ => x => true
            };
        }
        private Func<T, bool> GenerateFilterForDateTimeTypeInIDictionary()
        {
            DateTime? valueDateTime = Value == null ? null : (DateTime)Value;

            return Operator switch
            {
                FilterOperator.DateTime.Is when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v == valueDateTime;
                }
                ,

                FilterOperator.DateTime.IsNot when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != valueDateTime;
                }
                ,

                FilterOperator.DateTime.After when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v > valueDateTime;
                }
                ,

                FilterOperator.DateTime.OnOrAfter when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v >= valueDateTime;
                }
                ,

                FilterOperator.DateTime.Before when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v < valueDateTime;
                }
                ,

                FilterOperator.DateTime.OnOrBefore when Value != null => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v <= valueDateTime;
                }
                ,

                FilterOperator.DateTime.Empty => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v == null;
                }
                ,

                FilterOperator.DateTime.NotEmpty => x =>
                {
                    var v = GetDateTimeFromObject(((IDictionary<string, object>)x)[Field]);

                    return v != null;
                }
                ,

                _ => x => true
            };
        }
        internal static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(double),
            typeof(decimal),
            typeof(long),
            typeof(short),
            typeof(sbyte),
            typeof(byte),
            typeof(ulong),
            typeof(ushort),
            typeof(uint),
            typeof(float),
            typeof(BigInteger),
            typeof(int?),
            typeof(double?),
            typeof(decimal?),
            typeof(long?),
            typeof(short?),
            typeof(sbyte?),
            typeof(byte?),
            typeof(ulong?),
            typeof(ushort?),
            typeof(uint?),
            typeof(float?),
            typeof(BigInteger?),
        };

        internal static bool IsNumber([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
        {
            return NumericTypes.Contains(type);
        }

        internal static bool IsEnum([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
        {
            if (null == type)
                return false;

            if (type.IsEnum)
                return true;

            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u.IsEnum;
        }

        internal static bool IsDateTime([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
        {
            if (type == typeof(System.DateTime))
                return true;

            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u == typeof(System.DateTime);
        }

        internal static bool IsBoolean([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
        {
            if (type == typeof(bool))
                return true;

            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u == typeof(bool);
        }

        internal static bool IsGuid(Type type)
        {
            if (type == typeof(System.Guid))
                return true;

            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u == typeof(System.Guid);
        }

        private static bool IsNullableEnum(Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }

        private string GetStringFromObject(object o)
        {
            if (o == null)
                return null;
            else if (o.GetType() == typeof(JsonElement))
            {
                return ((JsonElement)o).GetString();
            }
            else
            {
                return (string)o;
            }
        }

        private double? GetDoubleFromObject(object o)
        {
            if (o == null)
                return null;

            if (o.GetType() == typeof(JsonElement))
            {
                return ((JsonElement)o).GetDouble();
            }
            else
            {
                return Convert.ToDouble(o);
            }
        }

        private Enum GetEnumFromObject(object o)
        {
            if (o == null)
                return null;

            if (o.GetType() == typeof(JsonElement))
            {
                return (Enum)Enum.ToObject(DataType, ((JsonElement)o).GetInt32());
            }
            else
            {
                return (Enum)Enum.ToObject(DataType, o);
            }
        }

        private bool? GetBoolFromObject(object o)
        {
            if (o == null)
                return null;

            if (o.GetType() == typeof(JsonElement))
            {
                return ((JsonElement)o).GetBoolean();
            }
            else
            {
                return Convert.ToBoolean(o);
            }
        }

        private DateTime? GetDateTimeFromObject(object o)
        {
            if (o == null)
                return null;

            if (o.GetType() == typeof(JsonElement))
            {
                return ((JsonElement)o).GetDateTime();
            }
            else
            {
                return Convert.ToDateTime(o);
            }
        }

        private Guid? GetGuidFromObject(object o)
        {
            if (o == null)
                return null;

            if (o.GetType() == typeof(JsonElement))
            {
                return ParseGuid(((JsonElement)o).GetString());
            }
            else
            {
                return ParseGuid(Convert.ToString(o));
            }
        }

        private Guid? ParseGuid(string value)
        {
            if (value != null && Guid.TryParse(value, out Guid guid))
            {
                return guid;
            }
            else
            {
                return null;
            }
        }
    }
}
