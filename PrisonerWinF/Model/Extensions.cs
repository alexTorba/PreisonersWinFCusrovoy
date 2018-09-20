using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PrisonerWinF.View;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;


namespace PrisonerWinF
{
    public static class Extensions
    {

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> sequence, string propertyName)
        {
            //в данном случае Т - Prisoner

            /*lambda expressions :
             value => value.propertyName
             */
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "value");
            Expression propertyExpression = Expression.Property(parameterExpression, propertyName);
            var lambda = Expression.Lambda(propertyExpression, parameterExpression).Compile();

            /*так как OrderBy принимает Func<T,TKey>, где Т - известно, а TKey - нет (не постоянно, т.к тип значения по которому происходит сортировка не одинаковый каждый раз) нужно получить OrderBy рефлексией и сделать его типизированным в зависимости от типа propertyExpression*/
            var orderBy = typeof(Enumerable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                .First()
                .MakeGenericMethod(typeof(T), propertyExpression.Type);

            var rezult = (IEnumerable<T>)orderBy.Invoke(null, new object[] { sequence, lambda });

            return rezult;
        }

        public static bool CheckNumber(this string text)
        {
            Regex regex = new Regex(@"[\d+$%';:&#@!?]");
            if (regex.IsMatch(text))
                return false;

            return true;
        }

        public static bool Check(this string text)
        {
            Regex regex = new Regex(@"[\d+$%';:&#@!?]");
            if (regex.IsMatch(text) ||
                text.Length < 2 ||
                string.IsNullOrEmpty(text))
                return true;

            return false;
        }

        public static string FindMethod(this string text)
        {
            return typeof(Generate).GetMethods().SingleOrDefault(x => x.Name.ToLowerInvariant().Contains(text.ToLowerInvariant())).Name;
        }

        public static DateTime GenerateDateTime(this MonthCalendar monthCalendar)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            string rezult = null;
            rezult += Convert.ToString(rand.Next(1, 30)) + ".";
            rezult += Convert.ToString(rand.Next(1, 12)) + ".";
            rezult += Convert.ToString(rand.Next(1950, 1995));

            return Convert.ToDateTime(rezult);
        }
    }
}
