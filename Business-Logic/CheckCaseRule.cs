using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla.Rules;

namespace Business_Logic
{
    public class CheckCaseRule : BusinessRule
    {
        public CheckCaseRule(Csla.Core.IPropertyInfo primaryProperty)
     : base(primaryProperty)
        { }
        protected override void Execute(IRuleContext context)
        {
            var text = (string)ReadProperty(context.Target, PrimaryProperty);
            if (string.IsNullOrWhiteSpace(text)) return;
            var ideal = text.Substring(0, 1).ToUpper();
            ideal += text.Substring(1).ToLower();
            if (text != ideal)
                context.AddWarningResult("Check capitalization");
        }
    }
}
