using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDataPracticalTest.DataTransferObjects.Security
{
    public record AccessTokenDto(string Name, string UserName, string Email, string? AccessToken);
}
