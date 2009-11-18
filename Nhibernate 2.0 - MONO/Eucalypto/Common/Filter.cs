using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Eucalypto.Common
{
  /// <summary>
  /// A filter to specify a list of values using a JunctionOperator (AND, OR) and a ValueOperator (Equal, NotEqual, ...).
  /// This class can generate a NHibernate ICriterion to be used inside queries.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Filter<T>
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pJunctionOperator"></param>
    /// <param name="pValueOperator"></param>
    /// <param name="pValues"></param>
    public Filter(JunctionOperator pJunctionOperator,
                            ValueOperator pValueOperator,
                            params T[] pValues)
    {
      mJunctionOperator = pJunctionOperator;
      mValueOperator = pValueOperator;

      if (pValues != null)
      {
        Values.AddRange(pValues);
      }
    }

    private JunctionOperator mJunctionOperator;
    public JunctionOperator JunctionOperator
    {
      get { return mJunctionOperator; }
    }

    private ValueOperator mValueOperator;
    public ValueOperator ValueOperator
    {
      get { return mValueOperator; }
    }

    private List<T> mValues = new List<T>();
    public List<T> Values
    {
      get { return mValues; }
    }

    /// <summary>
    /// Convert the filter to a ICriterion that can be used with NHibernate
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public virtual ICriterion ToCriterion(string propertyName)
    {
      Junction junction;
      if (JunctionOperator == JunctionOperator.AND)
        junction = new Conjunction();
      else if (JunctionOperator == JunctionOperator.OR)
        junction = new Disjunction();
      else
        throw new ArgumentException("Invalid value", "JunctionOperator");

      //This code is used when the values are empty because otherwise 
      // some version of NHibernate returns false if the Conjunction or Disjunction is empty
      if (Values.Count == 0)
        junction.Add(Restrictions.Or(Restrictions.IsNull(propertyName), Restrictions.IsNotNull(propertyName)));

      foreach (object val in Values)
      {
        if (ValueOperator == ValueOperator.Equal)
            junction.Add(Restrictions.Eq(propertyName, val));
        else if (ValueOperator == ValueOperator.NotEqual)
            junction.Add(Restrictions.Not(Restrictions.Eq(propertyName, val)));
        else if (ValueOperator == ValueOperator.Contains)
        {
          if (val is string == false)
            throw new ArgumentException("For Contains operator the value must be a string");
          junction.Add(Restrictions.Like(propertyName, (string)val, MatchMode.Anywhere));
        }
        else if (ValueOperator == ValueOperator.StartWith)
        {
          if (val is string == false)
            throw new ArgumentException("For StartWith operator the value must be a string");
          junction.Add(Restrictions.Like(propertyName, (string)val, MatchMode.Start));
        }
        else if (ValueOperator == ValueOperator.EndWith)
        {
          if (val is string == false)
            throw new ArgumentException("For EndWith operator the value must be a string");
          junction.Add(Restrictions.Like(propertyName, (string)val, MatchMode.End));
        }
        else
          throw new ArgumentException("Invalid value", "ValueOperator");
      }

      return junction;
    }
  }

    /// <summary>
    /// Filter Factory
    /// </summary>
  public static class Filter
  {
    #region Factory methods

    /// <summary>
    /// Create a filter for string values using an AND junction and a LIKE operator.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Filter<string> ContainsAll(params string[] values)
    {
      return new Filter<string>(JunctionOperator.AND, ValueOperator.Contains, values);
    }

    /// <summary>
    /// Create a filter for string values using an OR junction and a LIKE operator.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Filter<string> ContainsOne(params string[] values)
    {
      return new Filter<string>(JunctionOperator.OR, ValueOperator.Contains, values);
    }

    /// <summary>
    /// Create a filter using an OR junction and an EQUAL operator
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Filter<T> MatchOne<T>(params T[] values)
    {
      return new Filter<T>(JunctionOperator.OR, ValueOperator.Equal, values);
    }

    #endregion
  }
}
