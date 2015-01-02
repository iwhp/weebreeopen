namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.Linq;

    public abstract partial class Specification<T>
    {
        public bool IsSatisfiedBy(T item)
        {
            return this.SatisfyingElementsFrom(new[] { item }.AsQueryable()).Any();
        }

        public abstract IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates);
    }
}