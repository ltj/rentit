// -----------------------------------------------------------------------
// <copyright file="LINQExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Method that provides exist-quantification on a IQueryable.
        /// </summary>
        /// <remarks>Returns whether or not the predicate conditions exists at least one time.</remarks>
        public static bool Exists<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return source.Where(predicate).Any();
        }
    }
}
