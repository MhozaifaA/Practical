using System;
using System.Collections.Generic;
using System.Text;

namespace Meteors.AspNetCore.Infrastructure.EntityFramework.Util
{
    /// <summary>
    /// Pointer funcation invoked before <c> SaveChanges() </c>  DbContext
    /// </summary>
    public delegate void BeforeSaveChangesSignature();
}
