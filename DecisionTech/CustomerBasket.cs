using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTech
{
  public class CustomerBasket : ICustomerBasket
  {
    private Dictionary<Product, int> products;
    private List<IDiscountCalculator> discountCalculators;

    public Dictionary<Product, int> Products { get => products; }

    public CustomerBasket()
    {
      products = new Dictionary<Product, int>();
      discountCalculators = new List<IDiscountCalculator>();
    }

    public void AddProduct(Product product, int count = 1)
    {
      if (BasketContainsProduct(product))
      {
        products[product] += count;
      }
      else
      {
        products.Add(product, count);
      }
    }

    public decimal CalculateDiscountedTotalCost()
    {
      decimal totalCost = CalculateTotalCost();
      decimal discount = CalculateDiscount();

      return totalCost - discount;
    }

    public void RegisterDiscountCalculator(IDiscountCalculator discountCalculator)
    {
      if (!discountCalculators.Contains(discountCalculator))
      {
        discountCalculators.Add(discountCalculator);
      }
      else
      {
        throw new DiscountExistsException("Discount Calculator instance already registered");
      }
    }

    public int GetProductCount(Product product)
    {
      return BasketContainsProduct(product) ? products[product] : 0;
    }

    private bool BasketContainsProduct(Product product)
    {
      // For simplicty assuming that a product is the same if it is the same instance
      // Not assuming it is the same if it has the same name as it could have the same name but a different price
      return products.ContainsKey(product);
    }

    private decimal CalculateTotalCost()
    {
      return products.Sum(p => p.Key.Cost * p.Value);
    }

    private decimal CalculateDiscount()
    {
      decimal discount = 0;

      foreach (var calculator in discountCalculators)
      {
        discount += calculator.CalculateDiscount(products);
      }

      return discount;
    }
  }

  [Serializable]
  public class DiscountExistsException : Exception
  {
    public DiscountExistsException() { }
    public DiscountExistsException(string message) : base(message) { }
    public DiscountExistsException(string message, Exception inner) : base(message, inner) { }
    protected DiscountExistsException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
