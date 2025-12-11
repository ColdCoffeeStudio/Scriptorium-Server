using System;
using System.Collections.Generic;

namespace Infrastructure.Contexts;

public partial class Theme
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string? folder { get; set; }

    public virtual ICollection<Annotation> Annotations { get; set; } = new List<Annotation>();
}
