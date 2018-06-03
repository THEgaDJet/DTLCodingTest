using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTech
{
  public class DiscountCalculatorBase
  {
    protected bool ProductExists(Dictionary<Product, int> products, Product product)
    {
      return products.ContainsKey(product);
    }

    protected int GetProductCount(Dictionary<Product, int> products, Product product)
    {
      return ProductExists(products, product) ? products[product] : 0;
    }

    protected bool ProductsQualifyForDiscount(int numberRequired, int productCount)
    {
      return productCount >= numberRequired;
    }
  }
}
