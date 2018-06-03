using System;
using System.Collections.Generic;

namespace DecisionTech
{
  public class SingleDiscountCalculator : DiscountCalculatorBase, IDiscountCalculator
  {
    private Product discountProduct;
    private int requiredProductCount;
    private float multiplier;

    public SingleDiscountCalculator(Product discountProduct, int requiredProductCount, float multiplier)
    {
      this.discountProduct = discountProduct;
      this.requiredProductCount = requiredProductCount; //must be at least 1
      this.multiplier = multiplier;

      if (requiredProductCount < 1)
      {
        throw new Exception("Required product count must be greater than or equal to 1");
      }
    }

    public decimal CalculateDiscount(Dictionary<Product, int> products)
    {
      decimal discount = 0;

      int numberOfDiscounts = CalculateDiscountsToApply(products);

      discount = discountProduct.Cost * numberOfDiscounts * (decimal)multiplier;

      return discount;
    }

    private int CalculateDiscountsToApply(Dictionary<Product, int> products)
    {
      int qualifyingProductCount = GetProductCount(products, discountProduct);

      decimal numberOfDiscounts = ProductsQualifyForDiscount(requiredProductCount, qualifyingProductCount)
        ? Math.Floor((decimal)(qualifyingProductCount / requiredProductCount))
        : 0;

      return (int)numberOfDiscounts;
    }
  }
}
