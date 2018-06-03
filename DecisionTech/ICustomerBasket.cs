using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTech
{
  public interface ICustomerBasket
  {
    void AddProduct(Product product, int count);
    decimal CalculateDiscountedTotalCost();
  }
}
