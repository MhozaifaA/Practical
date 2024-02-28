using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public class ColumnDataType : ColumnAttribute
    {
        public ColumnDataType(string? Name) : base(Name) { }

        public ColumnDataType(string? Name, int Order) : this(Name)
        {
            base.Order = Order;
        }
        public ColumnDataType(string? Name, int Order, string? TypeName) : this(Name)
        {
            base.Order = Order;
            base.TypeName = TypeName;
        }


        public ColumnDataType(DataBaseTypes type, int size = 0, int scale = 0)
        {
            base.TypeName = type.ToString().ToLower();

            switch (type)
            {
                case DataBaseTypes.CHAR or DataBaseTypes.NCHAR or DataBaseTypes.VARCHAR or DataBaseTypes.NVARCHAR:
                    if (size == 0)
                        base.TypeName += StringExtension.IntoParentheses(FixedCommonValue.Max);
                    else
                        base.TypeName += StringExtension.IntoParentheses(size.ToString());
                    break;
                case DataBaseTypes.FLOAT:
                    if (size > 0)
                        base.TypeName += StringExtension.IntoParentheses(size.ToString());
                    break;
                case DataBaseTypes.DECIMAL:
                    if (size > 0 && scale > 0)
                        base.TypeName += StringExtension.IntoParentheses(size.ToString(), scale.ToString());
                    break;
            }
        }


        public ColumnDataType(string name, DataBaseTypes type, int size = 0, int scale = 0) : this(name)
        {
            base.TypeName = type.ToString().ToLower();

            switch (type)
            {
                case DataBaseTypes.CHAR or DataBaseTypes.NCHAR or DataBaseTypes.VARCHAR or DataBaseTypes.NVARCHAR:
                    if (size == 0)
                        base.TypeName += StringExtension.IntoParentheses(FixedCommonValue.Max);
                    else
                        base.TypeName += StringExtension.IntoParentheses(size.ToString());
                    break;
                case DataBaseTypes.FLOAT:
                    if (size > 0)
                        base.TypeName += StringExtension.IntoParentheses(size.ToString());
                    break;
                case DataBaseTypes.DECIMAL:
                    if (size > 0 && scale > 0)
                        base.TypeName += StringExtension.IntoParentheses(size.ToString(), scale.ToString());
                    break;
            }
        }


    }
}