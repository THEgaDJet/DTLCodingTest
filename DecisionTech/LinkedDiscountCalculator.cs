using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DecisionTech
{
  public class LinkedDiscountCalculator : DiscountCalculatorBase, IDiscountCalculator
  {
    private Product qualifyingProduct;
    private Product discountProduct;
    private int requiredQualifyingProductCount;
    private int requiredDiscountProductCount;
    private float multiplier;

    public LinkedDiscountCalculator(Product qualifyingProduct, Product discountProduct, int requiredQualifyingProductCount, int requiredDiscountProductCount, float multiplier)
    {
      this.qualifyingProduct = qualifyingProduct;
      this.discountProduct = discountProduct;
      this.requiredQualifyingProductCount = requiredQualifyingProductCount;
      this.requiredDiscountProductCount = requiredDiscountProductCount;
      this.multiplier = multiplier;

      if (requiredQualifyingProductCount < 1 || requiredDiscountProductCount < 1)
      {
        throw new Exception("Required product counts must be greater than or equal to 1");
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
      int qualifyingProductCount = GetProductCount(products, qualifyingProduct);

      int discountProductCount = GetProductCount(products, discountProduct);

      bool basketQualifies = ProductsQualifyForDiscount(requiredQualifyingProductCount, qualifyingProductCount)
        && ProductsQualifyForDiscount(requiredDiscountProductCount, discountProductCount);

      decimal numberOfDiscounts = basketQualifies ? CalculateMaxDiscounts(qualifyingProductCount, discountProductCount) : 0;

      return (int)numberOfDiscounts;
    }

    private int CalculateMaxDiscounts(int qualifyingProductCount, int discountProductCount)
    {
      decimal numberOfQualifyingProducts = Math.Floor((decimal)(qualifyingProductCount / requiredQualifyingProductCount));

      decimal numberOfDiscountProducts = Math.Floor((decimal)(discountProductCount / requiredDiscountProductCount));
   
      return (int)Math.Min(numberOfQualifyingProducts, numberOfDiscountProducts);
    }
  }
}
