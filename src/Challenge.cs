using System;

namespace DesignPatternChallenge
{
    // ============================
    // 1) COMPONENT
    // ============================
    public interface IBeverage
    {
        decimal GetCost();
        string GetDescription();
    }

    // ============================
    // 2) CONCRETE COMPONENTS (Bebidas base)
    // ============================
    public class Espresso : IBeverage
    {
        public decimal GetCost() => 3.50m;
        public string GetDescription() => "Espresso";
    }

    public class Cappuccino : IBeverage
    {
        public decimal GetCost() => 4.50m;
        public string GetDescription() => "Cappuccino";
    }

    public class Tea : IBeverage
    {
        public decimal GetCost() => 3.00m;
        public string GetDescription() => "Chá";
    }

    // ============================
    // 3) DECORATOR BASE
    // ============================
    public abstract class BeverageDecorator : IBeverage
    {
        protected readonly IBeverage _beverage;

        protected BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public virtual decimal GetCost() => _beverage.GetCost();
        public virtual string GetDescription() => _beverage.GetDescription();
    }

    // ============================
    // 4) CONCRETE DECORATORS (Complementos)
    // ============================
    public class Milk : BeverageDecorator
    {
        public Milk(IBeverage beverage) : base(beverage) { }
        public override decimal GetCost() => base.GetCost() + 0.50m;
        public override string GetDescription() => base.GetDescription() + " com Leite";
    }

    public class Chocolate : BeverageDecorator
    {
        public Chocolate(IBeverage beverage) : base(beverage) { }
        public override decimal GetCost() => base.GetCost() + 0.70m;
        public override string GetDescription() => base.GetDescription() + " com Chocolate";
    }

    public class WhippedCream : BeverageDecorator
    {
        public WhippedCream(IBeverage beverage) : base(beverage) { }
        public override decimal GetCost() => base.GetCost() + 1.00m;
        public override string GetDescription() => base.GetDescription() + " com Chantilly";
    }

    public class Caramel : BeverageDecorator
    {
        public Caramel(IBeverage beverage) : base(beverage) { }
        public override decimal GetCost() => base.GetCost() + 0.80m;
        public override string GetDescription() => base.GetDescription() + " com Caramelo";
    }

    // ============================
    // 5) DEMO
    // ============================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Pedidos - Cafeteria (Decorator) ===\n");

            // Espresso simples
            IBeverage order1 = new Espresso();
            Print(order1);

            // Espresso com leite
            IBeverage order2 = new Milk(new Espresso());
            Print(order2);

            // Espresso com leite e chocolate
            IBeverage order3 = new Chocolate(new Milk(new Espresso()));
            Print(order3);

            // Cappuccino com leite + chocolate + chantilly
            IBeverage order4 = new WhippedCream(new Chocolate(new Milk(new Cappuccino())));
            Print(order4);

            // Espresso com tudo (Leite + Chocolate + Chantilly + Caramelo)
            IBeverage order5 = new Caramel(new WhippedCream(new Chocolate(new Milk(new Espresso()))));
            Print(order5);

            Console.WriteLine("\n✅ Sem explosão combinatória, complementos adicionados em runtime.");
            Console.ReadLine();
        }

        static void Print(IBeverage beverage)
        {
            Console.WriteLine($"{beverage.GetDescription()}: R$ {beverage.GetCost():N2}");
        }
    }
}