using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AskQuestion.Core.Aspects.Postsharp.AuthorizationAspects
{

    [Serializable]
    public class SignedInOperation : OnMethodBoundaryAspect
    {

        public override void OnEntry(MethodExecutionArgs args)
        {

            ClaimsIdentity identity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;

            if (!identity.IsAuthenticated)
            {
                throw new SecurityException("You are not authorized!");
            }

        }

    }

}
