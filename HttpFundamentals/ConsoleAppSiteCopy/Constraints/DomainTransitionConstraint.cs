using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCopy;
using SiteCopy.Abstract;

namespace ConsoleAppSiteCopy.Constraints
{
    public class DomainTransitionConstraint : IConstraint
    {
        private readonly Uri _parentUri;
        private readonly DomainTransition _availableTransition;

        public DomainTransitionConstraint(DomainTransition availableTransition, Uri parentUri)
        {
            switch (availableTransition)
            {
                case DomainTransition.All:
                case DomainTransition.CurrentDomainOnly:
                case DomainTransition.DescendantUrlsOnly:
                    _availableTransition = availableTransition;
                    _parentUri = parentUri;
                    break;
                default:
                    throw new ArgumentException($"Unknown transition type: {availableTransition}");
            }
        }

        public ConstraintType ConstraintType => ConstraintType.UrlConstraint | ConstraintType.FileConstraint;

        public bool IsAcceptable(Uri uri)
        {
            switch (_availableTransition)
            {
                case DomainTransition.All:
                    return true;
                case DomainTransition.CurrentDomainOnly:
                    if (_parentUri.DnsSafeHost == uri.DnsSafeHost)
                    {
                        return true;
                    }
                    break;
                case DomainTransition.DescendantUrlsOnly:
                    if (_parentUri.IsBaseOf(uri))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
