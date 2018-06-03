using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTech
{
  public interface IDiscountCalculator
  {
    decimal CalculateDiscount(Dictionary<Product, int> products);
  }
}
