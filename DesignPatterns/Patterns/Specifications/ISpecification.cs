namespace DesignPatterns.Patterns.Specifications {

    public interface ISpecification<T> {
        bool IsSatisfiedBy(T candidate);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> AndAlso(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> OrElse(ISpecification<T> other);
        ISpecification<T> Not();
    }

    public abstract class CompositeSpecification<T> : ISpecification<T> {
        public abstract bool IsSatisfiedBy(T candidate);
        public ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);
        public ISpecification<T> AndAlso(ISpecification<T> other) => new AndAlsoSpecification<T>(this, other);
        public ISpecification<T> Or(ISpecification<T> other) => new OrSpecification<T>(this, other);
        public ISpecification<T> OrElse(ISpecification<T> other) => new OrElseSpecification<T>(this, other);
        public ISpecification<T> Not() => new NotSpecification<T>(this);
    }

    public class AndSpecification<T> : CompositeSpecification<T> {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right) {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate) {
            return _left.IsSatisfiedBy(candidate) & _right.IsSatisfiedBy(candidate);
        }
    }

    public class AndAlsoSpecification<T> : CompositeSpecification<T> {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndAlsoSpecification(ISpecification<T> left, ISpecification<T> right) {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate) {
            return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T> {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right) {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate) {
            return _left.IsSatisfiedBy(candidate) | _right.IsSatisfiedBy(candidate);
        }
    }

    public class OrElseSpecification<T> : CompositeSpecification<T> {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrElseSpecification(ISpecification<T> left, ISpecification<T> right) {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate) {
            return _left.IsSatisfiedBy(candidate) || _right.IsSatisfiedBy(candidate);
        }
    }

    public class NotSpecification<T> : CompositeSpecification<T> {
        private readonly ISpecification<T> _other;

        public NotSpecification(ISpecification<T> other) {
            _other = other;
        }

        public override bool IsSatisfiedBy(T candidate) {
            return !_other.IsSatisfiedBy(candidate);
        }
    }

}