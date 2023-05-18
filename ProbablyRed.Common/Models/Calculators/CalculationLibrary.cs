using System.Reflection;

namespace ProbablyRed.Common.Models.Calculators
{
    public class CalculationLibrary
    {
        public static IEnumerable<IStrategyCalculator> GetAvailableCalculators() {
            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IStrategyCalculator))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IStrategyCalculator;
            return instances;
        }        
    }
}
