using System.ComponentModel;
using System.Reflection;

namespace ProbablyRed.Common.Models.Calculators
{
    public abstract class CalculatorBase
    {
        public string CalculationType => this.GetType().ToString();
        public IEnumerable<CalculatorInput> InputProperties { get { return this.GetType().GetProperties().Where(n => n.GetCustomAttributes().Any(i => (i as DataInputAttribute) != null)).Select(n => new CalculatorInput() { Name = n.Name, DataType = n.PropertyType.ToString() }); } }
    }
}
