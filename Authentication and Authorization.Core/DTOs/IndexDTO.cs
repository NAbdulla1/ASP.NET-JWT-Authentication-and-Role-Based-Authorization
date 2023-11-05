using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_and_Authorization.Core.DTOs
{
    public class IndexDTO
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; } = 10;
        public string? SortBy { get; set; }

        [RegularExpression("asc|desc", ErrorMessage = "Sort order should either 'asc' or 'desc'.")]
        public string? SortOrder { get; set; }
    }
}
